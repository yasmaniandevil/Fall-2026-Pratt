using UnityEngine;
using UnityEngine.UI;               
using UnityEngine.SceneManagement; 

// StatMeter: one stat that drains over time (hunger, energy, patience, plant water...)
// Put one of these on an empty GameObject for EACH stat in your game.
// Real life comparison: a phone battery. It drains on its own every second,
// you can plug it in to refill it, and when it hits 0 the phone dies.
public class StatMeter : MonoBehaviour
{

    public string statName = "Hunger";  // just a label so we know which stat this is
    public float maxValue = 100f;       // the stat can never go above this
    public float drainRate = 2f;        // how much the stat drops PER SECOND
    //make sure to turn off interactable
    public Slider slider;               // drag your UI Slider here

    
    
    private float currentValue;
    
    
    void Start()
    {
        // Every level starts with a full stat.
        currentValue = maxValue;

        // Tell the slider what "full" means, so the bar matches our numbers.
        slider.maxValue = maxValue;
        slider.value = currentValue;
    }

    // Update still runs every frame, but deltaTime shrinks or grows each step
    // // so the stat drains the same amount per second on any computer.
    void Update()
    {
        // Drain the stat.
        // drainRate is "per second", but Update runs once per FRAME.
        // Multiplying by Time.deltaTime (the seconds since the last frame)
        // turns "per frame" into "per second", so the drain is the same
        // speed on a slow laptop and a fast gaming PC.
        currentValue -= drainRate * Time.deltaTime;

        // Mathf.Clamp keeps a number between a minimum and a maximum.
        // Here: never below 0, never above maxValue.
        currentValue = Mathf.Clamp(currentValue, 0f, maxValue);

        // Show the new value on the slider.
        slider.value = currentValue;

        // If the stat ran out, the player loses: restart this level.
        if (currentValue <= 0f)
        {
            // buildIndex is this scene's number in the build list,
            // so this reloads whatever level we're currently in.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    public float GetValue()
    {
        return currentValue;
    }
    // Other scripts call this to change the stat.
    // A positive number refills it (eating food: ChangeStat(10)).
    // A negative number takes some away (a plant wilts: ChangeStat(-15)).
    public void ChangeStat(float amount)
    {
        currentValue += amount;
        currentValue = Mathf.Clamp(currentValue, 0f, maxValue);
    }
}
