using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /*[SerializeField] private InputAction playermovement;
    float speed = 5;

    Vector2 look = new Vector2();*/
    Mouse mouse = Mouse.current;
    Keyboard keyboard = Keyboard.current;
    private Camera mainCam;
    private Vector3 movementDirection;
    public float movementSpeed = 3f;
    public float turnSpeed = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       /* if (mouse != null)
        {
            //look = playermovement.ReadValue<Vector2>();
            //transform.Translate(new Vector3(look.x, 0, look.y) * speed * Time.deltaTime);
            //look = playermovement.ReadValue<Vector2>();
            Vector2 mouseDelta = mouse.delta.ReadValue();
            transform.Translate(new Vector3(mouseDelta.x, 0, mouseDelta.y) * 5f * Time.deltaTime);
        }

        if(keyboard.aKey.wasPressedThisFrame)
        {
            //transform.position += Vector3.left * 5f * Time.deltaTime;
        }

        float w = keyboard.wKey.ReadValue();
        {

            //transform.position += Vector3.forward * 5f * Time.deltaTime;
            transform.position += transform.forward * 5f * Time.deltaTime;
        }*/

       Vector3 cameraDirection(Vector3 moveDir)
       {

            var cameraForward = mainCam.transform.forward;
            var cameraRight = mainCam.transform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;

            return cameraForward * movementDirection.z + cameraRight * movementDirection.x;

       }


    }
}
