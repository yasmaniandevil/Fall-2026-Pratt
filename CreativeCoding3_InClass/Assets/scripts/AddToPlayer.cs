using UnityEngine;

public class AddToPlayer : MonoBehaviour
{
    public GameObject uiHolder;
    //we need a reference to the score script so that we can use AddScore Function
    //so Score below is OUR SCRIPT
    private Score scoreScript;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //so this means UI holder is the GAMEOBJECT that has score script attached to
        //we first need to say the gameobject then GET COMPNENT
        scoreScript = uiHolder.GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //a trigger you can go through
    //if its a coin you want to pass through the coin you dont want it to block you from moving forward
    private void OnTriggerEnter(Collider other)
    {
        //other is the thing that we are hitting(which will be our cubeprefab)
        if (other.CompareTag("CubePrefab"))
        {
            //Debug.Log("hit obstacle");
            //this is our score script, we are accessing our function called add scored
            scoreScript.AddScore(1);
            //after all of that is done then destroy the cubePrefab
            Destroy(other.gameObject);
        }
    }

    //if we collide with a gameobject that has the tag "spherePrefab"
    //we PHYSICALLY collide, physically hit each other
    //then subtract from my score
    //Collision collision is like what we do at the top. Collision is the type of var we want and collision is the name of it, it can be named anything!!!
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("made collision");

        if (collision.gameObject.CompareTag("SpherePrefab"))
        {
            Debug.Log("Detected sphere");
            Debug.Log("hit sphere");
            scoreScript.AddScore(-1);
            Destroy(collision.gameObject);
        }
    }
}
