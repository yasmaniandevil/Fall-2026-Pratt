using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneChange : MonoBehaviour
{
   //you can create a button in unity 
   //canvas-> button
   //attach this script to the button
   //on the button there will be an area that says on click
   //click the + sign
   //drag in this script, make sure you drag it from where you just attached it (on the button). DO NOT DRAG SCRIPT FROM ASSETS FOLDER
   //open up the men
   //search for the name of this script-> SceneChange->
   //and in that menu search for LoadScene()
   //write in the name of the scene exactly how it is written
   //make sure the scene is added to build profiles and in correct order
   //file-> build profile-> scenelist-> add

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


}
