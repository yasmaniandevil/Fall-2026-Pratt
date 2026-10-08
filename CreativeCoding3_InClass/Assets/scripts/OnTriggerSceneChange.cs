using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerSceneChange : MonoBehaviour
{
    /*this script is ideal for if you want an object to switch the player from scene to scene instead of a button
    you can attach this script on as many objects as you want to change to different scenes
    
    1. Make sure your player is tagged as Player, in the inspector SAME EXACT WAY
    2. Create an object-> make sure there is a collider on it and you set it to trigger
    3. Attach this script to that game object
    4. On the script there will be two blank fields
    5. For Button Scene Change Script: drag in the object that has the ButtonSceneChange script on it (usually a button)
    6. For Scene Namey: write in the name of the scene you want to switch to make sure it is identically written 
    7.make sure the scene is added to build profiles and in correct order
    8. file-> build profile-> scenelist-> add
     
    IN THE EVENT THAT YOU DO NOT HAVE THE BUTTONSCENECHANGE SCRIPT ANYWHERE IN YOUR SCENE:
    if you do not have it in this scene then we will just use the function from that script directly
    i added it below, take off the comments if you want to use it
    1. Comment out public ButtonSceneChange line below
    Rest of the steps are below
    
    */
    
    public ButtonSceneChange buttonSceneChangeScript;
    public string sceneNamey;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buttonSceneChangeScript.LoadScene(sceneNamey);
            
        }
    }
    
    
    /*THIS IS IN CASE YOU DO NOT HAVE BUTTONSCENECHANGE SCRIPT ON AN OBJECT IN YOUR SCENE
    2.Turn this on AKA remove the comments (its the slash asterick on both sides)
    
     */
    /*public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }*/
    
    /*change the trigger on the inside
    3.YOU CAN ONLY HAVE ONE ONTRIGGERENTER PER SCRIPT
        EITHER UPDATE THE ABOVE AND DELETE THIS OR DELETE 
        THE ABOVE AND TURN THIS ON, WHERE WE CALL THE FUCTION LOADSCENE DIRECTLY
    4.Continue with steps 6, 7, 8 at the top of the script
    */
    
    
    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadScene(sceneNamey);
        }
    }*/
}
