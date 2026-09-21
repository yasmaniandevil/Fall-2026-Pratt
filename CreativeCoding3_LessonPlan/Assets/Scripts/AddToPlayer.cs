using System;
using UnityEngine;

public class AddToPlayer : MonoBehaviour
{
    public GameObject UIHolder;
    private Score scoreScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //access the game object which has the score script attached
        //and just grab the score
        scoreScript = UIHolder.GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //talk about OnExit OnStay etc
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacles"))
        {
            //Debug.Log("hit obstacle");
            scoreScript.AddScore(1);
            Destroy(other.gameObject);
        }
    }

    //for homework they can add something here for when colliding with the sphere
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Sphere"))
        {
            Debug.Log("Hit enemy");
            scoreScript.AddScore(-1);
        }
    }
}
