using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    private int dialogueLine = 0;
    public TextMeshProUGUI dialogueText;

    private void Start()
    {
        //when we start playing if we dont put something in the text box or change the text on start then it will just say New Text until we hit space
        //then changes to case 0
        dialogueText.text = "Hello";
    }

    // Update is called once per frame
    void Update()
    {
        //everytime we hit space it adds one and changes the dialogue line
        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            //calling our function
            NextDialogueLine();
        }*/
        
        

    }

    //switch statment provides a structeeed way to execute code based on the value of a single variable
    //this is an alternative to writing multiple if statements
    private void ShowDialogueLine()
    {
        switch(dialogueLine)
        {
            case 0:
                dialogueText.text = "how are you?";
                break;
            case 1:
                dialogueText.text = "What is up mi friend";
                break;
            case 2:
                dialogueText.text = "Nothing much";
                break;
            case 3:
                dialogueText.text = "hush hush";
                break;
        }

        //this is how we would write it if it was if statements
        /*if(dialogueLine == 0)
        {
            dialogueText.text = "Hello";
        }
        if(dialogueLine == 1)
        {
            dialogueText.text = "something";
        }*/
    }



    private void NextDialogueLine()
    {
        //++ means it adds 1
        //dialogueline is -1 at start
        //then we add 1 which equals 0
        //then we add 1 again which equals 1
        //then we add 1 again which equals 2
        dialogueLine++;
        ShowDialogueLine();
    }

    public void OnHandleDialogue(InputAction.CallbackContext context)
    {
        //before i put  if context.performed it would cycle through all the text quickly
        if (context.performed)
        {
            NextDialogueLine();
            Debug.Log("called next dialogue");
            
        }
    }
}
