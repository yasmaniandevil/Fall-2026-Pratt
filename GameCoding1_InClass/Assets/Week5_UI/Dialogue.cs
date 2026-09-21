using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    //this keeps track of what dialogue we are currently on
    //this is an index
    private int dialogueLine = 0;
    public TextMeshProUGUI dialogueText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueText.text = "Hello";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this function just changes the text
    private void ShowDialogueLine()
    {
        //this is the basic way to do it as simply as possible
        if(dialogueLine == 0)
        {
            dialogueText.text = "Hello";
        }
        if(dialogueLine == 1)
        {
            dialogueText.text = "Whats up?";
        }
        if(dialogueLine == 2)
        {
            dialogueText.text = "I do not know";
        }
    }
    //this function is responsible for moving us forward to the next line
    //then calling our showdialogue function above
    void NextDialogueLine()
    {
        dialogueLine++; //all this means is dialogueline + 1; 0+1 = 1
        //then the next time it gets called it would be 1+1= 2
        ShowDialogueLine();//after we move up the index then we want to call our function
    }

    //we are making a function that handles calling our function next dialogue line
    //so when we press whatever key it will call it, and move forward
    public void OnHandleDialogue(InputAction.CallbackContext context)
    {
        //we are doing if context.performed bc if we didnt and then press the key all the text would cycle through quickly
        //we used .performed or .cancelled with inputaction.callbackcontext
        //if context.performed means if i pressed the button
        if (context.performed)
        {
            NextDialogueLine();
        }
        //first we press the key
        //it calls nextdialogueline
        //then that function advances the dialogue line by +1
        //then it calls showdialogueline
        //which is responsible for seeing which index we are at and then changing the text
    }
}
