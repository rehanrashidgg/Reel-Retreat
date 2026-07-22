using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace WaterSystem
{
    public class WaterSystemFeature : ScriptableRendererFeature
    {
        [System.Serializable]
        public class WaterSystemSettings
        {
            public enum DebugMode { Disabled, WaterEffects, Caustics }
            public DebugMode debug = DebugMode.Disabled;
            public float causticBlendDistance = 10f;
        }

        #region Water Effects Pass

        class WaterFxPass : ScriptableRenderPass
        {
            private const string k_RenderWaterFXTag = "Render Water FX";
            private ProfilingSampler m_WaterFX_Profile = new ProfilingSampler(k_RenderWaterFXTag);
            private readonly ShaderTagId m_WaterFXShaderTag = new ShaderTagId("WaterFX");
            private readonly Color m_ClearColor = new Color(0.0f, 0.5f, 0.5f, 0.5f); // r = foam mask, g = normal.x, b = normal.z, a = displacement
            private FilteringSettings m_FilteringSettings;

            private RTHandle m_WaterFXHandle;
            private static readonly int m_WaterFXMapId = Shader.PropertyToID("_WaterFXMap");

            public WaterFxPass()
            {
                m_FilteringSettings = new FilteringSettings(RenderQueueRange.transparent);
            }

            public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
            {
                RenderTextureDescriptor cameraTextureDescriptor = renderingData.cameraData.cameraTargetDescriptor;
                cameraTextureDescriptor.depthBufferBits = 0;
                cameraTextureDescriptor.width /= 2;
                cameraTextureDescriptor.height /= 2;
                cameraTextureDescriptor.colorFormat = RenderTextureFormat.Default;

                // Re-allocate the RTHandle safely for Unity 6
                RenderingUtils.ReAllocateIfNeeded(ref m_WaterFXHandle, cameraTextureDescriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, name: "_WaterFXMap");

                ConfigureTarget(m_WaterFXHandle);
                ConfigureClear(ClearFlag.Color, m_ClearColor);
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                // Unity 6 requires CommandBuffer from CommandBufferPool
                CommandBuffer cmd = CommandBufferPool.Get();
                using (new ProfilingScope(cmd, m_WaterFX_Profile))
                {
                    // Global property configuration to make the texture readable by the water shaders
                    cmd.SetGlobalTexture(m_WaterFXMapId, m_WaterFXHandle);
                    context.ExecuteCommandBuffer(cmd);
                    cmd.Clear();

                    var drawSettings = CreateDrawingSettings(m_WaterFXShaderTag, ref renderingData, SortingCriteria.CommonTransparent);
                    context.DrawRenderers(renderingData.cullResults, ref drawSettings, ref m_FilteringSettings);
                }
                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }

            public override void OnCameraCleanup(CommandBuffer cmd)
            {
                // Cleanup targeting values
            }

            public void Dispose()
            {
                m_WaterFXHandle?.Release();
            }
        }

        #endregion

        #region Caustics Pass

        class WaterCausticsPass : ScriptableRenderPass
        {
            private const string k_RenderWaterCausticsTag = "Render Water Caustics";
            private ProfilingSampler m_WaterCaustics_Profile = new ProfilingSampler(k_RenderWaterCausticsTag);
            public Material WaterCausticMaterial;
            private static Mesh m_mesh;

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                var cam = renderingData.cameraData.camera;
                if (cam.cameraType == CameraType.Preview || !WaterCausticMaterial)
                    return;

                CommandBuffer cmd = CommandBufferPool.Get();
                using (new ProfilingScope(cmd, m_WaterCaustics_Profile))
                {
                    var sunMatrix = RenderSettings.sun != null
                        ? RenderSettings.sun.transform.localToWorldMatrix
                        : Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(-45f, 45f, 0f), Vector3.one);
                    WaterCausticMaterial.SetMatrix("_MainLightDir", sunMatrix);

                    if (!m_mesh)
                        m_mesh = GenerateCausticsMesh(1000f);

                    var position = cam.transform.position;
                    position.y = 0;
                    var matrix = Matrix4x4.TRS(position, Quaternion.identity, Vector3.one);

                    cmd.DrawMesh(m_mesh, matrix, WaterCausticMaterial, 0, 0);
                }

                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }

            private Mesh GenerateCausticsMesh(float size)
            {
                Mesh mesh = new Mesh();
                Vector3[] vertices = new Vector3[4]
                {
                    new Vector3(-size, 0, -size),
                    new Vector3(size, 0, -size),
                    new Vector3(-size, 0, size),
                    new Vector3(size, 0, size)
                };
                int[] tris = new int[6] { 0, 2, 1, 2, 3, 1 };
                Vector2[] uv = new Vector2[4]
                {
                    new Vector2(0, 0),
                    new Vector2(1, 0),
                    new Vector2(0, 1),
                    new Vector2(1, 1)
                };

                mesh.vertices = vertices;
                mesh.triangles = tris;
                mesh.uv = uv;
                mesh.RecalculateNormals();
                return mesh;
            }
        }

        #endregion

        WaterFxPass m_WaterFxPass;
        WaterCausticsPass m_CausticsPass;

        public WaterSystemSettings settings = new WaterSystemSettings();
        [HideInInspector][SerializeField] private Shader causticShader;
        [HideInInspector][SerializeField] private Texture2D causticTexture;

        private Material _causticMaterial;

        private static readonly int SrcBlend = Shader.PropertyToID("_SrcBlend");
        private static readonly int DstBlend = Shader.PropertyToID("_DstBlend");
        private static readonly int CausticTexture = Shader.PropertyToID("_CausticMap");

        public override void Create()
        {
            m_WaterFxPass = new WaterFxPass { renderPassEvent = RenderPassEvent.BeforeRenderingOpaques };
            m_CausticsPass = new WaterCausticsPass();

            causticShader = causticShader ? causticShader : Shader.Find("Hidden/BoatAttack/Caustics");
            if (causticShader == null) return;

            if (_causticMaterial)
            {
                CoreUtils.Destroy(_causticMaterial);
            }
            _causticMaterial = CoreUtils.CreateEngineMaterial(causticShader);
            _causticMaterial.SetFloat("_BlendDistance", settings.causticBlendDistance);

            if (causticTexture == null)
            {
                Debug.Log("Caustics Texture missing, attempting to load.");
#if UNITY_EDITOR
                causticTexture = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>("Packages/com.verasl.water-system/Textures/WaterSurface_single.tif");
#endif
            }
            if (causticTexture != null)
            {
                _causticMaterial.SetTexture(CausticTexture, causticTexture);
            }

            m_CausticsPass.WaterCausticMaterial = _causticMaterial;

            switch (settings.debug)
            {
                case WaterSystemSettings.DebugMode.Caustics:
                    _causticMaterial.SetFloat(SrcBlend, 1f);
                    _causticMaterial.SetFloat(DstBlend, 0f);
                    _causticMaterial.EnableKeyword("_DEBUG");
                    m_CausticsPass.renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
                    break;
                case WaterSystemSettings.DebugMode.WaterEffects:
                    m_CausticsPass.renderPassEvent = RenderPassEvent.BeforeRenderingOpaques;
                    break;
                case WaterSystemSettings.DebugMode.Disabled:
                    _causticMaterial.SetFloat(SrcBlend, 2f);
                    _causticMaterial.SetFloat(DstBlend, 0f);
                    _causticMaterial.DisableKeyword("_DEBUG");
                    m_CausticsPass.renderPassEvent = RenderPassEvent.BeforeRenderingOpaques;
                    break;
            }
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(m_WaterFxPass);
            if (settings.debug != WaterSystemSettings.DebugMode.WaterEffects)
            {
                renderer.EnqueuePass(m_CausticsPass);
            }
        }

        protected override void Dispose(bool disposing)
        {
            m_WaterFxPass?.Dispose();
            if (_causticMaterial)
            {
                CoreUtils.Destroy(_causticMaterial);
            }
        }
    }
}
