using UnityEngine;

public class CrosshairSpawner : MonoBehaviour
{
    [Header("Spawning Settings")]
    public GameObject objectToSpawn;
    public LayerMask spawnableLayers;

    [Header("Line Connection")]
    public FishingStringRenderer lineSystem; // Drag your LineManager object here

    [Header("Input")]
    public KeyCode spawnKey = KeyCode.Mouse0;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            SpawnObjectAtScreenCenter();
        }
    }

    void SpawnObjectAtScreenCenter()
    {
        if (objectToSpawn == null || cam == null) return;

        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = cam.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, spawnableLayers))
        {
            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            // 1. Spawn the object and store a reference to it
            GameObject newSpawnedObject = Instantiate(objectToSpawn, hit.point, spawnRotation);

            // 2. Pass the new object's Transform component to the line system
            if (lineSystem != null)
            {
                lineSystem.SetTarget(newSpawnedObject.transform);
            }
        }
    }
}
