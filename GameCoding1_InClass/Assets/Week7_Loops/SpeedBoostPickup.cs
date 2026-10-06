using UnityEngine;

public class SpeedBoostPickup : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //speed boost script is going to exist ON THE PLAYER
        //we need to say once we collide grab it OFF THE PLAYER
        //saving it inside our variable
        SpeedBoost speedBoostScript = collision.GetComponent<SpeedBoost>();

        //if we actually grabbed it
        if(speedBoostScript != null)
        {
            //call the active boost function
            speedBoostScript.ActiveBoost();
            //then destroy it!
            Destroy(gameObject); //gameobject = THIS ONE THAT THE SCRIPT IS ATTACHED TO

        }
    }
}
