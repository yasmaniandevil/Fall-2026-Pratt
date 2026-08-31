using TMPro;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    //private means that it is not accessible in other scripts
    //private also means we do not have access to it in the inspector
    //textmeshprougui is the TYPE of variable i want
    //myText is just the name of the variable
    private TextMeshProUGUI myText;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //we are grabbing the text component from the object, and our variable is storing it
        myText = GetComponent<TextMeshProUGUI>();
        

        //we are doing .text to access the text box in the component, and to change the text
        myText.text = "Hello World";

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}