using UnityEngine;
using UnityEngine.InputSystem;

public class Chore : MonoBehaviour
{
    //how much money the player earns for doing this chore
    public int pay = 5;

    //is the player at the chore?
    private bool playerInRange = false;

    //we will grab the wallet off of the player when we walk into chore trigger
    private Wallet playerWallet;

    //you can do another eText thing here like we did with fridge, but it would
    //make more sense just to have one eText and then call it


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //turn on your eText (optional but I think good)
            playerInRange = true;
            //grab wallet off of player
            playerWallet = collision.GetComponent<Wallet>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            //turn off eText
        }
    }

    // Update is called once per frame
    void Update()
    {
        //only do the chore if the player is in range and if they pressed the e key!
        //it is possible that maybe waspressedthisframe
        if(playerInRange && Keyboard.current.eKey.isPressed)
        {
            //we call add money from our wallet script
            playerWallet.AddMoney(pay);

            //then we are done with the chore so destroy it
            //Destroy(gameObject);
        }
    }
}
