using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    //how long we want our timer for
    public float timerTime = 60f;
    //the text box for our timer
    public TextMeshProUGUI timerText;
    //the text box for our score
    public TextMeshProUGUI scoreUI;

    private bool gameOver;
    //the variable that keeps track of our score
    private int score;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreUI.text = "Score: ";
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        //this is going to count down for us reduce the timer by the time that has passed
        timerTime -= Time.deltaTime;

        //if timer is less than or equaL TO 0, 
        // we cap the timer at zero and we change the text to game over
        //we can also do if we run out of time and the score is equal
        if (timerTime <= 0)
        {
            timerTime = 0;
            Time.timeScale = 0f;
            timerText.text = "WINNER";
            gameOver = true;
           
        }
        else if(timerTime >= 0)//iif the games time is greater than 0 (game not over)
        {
            //continue to update the text
            UpdateTimerText(timerTime);
            gameOver = false;
        }
    }

    //making this function public to call in our other scripts 
    //when player collides with something we call AddScore
    public void AddScore(int amt)
    {
        //add the amount we are adding by to the score
        score += amt;
        //display the score
        scoreUI.text = "Score: " + score.ToString();
    }

    void UpdateTimerText(float secondsLeft)
    {
        int minutes = Mathf.FloorToInt(secondsLeft / 60);
        int seconds = Mathf.FloorToInt(secondsLeft % 60f);
        timerText.text = minutes.ToString("0") + ":" + seconds.ToString("00");

    }


}
