using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    private Vector3 startPos; //this vector 3 is empty it is 0, 0, 0
    public string functionName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //here we are saying wherever the obj this script is attached to take its position
        //and give it to StartPos
        startPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        SendMessage(functionName);
    }

    void RotateInPlace()
    {
        //just rotates on the y
        transform.Rotate(0, 90 * Time.deltaTime, 0, Space.Self);
        
    }

    void PingPong()
    {
        //moveDir IS OUR VARIABLE!!!
        //its like writing it at the top
        //but here we are immeditely giving it something
        //and we dont write it at the top because we only need it here
        Vector3 moveDir = Vector3.right;
        float moveDistance = 2f;
        float moveSpeed = .5f;

        float t = Mathf.PingPong(Time.time * moveSpeed, 1f);
        Vector3 offset = moveDir.normalized * (t * moveDistance);
        transform.position = startPos + offset;
        
    }
}
