using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
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

    public GameObject uiHolder;
    private Score scoreScript;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
        if (bulletPrefab == null)
        {
            Debug.Log("bulletPrefab is not assigned on " + gameObject.name);
            return;
        }
        scoreScript = uiHolder.GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        
        if (isGrounded && context.performed)
        {
           
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
    
    
    public void OnSpawnBullet(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
       
        Vector3 offset = new Vector3(1, 0, 0);
        Instantiate(bulletPrefab, transform.position + offset, bulletPrefab.transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
         
            scoreScript = uiHolder.GetComponent<Score>();
            scoreScript.AddScore(1);
            Debug.Log("Coin collected");
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            scoreScript.AddScore(-1);
        }
    }
}
