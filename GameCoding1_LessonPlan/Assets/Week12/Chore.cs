using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class Chore : MonoBehaviour
{
    // How much money the player earns for doing this chore
    public int pay = 5;

    // Is the player standing on the chore right now?
    bool playerInRange = false;

    // The player's wallet. This is a prefab, so we can't drag the player into the
    // Inspector (prefabs can't hold things from the scene). Instead we grab
    // the wallet off the player when they walk into us.
    Wallet playerWallet;

    public TextMeshPro eText;

    // Unity calls this automatically when something walks INTO the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //optional
            eText.enabled = true;
            
            playerInRange = true;

            // Look on the player for a Wallet script and remember it
            playerWallet = other.GetComponent<Wallet>();
        }
    }

    // Unity calls this automatically when something walks OUT of the trigger
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            eText.enabled = false;
            playerInRange = false;
        }
    }

    // Unity calls Update automatically every frame
    void Update()
    {
        // Only do the chore if the player is here AND pressed E this frame
        if (playerInRange && Keyboard.current.eKey.isPressed)
        {
            // AddMoney is a method WE wrote in the Wallet script, so we call it ourselves
            playerWallet.AddMoney(pay);

            // The chore is done, so remove it from the scene.
            // This also frees up a slot in the spawner (one less child).
            Destroy(gameObject);
        }
    }
}
