using UnityEngine;

// PICKUP
// Put this on any 3D object you want the player to pick up.
// The object needs a Collider so the player's look ray can hit it.
//
// Look at the object (reticle turns red), press Interact,
// and it does whatever boxes you checked in the Inspector.
//
// EXAMPLES:
// Normal item:        Add To Inventory ON,  Show Dialogue OFF
// Item with a note:   Add To Inventory ON,  Show Dialogue ON
// Just read it:       Add To Inventory OFF, Show Dialogue ON, Disappear OFF

public class PickupInteractable : Interactable
{
    [Header("What item is this?")]
    public ItemData item;

    [Header("What happens when you pick it up?")]
    [Tooltip("Adds the item to the player's Inventory. The Player needs the Inventory script.")]
    public bool addToInventory = true;

    [Tooltip("Shows a dialogue box after pickup. Fill in Pickup Dialogue below.")]
    public bool showDialogue = false;

    [Tooltip("Any NPCData asset works here. Put the item's name as the Display Name.")]
    public NPCData pickupDialogue;

    [Tooltip("Removes the object from the world after pickup.")]
    public bool disappearAfterPickup = true;

    //TRY IT: play a sound when you pick this up
    //1. uncomment the line below
    //2. drag a sound file into "Pickup Sound" in the Inspector
    //3. uncomment the matching lines in Interact()
    //public AudioClip pickupSound;

    public override void Interact(AddToPlayerInteract player)
    {
        //1. ADD TO INVENTORY
        if (addToInventory)
        {
            //grab the Inventory script off the player
            Inventory inventory = player.GetComponent<Inventory>();

            if (inventory != null)
            {
                inventory.AddItem(item);
            }
            else
            {
                Debug.LogWarning("Add To Inventory is checked on " + gameObject.name +
                                 ", but your Player has no Inventory script. Add one to the Player.");
            }
        }

        //2. SHOW DIALOGUE
        if (showDialogue)
        {
            if (pickupDialogue != null)
            {
                //same method NPCs use, so it shows up in the same dialogue box
                player.RequestDialogue(pickupDialogue);
            }
            else
            {
                Debug.LogWarning("Show Dialogue is checked on " + gameObject.name +
                                 ", but Pickup Dialogue is empty. Drag in an NPCData asset.");
            }
        }

        //TRY IT: play a sound (see the top of this script)
        //if (pickupSound != null)
        //{
        //    //PlayClipAtPoint keeps playing even after this object is destroyed
        //    AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        //}

        //3. DISAPPEAR
        if (disappearAfterPickup)
        {
            Destroy(gameObject);
        }
    }
}
