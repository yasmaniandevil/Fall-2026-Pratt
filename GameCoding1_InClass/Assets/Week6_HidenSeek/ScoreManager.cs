using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI caughtText;
    public Transform playerOne; //we need their positions
    public Transform playerTwo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //caughtText.gameObject.SetActive(false);
        caughtText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //calculating the distance between the two objects
        float distance = Vector2.Distance(playerOne.position, playerTwo.position);
        if(distance <= 2) //if the distance is less than or equal to 2
        {
            caughtText.enabled=true; //turn on the text
            caughtText.text = "Caught"; //change the text
        }
        else //if its not true continue to keep the text off
        {
            caughtText.enabled = false;
        }
    }
}
