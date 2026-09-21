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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //print this out in start
        scoreUI.text = "Score: ";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int amt)
    {
        //add the amount we are adding by to the score
        score += amt;
        //display the score
        scoreUI.text = "Score: " + score.ToString();// we take our int score and turn it into a string
    }
}
