using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public bool IsFrozen = false;

    public float moveSpeed = 5f;
    public float boostValue = 5f;

    public float mouseSensitivity = 200f;
    public Transform playerCamera;

    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpHeight = 2f; // Height in meters the player can jump
    private float verticalVelocity;

    private CharacterController characterController;
    private float xRotation = 0f;

    void Start()
    {
        // Fetch the Character Controller component automatically
        characterController = GetComponent<CharacterController>();

        // Locks the cursor to the center of the screen and hides it
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (IsFrozen)
        {
            return;
        }

        HandleLook();
        HandleMovement();

        // speeds up the player when shift is pressed
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

        // Apply movement speed
        moveDirection *= moveSpeed;

        // Handle jump and gravity mechanics
        if (characterController.isGrounded)
        {
            // Small downward force to firmly plant the controller on hills when walking
            verticalVelocity = -0.5f;

            // Check for jump input
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Physics formula to calculate initial velocity needed to reach a specific jump height: v = sqrt(height * 2 * g)
                verticalVelocity = Mathf.Sqrt(jumpHeight * 2f * gravity);
            }
        }
        else
        {
            // Fall over time when in mid-air
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // Apply the gravity/jump calculation to the Y axis
        moveDirection.y = verticalVelocity;

        // Moves safely using the physics system instead of transform.Translate
        characterController.Move(moveDirection * Time.deltaTime);
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