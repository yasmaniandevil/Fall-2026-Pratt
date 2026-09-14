using UnityEngine;
using UnityEngine.InputSystem;

public class playerEvents : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    public Transform groundCheck;
    private float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb2d;
    private Vector2 moveInput;
    public bool isGrounded;
    private bool jumpPressed;
    
    public GameObject bulletPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (isGrounded)
        {
            Debug.Log("omfg");
            jumpPressed = true;
        }
    }
    
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        rb2d.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb2d.linearVelocity.y);

        if (jumpPressed)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
            jumpPressed = false;
        }
    }
    
    
    void OnSpawnBullet()
    {
        Vector3 offset = new Vector3(1, 0, 0);
        Instantiate(bulletPrefab, transform.position + offset, bulletPrefab.transform.rotation);
    }
}
