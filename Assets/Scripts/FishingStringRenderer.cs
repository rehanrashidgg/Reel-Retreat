using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FishingStringRenderer : MonoBehaviour
{
    [Header("Position Tuning")]
    [Tooltip("Right/Left offset from camera center")]
    public float lineOffsetX = 0.4f;
    [Tooltip("Up/Down offset from camera center")]
    public float lineOffsetY = 0.3f;
    [Tooltip("Forward distance out from camera lens")]
    public float lineOffsetZ = 1.2f;

    private Transform cameraTransform;
    private Transform targetObject;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        // Cache the main camera transform
        if (Camera.main != null) cameraTransform = Camera.main.transform;
    }

    public void SetTarget(Transform newTarget)
    {
        targetObject = newTarget;
    }

    void Update()
    {
        if (cameraTransform == null || targetObject == null)
        {
            if (lineRenderer != null) lineRenderer.positionCount = 0;
            return;
        }

        lineRenderer.positionCount = 2;

        // Calculate the static start point using the camera's fixed position and look direction
        Vector3 startPoint = cameraTransform.position
                           + (cameraTransform.right * lineOffsetX)
                           + (cameraTransform.up * lineOffsetY)
                           + (cameraTransform.forward * lineOffsetZ);

        // Render the line from the calculated rod tip to the bobber
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, targetObject.position);
    }
}
