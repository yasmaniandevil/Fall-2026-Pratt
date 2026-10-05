using System.Collections;
using TMPro;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    //we make a variable out of OUR SCRIPT PLAYERONE
    private PlayerOne playerOne;
    public float boostedSpeed;
    //how long the boost lasts
    public float boostDuration;
    public TextMeshProUGUI countdownBoostText;
    //check if the boost is active
    private bool isBoostActive; //check if they are already boosted

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //first find the gameobject by its name
        //then grab the script
        //you can also do findwithtags
        playerOne = GameObject.Find("PlayerOne").GetComponent<PlayerOne>();
    }

    public void ActiveBoost()
    {
        //if boost is not active aka FALSE
        if (!isBoostActive)
        {
            //we have to call coroutines like how we call functions!
            //but the diff is we have to say start coroutine
            StartCoroutine(Boost());
            Debug.Log("boosting");
        }
    }

    //coroutines are the Ienumerator
    //there is no void so we are saying RETURN SOMETHING
    private IEnumerator Boost()
    {
        isBoostActive = true;
        //save the players original speed so we can reset later
        float originalSpeed = playerOne.moveSpeed;
        //set the new boosted speed
        playerOne.moveSpeed = boostedSpeed;
        //track how much time is left
        float timeLeft = boostDuration;
        //while this condition is still being met, then do the inside
        //when the condition is no longer met dont do it
        while(timeLeft > 0)
        {
            //if the countdown exists
            //if we gave it something in the inspector then do {}
            //if it doesnt exist in the inspector dont do anything
            //this is to prevent null errors
            if(countdownBoostText != null)
            {
                countdownBoostText.text = "Boost: " + Mathf.Ceil(timeLeft);
            }

            yield return null;

            //subtract the time that passed since the last frame
            timeLeft -= Time.deltaTime;
        }

        playerOne.moveSpeed = originalSpeed;

        //check to see if its in inspector
        if(countdownBoostText != null)
        {
            //then change the text
            countdownBoostText.text = "Boost Over";
        }

        isBoostActive = false;
        Debug.Log("boost has finished");
    }
}
