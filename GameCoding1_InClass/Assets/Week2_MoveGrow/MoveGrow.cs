using UnityEngine;

public class MoveGrow : MonoBehaviour
{
    //how far the obj can go
    private int minBoundary = -8;
    private int maxBoundary = 8;
    public float speed = 5; //how fast the obj moves
    private int direction = 1;//the direction of the obj

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this is going to grab the transform
        //of the script its on
        //+= is an operator
        //it means to add and equal it back out to update it
        //vector 3 takes (x, y, z)
        //i only want to move the square on the x
        //i dont want to do anything with y and z
        transform.position += new Vector3(speed * direction * Time.deltaTime, 0, 0);

        //if(this condition is met){then do this task}
        //if(its past the max or min boundary)
        //then send me a message and change the direction
        if(transform.position.x > maxBoundary)
        {
            // a message to ourselves! to see in the console
            //so i know its working
            //Debug.Log("hit boundary");
            //flips it back and forth from -1 to 1
            //claming just means giving the obj a definite value 
            //transform.position = 8, y, z
            transform.position = new Vector3(maxBoundary, transform.position.y, transform.position.z); //clamping to boundary
            direction *= -1;
            //Debug.Log(direction);
        }
        else if(transform.position.x < minBoundary)
        {
            transform.position = new Vector3(minBoundary, transform.position.y, transform.position.z);//clamping to the boundary
            direction *= -1;

            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("entered trigger");
        //when it enters the trigger the square will get larger in scale
        transform.localScale = new Vector3(3, 3, 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("I collided with: " + collision.gameObject.name);

    }
}
