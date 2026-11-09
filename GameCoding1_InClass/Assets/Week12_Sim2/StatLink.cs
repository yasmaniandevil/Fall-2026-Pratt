using UnityEngine;

public class StatLink : MonoBehaviour
{
    /*the purpose of this script connects two stats so one affects the other!
    ex: when you're hungry you get tired faster
    if hunger drops below a certain point, energy starts to drain faster!
    */

    //the stat that does the affecting
    public StatMeter hunger;

    //the stat that gets affected
    public StatMeter energy;

    //when hunger is below this number, the player counts as "hungry"
    public float hungryPoint = 30f;

    //how much faster energy drains when hungry (2 means twice as fast)
    public float hungryMultiplier = 1f;

    //we got to save the energys normal drain rate so we can go back to it
    private float normalDrainRate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //remeber what the drain rate was when we started
        normalDrainRate = energy.drainRate;
    }

    // Update is called once per frame
    void Update()
    {
        //get value is our function that returns the value of the meter
        //so while playing if the meter is at 50
        //if 50 < 30
        //because get value is constantly updating and returning the current value of the stat meter
        //so after we eat, getvalue can be 100
        //100 < 30 wont run
        //got to check this every frame so thats why we are in update
        if(hunger.GetValue() < hungryPoint)
        {
            //hungry now will drain faster because we are at the hungry point
            //great example of time.deltaTime usage because it is happening over time!
            energy.drainRate = normalDrainRate * hungryMultiplier;
        }
        else
        {
            //if we are not at the hungry point (30)
            energy.drainRate = normalDrainRate;
        }
    }
}
