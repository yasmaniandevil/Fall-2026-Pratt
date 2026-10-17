using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClass : MonoBehaviour
{
    //you dont have to get rid of your player script you can start new ones if youre worried about messing things up
    
    //public can be accessed by any script if connected
    //private only for this script
    //protected = child classes can use it too but can not be public
    //[serializefield] lets us see it in the inspector without being able to accessed by unrealted scripts but any child can access it
    //[SerializeField] protected float moveSpeed = 5; 
    public float moveSpeed = 5;
    Vector2 moveInput;   
    Rigidbody2D rb2d;

    protected virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    
    //both players point their Player Input event here instead of OnWASD/OnArrows
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    
    void FixedUpdate()
    {
        rb2d.linearVelocity = moveInput * moveSpeed;
    }



    public void OnAbility(InputAction.CallbackContext context)
    {
        if (context.performed) UseAbility();
    }
    
    public void OnAbilityTwo(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) AbilityTwo();
    }
    
    

    
    //virtual = child classes are allowed to replace this method
    protected virtual void UseAbility() { }
    protected virtual void AbilityTwo() { }
    
}
