using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColorChanger : MonoBehaviour
{
    
 
    public Color[] colors; //this is an array an array is a collection of values we are making a collection of colors
    //there are arrays and lists, arrays are usually fixed amount of values in the collection while lists can be added onto
    private int index = 0; //so we need a index to keep track of where we are in the array
    private SpriteRenderer spriteRendy;//we need the sprite renderer to access the color to change it

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //grabbing the sprite renderer
        spriteRendy = GetComponent<SpriteRenderer>();
        //this is optional, its for when the game starts
        spriteRendy.color = colors[index];//when it starts it will change the sprite to the color at the 0 index
    }

    // Update is called once per frame
    void Update()
    {
        //if the e key was pressed this frame
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            //add one to the index everytime we press the e key
            index++;
            //colors.length means how many things are in our array
            //if you have 8 colors in your array, colors.length is 8 and last index is 7
            //if the index = 7 then we have to loop back to the beginning of the colors bc we ran out
            if(index >= colors.Length)
            {
                index = 0; //loops back around to the beginning
            }
            //accessing the color property on the sprite renderer
            //we need to do this in update so unity is checking every frame for our input
            //and updating the index
            //so lets say the index is at 1 which is the color red, colors[1] and whatever the color is at that spot on the list
            //it will change to
            spriteRendy.color = colors[index];
            int randomColors = Random.Range(0, colors.Length);
            spriteRendy.color = colors[randomColors];
            

        }
    }
}
