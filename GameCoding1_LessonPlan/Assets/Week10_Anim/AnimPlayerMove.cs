using UnityEngine;
using UnityEngine.InputSystem;

public class AnimPlayerMove : MonoBehaviour
{
    public float moveSpeed = 5;
    Vector2 moveInput;   
    Rigidbody2D rb2d;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //Debug.Log("grabbed it");
    }
    
    
    void FixedUpdate()
    {
        rb2d.linearVelocity = moveInput * moveSpeed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //when we are walking set it to true
        animator.SetBool("isWalking", true);
        
        //has it been cancelled
        //then set it to false and store the last input
        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
        moveInput = context.ReadValue<Vector2>();
        //and when we are moving we will be able to grab our moveinput x and y
        animator.SetFloat("currentInputX", moveInput.x);
        animator.SetFloat("currentInputY", moveInput.y);
    }
}
