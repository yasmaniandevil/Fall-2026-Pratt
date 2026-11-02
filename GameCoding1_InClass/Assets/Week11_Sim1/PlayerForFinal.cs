using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerForFinal : MonoBehaviour
{
    public float moveSpeed = 5;
    private Vector2 moveInput;   
    private Rigidbody2D rb2d;

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        rb2d.linearVelocity = moveInput * moveSpeed;
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    
    
}
