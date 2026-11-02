using UnityEngine;
using System.Collections;  // coroutines need this

// RecurringEvent: something happens every few seconds, over and over.
// Examples: a plant wilts, a customer walks in, the baby cries.
// This one knocks a chunk off a stat each time it happens.
public class RecurringEvent : MonoBehaviour
{

    public StatMeter statToAffect;          // which stat gets hit
    public float secondsBetweenEvents = 10f; // how often it happens
    public float amountLost = 15f;           // how much the stat drops each time
    public string eventMessage = "The plant wilted!";

    
    void Start()
    {
        StartCoroutine(EventLoop());
    }
    

    IEnumerator EventLoop()
    {
        // while (true) means "repeat forever".
        // Normally a forever loop freezes Unity, but this one is safe
        // because yield return pauses it every time around.
        while (true)
        {
            // Wait first, so the event doesn't happen the second the level loads.
            yield return new WaitForSeconds(secondsBetweenEvents);

            // The event happens.
            Debug.Log(eventMessage);
            statToAffect.ChangeStat(-amountLost);  // negative number takes some away
        }
    }
}
