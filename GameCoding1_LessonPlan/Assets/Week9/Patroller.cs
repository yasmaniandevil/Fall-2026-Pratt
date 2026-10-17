using UnityEngine;

public class Patroller : Enemy
{
    public float turnTime = 5; //how long before it turns around
    private float timer = 0;
    private int direction = 1; //1 = right, -1 = left
    private Color turnColor = Color.black;
   
    protected override void Move()
    { 
        /*Rider prefers you multiply the plain numbers together first and the Vector2 last
         because multiplying a vector costs a little more than multiplying a number. 
         It doesn’t matter at this scale, so it’s safe to ignore*/
        //transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);
        
        transform.Translate( Vector2.right * (direction * moveSpeed * Time.deltaTime));

        //timer starts at 0 and counts up until it hits turnTime
        timer += Time.deltaTime;
        if (timer >= turnTime)
        {
            direction *= -1; //flip direction
            timer = 0;
            if (direction == -1)
            {
                spriteRendy.color = turnColor;
            }
            else
            {
                spriteRendy.color = enemyColor;
            }
            /*Before the ? is the condition
               Between ? and : is the value if it’s true
               After : is the value if it’s false*/
            //spriteRendy.color = (direction == -1) ? turnColor : enemyColor;
        }
    }
}