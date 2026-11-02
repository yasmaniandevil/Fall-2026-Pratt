using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem; // coroutines need this (it's where IEnumerator lives)

// RefillStation: stand here, press E, and a stat refills a little at a time.
// Examples: a fridge (hunger), a bed (energy), a watering can (plant water).
// Setup: give this object a Collider2D with "Is Trigger" checked,
// and make sure your player is tagged "Player" and has a Rigidbody2D.
//
// Real life comparison: filling a glass at the tap. It doesn't fill instantly,
// it fills bit by bit, and if you walk away the water stops.
public class RefillStation : MonoBehaviour
{

    public StatMeter statToRefill;       // drag the stat this station refills here
    public float amountPerTick = 5f;     // how much each little refill adds
    public float timeBetweenTicks = 0.25f; // seconds to wait between refills
    public int numberOfTicks = 8;        // how many refills per E press (8 x 5 = 40 total)

    bool playerIsHere = false;   // is the player standing in the trigger?
    bool isRefilling = false;    // is a refill already happening? (stops E spam)
    public TextMeshProUGUI eText;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsHere = true;
            eText.enabled = true;
        }
    }

    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsHere = false;
            eText.enabled = false;
        }
    }

    // Update runs every frame. We only use it to listen for the E key.
    void Update()
    {
        // All three have to be true: player is here, E was pressed, not already refilling.
        if (playerIsHere && Keyboard.current.eKey.isPressed&& !isRefilling)
        {
            // Start the kitchen timer. Update keeps running while this happens.
            StartCoroutine(RefillOverTime());
        }
    }

    

    // This is a COROUTINE: a method that can pause and come back later.
    // Real life comparison: a kitchen timer. You start it, walk away,
    // and it calls you back when the time is up.
    IEnumerator RefillOverTime()
    {
        isRefilling = true;

        // Repeat the refill numberOfTicks times.
        for (int i = 0; i < numberOfTicks; i++)
        {
            // If the player walked away, stop refilling early.
            if (!playerIsHere)
            {
                break;  // break jumps out of the loop
            }

            // Add a little to the stat.
            statToRefill.ChangeStat(amountPerTick);

            // PAUSE this method here for a bit. The rest of the game keeps going.
            // After the wait, Unity comes back and continues from this exact line.
            yield return new WaitForSeconds(timeBetweenTicks);
        }

        // Done, so the player can press E again.
        isRefilling = false;
    }
}
