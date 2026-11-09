using Random = UnityEngine.Random;
using UnityEngine;
using System.Collections;
using TMPro; // coroutines need this

/* RecurringEvent: something happens every few seconds, over and over.
Examples: a plant wilts, a customer walks in, the baby cries.
This one knocks a chunk off a stat each time it happens.*/
public class RecurringEvent : MonoBehaviour
{

    public StatMeter statToAffect;          // which stat gets hit
    public float secondsBetweenEvents = 10f; // how often it happens
    public float amountLost = 15f;           // how much the stat drops each time
    //public string eventMessage = "The plant wilted!";
    
    //this is for the chore loop
    public GameObject chorePrefab;
    
    void Start()
    {
        StartCoroutine(EventLoop());
        StartCoroutine(ChoreLoop());
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
            Debug.Log("stomach sounds");
            statToAffect.ChangeStat(-amountLost);  // negative number takes some away
        }
    }
    
    IEnumerator ChoreLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            Vector2 randomPos = new Vector2(Random.Range(7, -7), Random.Range(3f, -3f));
            Debug.Log("chore drop");
            
            GameObject newChore = Instantiate(chorePrefab, randomPos, Quaternion.identity);
            
            Chore newChoreScript = newChore.GetComponent<Chore>();
            GameObject eText = GameObject.FindWithTag("eText");
            
            if (newChoreScript.eText == null)
            {
                newChoreScript.eText = eText.GetComponent<TextMeshPro>();
            }
        }
        
    }
    
    
}
