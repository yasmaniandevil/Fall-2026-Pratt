using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Inventory : MonoBehaviour
{
    //a list that stores all collected keys
    //a list is if you dont know how many you want in a collection, only use it for that otherwise its a waste of resources
    public List<string> collectedKeysList = new List<string>();
    public TextMeshProUGUI inventoryText; //text that tells us whats in our inventory

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            //add it to the list
            //by adding its name
            collectedKeysList.Add(collision.gameObject.name);
            Destroy(collision.gameObject);
            //print it out into a string
            //so we can update our inventory text
            // \n means write it on a new line
            //join all the strings together, write each of them on a new line the strings on our list
            inventoryText.text = "Collected: \n" + string.Join("\n", collectedKeysList);
        }
    }

    //this function checks if the player has a specific key
    //it then returns true or false depending on if they have the correct key
    //when a function doesnt have void that means it has to return something
    //it needs a return
    public bool HasKey(string keyName)
    {
        //.contains checks if our list has that value
        //return collectedKeysList.Contains(keyName);//its going to check for the specific key name and return true or false

        //for debugging its good to do it this way
        //so you can store it in a var and then do a debug.log for that var and see if it prints true or false
        bool result = collectedKeysList.Contains(keyName);
        Debug.Log(result);
        return result;
    }
}
