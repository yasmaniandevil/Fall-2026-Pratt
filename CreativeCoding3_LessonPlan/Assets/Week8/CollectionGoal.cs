using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

// COLLECTION GOAL
// Collect items and something happens.
//
// Pick a Goal Type in the Inspector:
// COLLECT AMOUNT: collect a number of items ("collect 5 crystals")
//                 Leave Item To Collect empty to count ANY item.
// COLLECT LIST:   collect every item in a list ("find the key, the gem AND the feather")
//                 Click + under Items Needed and drag in each Item Data.
//
// Put this on an EMPTY GameObject (right click in Hierarchy > Create Empty, name it "Goal").
// Don't put it on an object that starts turned off, like a hidden portal.
// Turned off objects can't listen, so the goal would never count anything.
// Instead, put it on the empty Goal object and turn the portal on with On Complete.
//
// Your Player needs the Inventory script for this to work.
//
// HOW "ON COMPLETE" WORKS:
// Same as a UI Button's On Click. Click +, drag in any object from the Hierarchy,
// then pick what happens from the dropdown:
//   GameObject > SetActive (checked = turn on, unchecked = turn off)
//   AudioSource > Play
//   Animator > SetTrigger
//
// Heads up: if the LAST item they pick up also has Show Dialogue checked,
// its dialogue will replace the Complete Dialogue. Leave dialogue off on collectibles.

public class CollectionGoal : MonoBehaviour
{
    public enum GoalType { CollectAmount, CollectList }

    [Header("What kind of goal?")]
    [Tooltip("Collect Amount: a number of items. Collect List: every item in a list.")]
    public GoalType goalType = GoalType.CollectAmount;

    [Header("Collect Amount settings")]
    [Tooltip("Leave this empty to count ANY item they pick up")]
    public ItemData itemToCollect;
    public int amountNeeded = 5;

    [Header("Collect List settings")]
    [Tooltip("Every item the player needs. Add each item only once.")]
    public List<ItemData> itemsNeeded = new List<ItemData>();

    [Header("Optional")]
    [Tooltip("A TextMeshPro text that shows progress, like 'Crystals: 2/5'")]
    public TextMeshProUGUI progressText;

    [Tooltip("The word before the numbers in the progress text")]
    public string progressLabel = "Collected";

    [Tooltip("Shows when they collect them all")]
    public DialogueData completeDialogue;

    [Header("What happens when they collect them all?")]
    public UnityEvent onComplete;

    //Collect Amount: how many we've picked up
    private int collected = 0;
    //Collect List: which items from the list we've picked up
    private List<ItemData> collectedFromList = new List<ItemData>();
    private bool isComplete = false;

    //start listening for new items
    private void OnEnable()
    {
        Inventory.OnItemAdded += ItemAdded;
    }

    //stop listening
    private void OnDisable()
    {
        Inventory.OnItemAdded -= ItemAdded;
    }

    void Start()
    {
        if (goalType == GoalType.CollectList)
        {
            if (itemsNeeded.Count == 0)
            {
                Debug.LogWarning(gameObject.name + ": Goal Type is Collect List, but Items Needed is empty. Add items with +.");
            }

            //if the player is already carrying some of the items, count them
            Inventory inventory = FindFirstObjectByType<Inventory>();
            if (inventory != null)
            {
                foreach (ItemData item in itemsNeeded)
                {
                    if (item != null && inventory.HasItem(item) && !collectedFromList.Contains(item))
                    {
                        collectedFromList.Add(item);
                    }
                }
            }
        }

        UpdateText();
        CheckComplete();
    }

    //runs every time the player picks up ANY item
    void ItemAdded(ItemData item)
    {
        if (isComplete) return;

        if (goalType == GoalType.CollectAmount)
        {
            //if we're looking for a specific item and this isn't it, ignore it
            if (itemToCollect != null && item != itemToCollect) return;
            collected++;
        }
        else
        {
            //ignore items that aren't on the list, or that we already counted
            if (!itemsNeeded.Contains(item) || collectedFromList.Contains(item)) return;
            collectedFromList.Add(item);
        }

        UpdateText();
        CheckComplete();
    }

    //how many we have so far
    int GetCollected()
    {
        if (goalType == GoalType.CollectAmount) return collected;
        return collectedFromList.Count;
    }

    //how many we need in total
    int GetNeeded()
    {
        if (goalType == GoalType.CollectAmount) return amountNeeded;
        return itemsNeeded.Count;
    }

    void CheckComplete()
    {
        if (isComplete) return;
        if (GetNeeded() > 0 && GetCollected() >= GetNeeded())
        {
            Complete();
        }
    }

    void Complete()
    {
        isComplete = true;
        Debug.Log("Goal complete! Collected " + GetCollected() + "/" + GetNeeded());

        //find the player so we can show dialogue
        if (completeDialogue != null)
        {
            PlayerInteract player = FindFirstObjectByType<PlayerInteract>();
            if (player != null) player.RequestDialogue(completeDialogue);
        }

        //run everything in the On Complete box
        onComplete.Invoke();
    }

    void UpdateText()
    {
        if (progressText == null) return;
        progressText.text = progressLabel + ": " + GetCollected() + "/" + GetNeeded();
    }
}