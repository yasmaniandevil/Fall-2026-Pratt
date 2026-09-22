using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    //how long we want our timer for
    public float timerTime = 60f;
    public TextMeshProUGUI timerText; //this is the text box where our timer will be in
    private int score; //this is the actual score amount
    public TextMeshProUGUI scoreUI; //this is the visual display of the score
    public GameObject winImage; //the image that pops up once we win the game! 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreUI.text = "Score: ";
    }

    // Update is called once per frame
    void Update()
    {
        /*//if we collect a coin add to the score +1
        //this is us calling the function
       AddScore(1); 
       //if we collect a bigger coin
       AddScore(2);*/

       timerTime -= Time.deltaTime; //if timerTime is 60 and we press play and two seconds has passed then we subtract and its 58
       //if the timer hits 0 (from 60)
       if(timerTime <= 0)
        {
            timerTime = 0;
            Time.timeScale = 0; //this pauses the game 1 plays it
            timerText.text = "WINNER"; //change the text
            winImage.SetActive(true); //turn on our win image
        }
        else if(timerTime > 0) //if the timer is greater than 0 (we are still playing)
        {
            //then print out the timer
            //this is us calling our function and we are telling it to pass in timerTime which is 60
            UpdateTimerText(timerTime);
        }
    }

    //this is a function we are creating, it is simliar to update and start but those are unitys
    //we can create our own
    //we do this to create chunks of code that we can reuse and call in other places
    public void AddScore(int amt)
    {
        //adds the amount to the score
        score += amt;
        //display the score
        scoreUI.text = "Score: " + score.ToString(); //turns the int into a string
    }

    private void UpdateTimerText(float secondsLeft)
    {
        int minutes = Mathf.FloorToInt(secondsLeft / 60);
        int seconds = Mathf.FloorToInt(secondsLeft % 60f);
        //display the above in our text
        timerText.text = minutes.ToString("0") + ":" + seconds.ToString("00");  // 0:00
    }
}
