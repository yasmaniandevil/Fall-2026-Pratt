using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerTwo : MonoBehaviour
{
    Vector2 moveInput;

    private bool jumpPressed;

    private float jumpForce;
    private Rigidbody2D rb2d;
    public float moveSpeed = 5;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
    }

    public void OnArrows(InputAction.CallbackContext ctx)
    {
        
        moveInput = ctx.ReadValue<Vector2>();
    }
    void FixedUpdate()
    {
        rb2d.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        Debug.Log("movement");
    }

    
}
