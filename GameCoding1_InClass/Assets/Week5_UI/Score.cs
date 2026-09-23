using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    //how long we want a timer in our game
    //example: game lasts for 60 seconds
    public float timerTime = 60f;
    //textbox for our timer
    public TextMeshProUGUI timerText;
    //text for our score
    public TextMeshProUGUI scoreUI;
    //int of our score
    private int score;
    //public GameObject image; //image is old unity text and the easiest thing is to just consider it a game object

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //print this out in start
        scoreUI.text = "Score: ";
        
    }

    // Update is called once per frame
    void Update()
    {
        //time.delta time starts up when we press play
        //it counts up 1 seconds, 2 second, 3 second etc
        //then we subtract how much time has passed from our variable (which is set to 60s)
        //so if 3seconds has passed then timerTime = 57seconds
        //this makes the timer go down
        timerTime -= Time.deltaTime;
        if(timerTime <= 0)
        {
            //this is just incase it goes past 0  (into the negavites)
            //so we manually set it
            timerTime = 0;
            Time.timeScale = 0; //this pauses the game if it was 1 it would play the game
            timerText.text = "WINNER"; //change from 0 to the word winner
            //image.SetActive(true);
        }else if(timerTime > 0) //if timerTime is greater than 0
        {
            //continue to call the function that updates the timer
            UpdateTimerText(timerTime);
        }

    }

    public void AddScore(int amt)
    {
        //add the amount we are adding by to the score
        score += amt;
        //display the score
        scoreUI.text = "Score: " + score.ToString();// we take our int score and turn it into a string
    }

    void UpdateTimerText(float secondsLeft)
    {
        //we made a local var called minutes
        //mathf.floortoint turning it into an int because time will be like 3.46548837847384
        int minutes = Mathf.FloorToInt(secondsLeft / 60);
        int seconds = Mathf.FloorToInt(secondsLeft % 60);
        //we are turning the ints to string because we need it for our text box
        //text box will only accept strings not ints
        //5.2498942.ToString (convert it to s tring) but FORMAT IT as 5
        //seconds could be like 364782648 convert it to a string and the format is 00
        timerText.text = minutes.ToString("0") + ":" + seconds.ToString("00"); //is it will print out the time like 0.00
    }
}
