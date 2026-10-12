using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// INVENTORY
// Put this on your Player (the same GameObject that has CCPlayer).
// It keeps a list of every item the player has picked up.
//
// OPTIONAL: drag a TextMeshPro text from your Canvas into "Inventory Text"
// to show the list on screen. Leave it empty if you don't want that.
//
// You don't need this script at all if your game has no items.

public class Inventory : MonoBehaviour
{
    //every item we are carrying
    public List<ItemData> items = new List<ItemData>();

    //optional on screen list
    public TextMeshProUGUI inventoryText;

    //ADVANCED (optional): other scripts can listen for this event,
    //like a script that plays a sound every time you pick something up.
    //works the same way as OnDialogueRequested in CCPlayer.
    public static event Action<ItemData> OnItemAdded;

    void Start()
    {
        UpdateText();
    }

    //adds an item to our list
    public void AddItem(ItemData item)
    {
        if (item == null)
        {
            Debug.LogWarning("Tried to add an item, but no ItemData was assigned on the pickup.");
            return;
        }

        items.Add(item);
        Debug.Log("Picked up: " + item.itemName);
        UpdateText();

        //tell anyone listening that we got a new item
        OnItemAdded?.Invoke(item);
    }

    //removes one copy of an item (using up a key, eating food, etc)
    public void RemoveItem(ItemData item)
    {
        if (items.Remove(item))
        {
            UpdateText();
        }
    }

    //returns true if we are carrying this item, false if we aren't
    //
    //example of using this in another interactable script:
    //   Inventory inventory = ccplayer.GetComponent<Inventory>();
    //   if (inventory != null && inventory.HasItem(requiredItem))
    //   {
    //       //open the door!
    //   }
    public bool HasItem(ItemData item)
    {
        return items.Contains(item);
    }

    //returns how many of this item we have (3 apples, 2 coins, etc)
    public int CountItem(ItemData item)
    {
        int count = 0;
        foreach (ItemData i in items)
        {
            if (i == item) count++;
        }
        return count;
    }

    //rebuilds the on screen list
    void UpdateText()
    {
        //if no text was dragged in, skip this
        if (inventoryText == null) return;

        if (items.Count == 0)
        {
            inventoryText.text = "Inventory: empty";
            return;
        }

        string list = "Inventory:";
        //foreach because we just need every item, we don't need its number
        foreach (ItemData item in items)
        {
            if (item != null) list += "\n" + item.itemName;
        }
        inventoryText.text = list;
    }
}
