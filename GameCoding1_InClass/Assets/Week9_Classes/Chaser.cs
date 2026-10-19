using UnityEngine;

public class Chaser : Enemy
{
    //now we are overriding the move function in enemy base class
    //to be specfic to the chaser enemy
    //needs either to be protected or abstract

    //what am i chasing?
    public Transform chaseTarget;
    protected override void Move()
    {
        if(chaseTarget != null)
        {
            //move a little bit toward the target every frame
            //i can access the move speed because enemy base class has it
            transform.position = Vector2.MoveTowards(transform.position, chaseTarget.position, 
                moveSpeed * Time.deltaTime);
        }
    }

   
}
