using UnityEngine;
using UnityEngine.Events;

// REQUIRES ITEM
// Put this on anything that needs an item before something happens.
// The object needs a Collider so the player can find it.
//
// EXAMPLES:
// Locked door:   needs a key, says "It's locked" without it, door disappears with it
// Locked pickup: needs the book before you can take the key (Item To Give = key)
// Quest NPC:     "Have you seen my cat?" without it, "You found her!" with it
// Chest:         needs a key, turns on a hidden treasure object with it
//
// HOW "ON SUCCESS" WORKS:
// It's the same box a UI Button uses for On Click. Click +, drag in any object
// from the Hierarchy, then pick what happens from the dropdown:
//   GameObject > SetActive (checked = turn on, unchecked = turn off)
//   AudioSource > Play
//   Animator > SetTrigger
// You can add as many as you want. They all happen when the player has the item.

public class RequiresItemInteractable : Interactable
{
    [Header("What item does this need?")]
    public ItemData requiredItem;

    [Tooltip("Takes the required item out of the inventory when it's used, like using up the book")]
    public bool removeItemWhenUsed = true;

    [Header("What gets added to the inventory? (optional)")]
    [Tooltip("Gets added to the inventory when it works, like the key. Leave empty for doors, NPCs, etc.")]
    public ItemData itemToAdd;

    [Tooltip("Removes this object from the world when it works, like picking up the key")]
    public bool disappearAfterSuccess = false;

    [Header("Dialogue (all optional)")]
    [Tooltip("Shows when the player DOESN'T have the item")]
    public DialogueData dialogueWithoutItem;

    [Tooltip("Shows when the player DOES have the item")]
    public DialogueData dialogueWithItem;

    [Tooltip("Shows if the player interacts again after it already worked")]
    public DialogueData dialogueAfterComplete;

    [Header("What happens when the player has the item?")]
    public UnityEvent onSuccess;

    //remembers if this already worked, so it only happens once
    private bool isDone = false;

    public override void Interact(PlayerInteract player)
    {
        //ALREADY DONE: don't do it again
        if (isDone)
        {
            if (dialogueAfterComplete != null) player.RequestDialogue(dialogueAfterComplete);
            return;
        }

        if (requiredItem == null)
        {
            Debug.LogWarning(gameObject.name + " has no Required Item. Drag an Item Data asset in.");
            return;
        }

        //grab the Inventory script off the player
        Inventory inventory = player.GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.LogWarning(gameObject.name + " needs an item, but your Player has no Inventory script. Add one to the Player.");
            return;
        }

        //DO THEY HAVE THE ITEM?
        if (inventory.HasItem(requiredItem))
        {
            isDone = true;
            Debug.Log("Used " + requiredItem.itemName + " on " + gameObject.name);

            //1. take the required item away (if checked)
            if (removeItemWhenUsed) inventory.RemoveItem(requiredItem);

            //2. give the new item (if there is one)
            if (itemToAdd != null) inventory.AddItem(itemToAdd);

            //3. show dialogue
            if (dialogueWithItem != null) player.RequestDialogue(dialogueWithItem);

            //4. run everything in the On Success box
            onSuccess.Invoke();

            //5. disappear (if checked)
            if (disappearAfterSuccess) Destroy(gameObject);
        }
        else
        {
            if (dialogueWithoutItem != null)
            {
                player.RequestDialogue(dialogueWithoutItem);
            }
            else
            {
                Debug.Log("You need: " + requiredItem.itemName);
            }
        }
    }
}