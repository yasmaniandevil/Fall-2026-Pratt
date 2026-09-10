using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{   
    public float moveSpeed = 5;
    public float lookSensativity = 0.1f;
    public Transform camera;
    private float minPitch = -90;
    private float maxPitch = 90;
    private Rigidbody rb;
    private Vector2 moveInput; //(x,y)
    private Vector2 lookInput;
    private float pitch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    public void OnLook(InputValue inputValue)
    {
        lookInput = inputValue.Get<Vector2>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //yaw: rotate the whole player body let and right aroud the Y axis
        float yaw = lookInput.x * lookSensativity;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, yaw, 0f));

        //pitch: rotate only up and down (the camera) clamped so it doesnt flip over
        pitch -= lookInput.y * lookSensativity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        camera.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        //movement relative to the players forward direction, which yaw above just updated
        Vector3 movePos = transform.forward * moveInput.y + transform.right * moveInput.x;
        //this actually is what moves our rb
        rb.MovePosition(rb.position + movePos * moveSpeed * Time.fixedDeltaTime); 
    }
}
