using TMPro;
using UnityEngine;
using UnityEngine.Events;

// COLLECTION GOAL
// "Collect 5 crystals and something happens."
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
    [Header("What do they need to collect?")]
    [Tooltip("Leave this empty to count ANY item they pick up")]
    public ItemData itemToCollect;

    public int amountNeeded = 5;

    [Header("Optional")]
    [Tooltip("A TextMeshPro text that shows progress, like 'Crystals: 2/5'")]
    public TextMeshProUGUI progressText;

    [Tooltip("The word before the numbers in the progress text")]
    public string progressLabel = "Collected";

    [Tooltip("Shows when they collect them all")]
    public DialogueData completeDialogue;

    [Header("What happens when they collect them all?")]
    public UnityEvent onComplete;

    private int collected = 0;
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
        UpdateText();
    }

    //runs every time the player picks up ANY item
    void ItemAdded(ItemData item)
    {
        if (isComplete) return;

        //if we're looking for a specific item and this isn't it, ignore it
        if (itemToCollect != null && item != itemToCollect) return;

        collected++;
        UpdateText();

        if (collected >= amountNeeded)
        {
            Complete();
        }
    }

    void Complete()
    {
        isComplete = true;
        Debug.Log("Goal complete! Collected " + collected + "/" + amountNeeded);

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
        progressText.text = progressLabel + ": " + collected + "/" + amountNeeded;
    }
}
