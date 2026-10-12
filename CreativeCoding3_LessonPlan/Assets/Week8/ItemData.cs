using UnityEngine;

// ITEM DATA
// This is a "recipe card" for an item. It isn't on any GameObject.
// It lives in your Project window as an asset, just like NPCData.
//
// HOW TO MAKE ONE:
// Right click in the Project window > Create > Inventory > Item Data
// Name it (like "OldKey"), then fill in the fields in the Inspector.
//
// Why an asset instead of just typing a name? Because the door, the pickup,
// and the inventory all point at the SAME asset, so there are no typos
// and no "Key (1)" naming problems.

[CreateAssetMenu(menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    //the name that shows up in the inventory list
    public string itemName;

    //optional: a picture of the item, if you make an inventory with icons
    public Sprite icon;

    //optional: a longer description of the item
    [TextArea] public string description;
}
