using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatMeter : MonoBehaviour
{
    public string statName = "Hunger"; //just a label so we know which stat this is
    public float maxValue = 100f; //the stat can never go above this
    public float drainRate = 2f; //how much the stat drops PER SECOND
    public Slider slider;
    private float currentValue; //keeps track of where the stat meter is at

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //every level starts with a full stat
        currentValue = maxValue;
        //tell the slider what full means so the bar matches our numbers
        slider.maxValue = maxValue;
        slider.value = currentValue;
    }

    // Update is called once per frame
    void Update()
    {
        //drain the stat
        //drainRate is per second but update runs once per FRAME
        //mutliplying by time.deltatime (the seconds since the last frame)
        //turns per frame into per second so the drain is the same on a fast gaming pc vs a slow laptop
        currentValue -= drainRate * Time.deltaTime;

        //mathf.clamp keeps a number between a min and a max
        //never below 0 never above 100
        currentValue = Mathf.Clamp(currentValue, 0f, maxValue);
        slider.value = currentValue; //shows the new value on the slider

        //if the stat ran out the player loses restart the level?
        if(currentValue <= 0)
        {
            //this will restart our scene
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        
    }

    //needs to be public for otgher scripts
    //a posi number refills it (changestat(10)
    //a -# takes some away (ex: plant starts to die ChangeStat(-15)
    //inside () is called an argument or parameter
    public void ChangeStat(float amount)
    {
        currentValue += amount;
        currentValue = Mathf.Clamp(currentValue, 0f, maxValue);
    }
}
