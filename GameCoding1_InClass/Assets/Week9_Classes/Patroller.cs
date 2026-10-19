using UnityEngine;

public class Patroller : Enemy
{
    //how long before they turn
    [SerializeField] float turnTime = 5f;
    private float timer = 0;
    //1 is right and -1 is left
    private int direction = 1;
    private Color turnColor = Color.black; //what color are they going to turn to?

    protected override void Move()
    {
        //first we are moving!
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);
        //timer starts at 0 and counts up until it hits turntime
        timer += Time.deltaTime;
        if(timer >= turnTime)
        {
            //flip direction
            direction *= -1;
            timer = 0;
            if(direction == -1)
            {
                spriteRendy.color = turnColor;
            }
            else
            {
                spriteRendy.color = enemyColor;
            }

            //this is shorthand on how to write the above if
            //before the ? is the condition 
            //between ? and : is the value if it is true
            //after that is the value if it is false
            //spriteRendy.color = (direction == -1) ? turnColor : enemyColor;
        }
    }
}
