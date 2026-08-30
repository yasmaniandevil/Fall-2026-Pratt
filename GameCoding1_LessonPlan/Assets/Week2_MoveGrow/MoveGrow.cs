using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGrow : MonoBehaviour
{
    private int minBoundary = -8;
    private int maxBoundary = 8;
    public float speed = 5;
    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //+= operator constantly adding and equaling back out
        transform.position += new Vector3(speed * direction * Time.deltaTime, 0, 0);

        //less than is an operator
        if(transform.position.x > maxBoundary || transform.position.x < minBoundary)
        {
            //Debug.Log("hit boundary");
            direction *= -1;
        }

        //easy rotate
        //triangleTransform.Rotate(0, 0, 90);
        //fancier rotation
        //can make a rotation speed variable
        //transform.Rotate(Vector3.forward * 20 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //first do simple debug that just sends a msg
        Debug.Log("I Hit: " + collision.name);
        gameObject.transform.localScale = new Vector3(3, 3, 3);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("I Hit: " + collision.gameObject.name);
        
        //can do something like if collided with object shrink back down
        if(collision.gameObject.name == "Circle")
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            //add force for it to bounce
            //change the color etc
        }
    }
}
