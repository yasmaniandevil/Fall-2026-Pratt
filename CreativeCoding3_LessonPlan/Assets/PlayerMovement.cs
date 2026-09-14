using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSensitivity = 0.1f;
    [SerializeField] private Transform cameraPivot; // camera should be a child of this transform
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;

    private Rigidbody rb;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float pitch;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // we rotate the body manually below; don't let physics tip it over

        if (cameraPivot == null && Camera.main != null)
        {
            cameraPivot = Camera.main.transform;
        }
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // PlayerInput (Broadcast Messages) calls these when "Move" / "Look" fire
    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        // Yaw: rotate the whole player body left/right around Y
        float yaw = lookInput.x * lookSensitivity;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, yaw, 0f));

        // Pitch: rotate only the camera up/down, clamped so it can't flip over
        pitch -= lookInput.y * lookSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // Movement relative to the player's own forward, which yaw above just updated
        Vector3 moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
