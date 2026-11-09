using UnityEngine;

/* STAT LINK
Connects two stats so one affects the other.
When you're hungry, you get tired faster:
if hunger drops below a certain point, energy drains faster.*/

public class StatLink : MonoBehaviour
{
    // The stat that does the affecting
    public StatMeter hunger;

    // The stat that gets affected
    public StatMeter energy;

    // When hunger is below this number, the player counts as "hungry"
    public float hungryPoint = 30f;

    // How much faster energy drains when hungry (2 means twice as fast)
    public float hungryMultiplier = 2f;

    // We save energy's normal drain rate so we can go back to it later
    float normalDrainRate;

    // Unity calls Start automatically, once, at the beginning
    void Start()
    {
        // Remember whatever drain rate energy started with
        normalDrainRate = energy.drainRate;
    }

    // Unity calls Update automatically every frame
    void Update()
    {
        // Check hunger every frame and set energy's drain rate to match
        if (hunger.GetValue() < hungryPoint)
        {
            // Hungry: energy drains faster
            energy.drainRate = normalDrainRate * hungryMultiplier;
        }
        else
        {
            // Not hungry: energy goes back to normal
            energy.drainRate = normalDrainRate;
        }
    }
}
