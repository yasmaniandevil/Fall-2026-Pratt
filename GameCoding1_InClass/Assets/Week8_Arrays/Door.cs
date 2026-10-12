using UnityEngine;

public class Door : MonoBehaviour
{
    //what is the required key name to open the door
    public string requiredKeyName;
    private bool isOpen = false; //is the door open or closed

    //the door is responsible for checking if the player has the key
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //only check if the player touches the door
        if (collision.gameObject.CompareTag("Player"))
        {
            //we grab the script off of the player (which is the collision in this case)
            //Inventory is the name of our script, and inventory is the variable that will store it
            //when the player and door collide, we will grab the inventory script from the player to access our function
            Inventory inventory = collision.gameObject.GetComponent<Inventory>();

            //if we grabbed the inventory script
            //and it has the required key name to open the door
            if(inventory != null  && inventory.HasKey(requiredKeyName))
            {
                Debug.Log("Door Opened With: " + requiredKeyName);
                OpenDoor();
            }
            else
            {
                Debug.Log("You need the " + requiredKeyName + "to open this door");
            }
        }
    }


    //what we want to happen when we open the door
    void OpenDoor()
    {
        //if we have not opened the door
        //if isOpen is false
        // ! means false or do == false
        if (!isOpen)
        {
            isOpen = true;

            //local spriterenderer variable that we grab
            //then change the color
            SpriteRenderer doorColor; //or = Getcomponenet
            doorColor = GetComponent<SpriteRenderer>();
            doorColor.color = Color.white;
        }
    }
}
