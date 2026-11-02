using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayClock : MonoBehaviour
{
    [SerializeField] float dayLength = 180f; //how many REAL seconds the whole day lasts (180=3mins)
    public int startHour = 9; //the time the day starts (24hr clock) 9 = 9am
    public int endHour = 17; //the time the day ends 17 = 5 (subtract 2!)
    public string nextSceneName; //so we can just write it in inspector
    public TextMeshProUGUI clockText;
    private float timer = 0f; //how many real seconds have passed since the day has started
    

    // Update is called once per frame
    void Update()
    {
        //count up in real seconds
        //adding deltaTime every frame adds up to exactly 1 per second
        timer += Time.deltaTime;

        //show the time on the screen
        clockText.text = GetClockText();
        //UpdateClockText();

        //did the day end? the player survived? move on
        if(timer >= dayLength)
        {
            //SceneManager.LoadScene(nextSceneName);
            Debug.Log("day ended move to next scene");
        }
    }

    //we will be returning a string
    //the string version is like asking someone the time
    //if we did a void version it would be like asking them to write the time on the board
    //turn our timer into clock text like 9:00am or 2:30PM
    string GetClockText()
    {
        //how far through the day are we? 0 = just started
        //.5 = halfway
        //1 = done
        float progress = timer / dayLength;

        //turn that time into game hours
        //ex: 9am to 5pm is 8 hours
        //halfway (.5) is 1pm
        float gameHours = startHour + progress * (endHour - startHour);

        //split it into hours and minutes
        //int chops off the decimal
        //so 13.5 becomes 13
        int hour = (int)gameHours;
        //the left over decimal is part of an hour 
        //so .5 is 30mins
        int minute = (int)((gameHours - hour) * 60);

        //figure out AM or PM
        string amPm = "AM";
        if(hour >= 12)
        {
            amPm = "PM";
        }

        //turn 24hoour time into 12 hour time % gives the remandier 13 % 12 = 1
        int displayHour = hour % 12;
        if(displayHour == 0)
        {
            displayHour = 12; //noon and midnight show as 12, not 0
        }

        //we did the math and are returning the time back
        //we arent changing the text box like we usually do with void where we do clocktext.text = this stuff
        return displayHour + ":" + minute.ToString("00") + " " + amPm;

    }
}
