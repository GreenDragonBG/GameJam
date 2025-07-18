using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]public Transform player;         // The player to follow
    [SerializeField]public Vector3 offset = new Vector3(0, 2, -2); // Camera offset from player
    public float mouseSensitivity = 3f;    // Mouse sensitivity
    public float distance = 2f;            // Distance from player
    public float height = 1f;              // Height above player
    public float rotationSpeed = 100f;       // Smooth speed

    private float yaw = 0f;                // Horizontal rotation
    private float pitch = 15f;             // Vertical angle

    public float minPitch = -20f;          // Clamp pitch
    public float maxPitch = 60f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor for mouse look
        Cursor.visible = false;

        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void Update()
    {
        // Mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Calculate rotation
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // Desired position around the player
        Vector3 desiredPosition = player.position - (rotation * Vector3.forward * distance) + Vector3.up * height;

        // Smooth position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, rotationSpeed * Time.deltaTime);

        // Look at player
        transform.LookAt(player.position + Vector3.up * height);
    }
}