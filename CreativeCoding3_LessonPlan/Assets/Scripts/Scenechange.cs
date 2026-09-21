using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenechange : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //make a function below 
        if (gameObject.activeInHierarchy == true)
        {
            Debug.Log("Scenechange Start");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
        else
        {
            Debug.Log("Scenechange false");
            Cursor.visible = false;
        }
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log(Cursor.visible);
        SceneManager.LoadScene(sceneName);
    }
    
}
