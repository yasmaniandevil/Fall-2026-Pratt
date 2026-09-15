using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{   
    //a variable is like a bucket
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
        //our empty rigidbody variable called rb
        //now is storing the rigidbody of the player by grabbing the rigidbody component
        rb = GetComponent<Rigidbody>();
        //we are saying the cursor lock state is LOCKED
        //cursor is not visible in play mode
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMove(InputValue inputValue)
    {
       //if we press w (0, 1, 0)
       //if we press S(0, -1, 0)
       //if we press a (-1, 0, 0)
       //if we press D (1,0, 0)
       //move input is storing this information inside a vector 2
        moveInput = inputValue.Get<Vector2>();
    }

    public void OnLook(InputValue inputValue)
    {
        //moveInput is our variable that is empty at the top
        //so if you move your mouse to the left it is (-1, 0, ))
        //if you move your mouse to the right(1, 0, 0)
        //if you move your mouse up (0, 1, 0)
        //if you move your mouse down (0, -1, 0)
        //all inputvalye.get vector 2 is doing is STORING the 
        // movement of the mouse inside our vector2 look input
        lookInput = inputValue.Get<Vector2>();
    }

    // Update is called once per frame
    //fixed update is called at scheduled intervals
    //we used fixed update for PHYSICS
    //we used fixedupdate bc its the same across all computers
    //instead of update bc computers can have different frame rates where movement looks diferent
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
