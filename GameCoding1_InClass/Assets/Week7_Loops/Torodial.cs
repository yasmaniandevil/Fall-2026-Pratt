using UnityEngine;

public class Torodial : MonoBehaviour
{
    //this is a script for "torodial" wall wrapping, which makes it look like the game edges loop
    //to achieve this we need to teleport our player to the opposite wall when they touch a wall
    //manually assign the opposite wall in the inspector
    public GameObject oppositeWall;
    public bool vertical;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "PlayerOne" || collision.gameObject.name == "PlayerTwo")
        {
            //we need to get the bounds (size) of the collider attached, which we'll need to later place our player
            Vector2 colliderSize = collision.gameObject.GetComponent<Collider2D>().bounds.size;

            //we need to make an int that we use later in the code to place our player correctly, it defaults to positive
            int positiveOrNegative = 1;

            //if the walls are horizontal
            if(vertical == false)
            {
                //check if the opposite wall is the "up" wall (has positive y axis)
                if(oppositeWall.transform.position.y > 0)
                {
                    positiveOrNegative = -1;
                }

                //we have our player on the opposite wall
                //we do this by keeping their x and z the same
                //and changing the transform only on the y
                collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x,
                    oppositeWall.transform.position.y + (colliderSize.y + 0.1f) * positiveOrNegative,
                    collision.gameObject.transform.position.z);
            }else if(vertical == true)
            {
                //so if vertical == true we do the same process but we are changing the x instead of the y
                if(oppositeWall.transform.position.x > 0)
                {
                    positiveOrNegative = -1;
                }
                collision.gameObject.transform.position =
                    new Vector3(oppositeWall.transform.position.x +(colliderSize.x + 0.1f) * positiveOrNegative,
                    collision.gameObject.transform.position.y,
                    collision.gameObject.transform.position.z);
            }
        }
    }
}
