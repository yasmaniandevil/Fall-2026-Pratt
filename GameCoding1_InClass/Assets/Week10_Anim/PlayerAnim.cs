using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnim : MonoBehaviour
{
    public float moveSpeed = 5;
    private Vector2 moveInput;
    private Rigidbody2D rb2d;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //our movement code
        rb2d.linearVelocity = moveInput * moveSpeed;
        //Debug.Log("moving player");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //when this function is being performed is walking is true
        animator.SetBool("isWalking", true);

        //when we stop performing the key press
        if (context.canceled)
        {
            //set is walking to false
            animator.SetBool("isWalking", false);
            //update lastinputX with our moveinput
            animator.SetFloat("lastInputX", moveInput.x);
            animator.SetFloat("lastInputY", moveInput.y);
        }
        moveInput = context.ReadValue<Vector2>();
        //and when we are moving wre will be able to grab our moveinput x and y
        animator.SetFloat("currentInputX", moveInput.x);
        animator.SetFloat("currentInputY", moveInput.y);
    }
}
