using UnityEngine;
using UnityEngine.SceneManagement;  
using TMPro;                       

// DayClock: counts up through the day and shows it as clock text ("9:00 AM").
// When the day is over, the player made it, so we load the next scene.
// Real life comparison: a work shift. You clock in at 9, you clock out at 5,
// and when 5 hits you go home no matter what.
public class DayClock : MonoBehaviour
{

    public float dayLength = 180f;      // how many REAL seconds the whole day lasts (180 = 3 minutes)
    public int startHour = 9;           // in game time the day starts (24 hour time, 9 = 9 AM)
    public int endHour = 17;            // in game time the day ends (17 = 5 PM)
    public string nextSceneName;  // must match the scene name EXACTLY
    public TextMeshProUGUI clockText;   // drag your clock text here

    // How many real seconds have passed since the day started.
    float timer = 0f;
    

    // Update runs every frame.
    void Update()
    {
        // Count up in real seconds. Same deltaTime idea as StatMeter:
        // adding deltaTime every frame adds up to exactly 1 per second.
        timer += Time.deltaTime;

        // Show the time on screen.
        clockText.text = GetClockText();

        // Did the day end? The player survived, so move on.
        if (timer >= dayLength)
        {
            SceneManager.LoadScene(nextSceneName); //or a string
            Debug.Log("change scene");
        }
    }
    
    //The string version is like asking someone the time,
    //and the void version is like asking them to write the time on the whiteboard.

    // Turns our timer into clock text like "9:00 AM" or "2:30 PM".
    string GetClockText()
    {
        // How far through the day are we? 0 = just started, 0.5 = halfway, 1 = done.
        float progress = timer / dayLength;

        // Turn that into in game hours.
        // Example: 9 AM to 5 PM is 8 hours. Halfway (0.5) is 9 + 4 = 13, which is 1 PM.
        float gameHours = startHour + progress * (endHour - startHour);

        // Split it into whole hours and minutes.
        // (int) chops off the decimal: 13.5 becomes 13.
        int hour = (int)gameHours;
        // The leftover decimal is part of an hour: 0.5 of an hour is 30 minutes.
        int minute = (int)((gameHours - hour) * 60);

        // Figure out AM or PM.
        string amPm = "AM";
        if (hour >= 12)
        {
            amPm = "PM";
        }

        // Turn 24 hour time into 12 hour time. % gives the remainder: 13 % 12 = 1.
        int displayHour = hour % 12;
        if (displayHour == 0)
        {
            displayHour = 12;  // noon and midnight show as 12, not 0
        }

        // ToString("00") always shows two digits, so 5 minutes shows as "05".
        return displayHour + ":" + minute.ToString("00") + " " + amPm;
    }
    
    /*// Turns our timer into clock text like "9:00 AM" and puts it on screen.
       // void means this method doesn't hand anything back. It just does its job.
       void UpdateClockText()
       {
           // How far through the day are we? 0 = just started, 0.5 = halfway, 1 = done.
           float progress = timer / dayLength;
       
           // Turn that into in game hours.
           // Example: 9 AM to 5 PM is 8 hours. Halfway (0.5) is 9 + 4 = 13, which is 1 PM.
           float gameHours = startHour + progress * (endHour - startHour);
       
           // Split it into whole hours and minutes.
           // (int) chops off the decimal: 13.5 becomes 13.
           int hour = (int)gameHours;
           // The leftover decimal is part of an hour: 0.5 of an hour is 30 minutes.
           int minute = (int)((gameHours - hour) * 60);
       
           // Figure out AM or PM.
           string amPm = "AM";
           if (hour >= 12)
           {
               amPm = "PM";
           }
       
           // Turn 24 hour time into 12 hour time. % gives the remainder: 13 % 12 = 1.
           int displayHour = hour % 12;
           if (displayHour == 0)
           {
               displayHour = 12;  // noon and midnight show as 12, not 0
           }
       
           // Put it on screen. ToString("00") always shows two digits, so 5 minutes shows as "05".
           clockText.text = displayHour + ":" + minute.ToString("00") + " " + amPm;
       }*/
}
