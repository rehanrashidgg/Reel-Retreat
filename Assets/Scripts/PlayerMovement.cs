using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float moveSpeed = 5f;
    public float boostValue = 5f;

    public float mouseSensitivity = 200f;
    public Transform playerCamera; 

    private float xRotation = 0f;

    void Start()
    {
        // Locks the cursor to the center of the screen and hides it
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleLook();
        HandleMovement();

        //speeds up the player when shift is pressed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed += boostValue;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed -= boostValue;
        }
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Moves relative to the direction the player is currently facing
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    void HandleLook()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player body horizontally Left & Right
        transform.Rotate(Vector3.up * mouseX);

        // Calculate and clamp vertical look Up & Down to prevent flipping upside down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotation to the camera locally
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
