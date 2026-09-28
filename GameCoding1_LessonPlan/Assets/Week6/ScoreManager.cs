using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    
    public TextMeshProUGUI caughtText;

    public Transform playerOneTransform;
    public Transform playerTwoTransform;

    // Start is called before the first frame update
    void Start()
    {
        

        caughtText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(playerOneTransform.transform.position,
            playerTwoTransform.transform.position);

        if(distance <= 2)
        {
            
            caughtText.gameObject.SetActive(true);
            caughtText.text = "Caught";
        }
        else
        {
            caughtText.gameObject.SetActive(false);
        }


    }

   
}
