using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    //if the audio is not on this gameobject make this public and drag the game object with audio in it
    private AudioSource gameAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
    }


    //if you want on trigger
    //MAKE SURE YOUR PLAYER HAS A TAGGED SPELLED WITH SAME CAPITALS AND LOWERCASE "Player"
    //make sure trigger is checked off on the collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameAudio.Play();
        }
    }

    //if you want on collision just choose one and delete or comment the other one out
    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameAudio.Play();
        }
    }*/
}
