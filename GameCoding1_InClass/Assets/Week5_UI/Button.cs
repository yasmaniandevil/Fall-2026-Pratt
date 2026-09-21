using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public GameObject image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //calling functions in order for functions to run they must be CALLED somewhere
        //we will not call them in this script, we will call them in the button's events in the inspector
        //LoadScene("Week4_Spawn");
        //Toggle();
    }

    //string scenename is an argument or parameter that gets passed into our function
    //when we call the function we can then write the actual scenename into it
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //we made up this function called toggle
    public void Toggle()
    {
        //if we image is active in the heirachy (check box on)f
        //if it is active turn it off, if it isnt turn it on
        if (image.activeInHierarchy)
        {
            image.SetActive(false);
        }
        else
        {
            image.SetActive(true);
        }
    }
}
