using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    void Update()
    {
        //if the R key was pressed this frame...
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            //reload the scene we're currently in
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
}
