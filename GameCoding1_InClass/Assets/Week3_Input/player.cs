using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    //initalized and set the variable
    public float speed = 5f;
    public float jumpForce = 5f;

    //we have this component on our player
    //we need to access it, in order to change it
    private Rigidbody2D rb2d;
    //vector 2 means x and y (x,y)
    private Vector2 moveInput;
    //bool is either true or false
    //tracks if we should jump or not, we have to be on the platform to jump
    public bool isGrounded;
    //this tracks if we pressed space to jump, true means yes we did false means we didnt
    private bool jumpPressed;


    //the player has to be on the ground layer which we assign to the platform in inspector
    public LayerMask groundLayer;
    //a little transform that acts as players feet, if this touches the layer than they are grounded
    public Transform groundCheck;
    //the feet are in a circle, anything within the circle counts as grounding
    private float groundCheckRadius = 0.2f; 
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //grab the rigidbody off of the player and store it inside this variable
        rb2d = GetComponent<Rigidbody2D>();
    }

    //we create a public function, that unity can find
    //we give it an argument/property of InputValue
    public void OnMove(InputValue value)
    {
        //our vector 2 which is x and y
        //we want it to read our input (the value) and store it inside moveInput
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        //if the player is on the ground (the bool has to turn true)
        if (isGrounded == true)
        {
            //then jump pressed bool is true
            jumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        //the circle is calculating with our radius (.2), the position of our "feet", and if we are on
        //the ground layer
        //if we are then change the bool isGrounded to true, if we aren't change it to false
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //whatever input we are pressing (move input) multiply it by the speed so we move
        //but keep the y whatever the current velocity is, because the y is for jumping
        //assign all of this math to our rigidbody's velocity
        rb2d.linearVelocity = new Vector2(moveInput.x * speed, rb2d.linearVelocity.y);

        //if jumpPressed is true (we hit the space bar) then let us jump
        if (jumpPressed == true)
        {
            //we change the y but the x stays the same
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
            jumpPressed = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("two things hit");
        //looking for something specific
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit enemy");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.CompareTag("Bullet"))
        
    }
}
