using UnityEngine;

public class GuardPatroller : Patroller
{
    //playert
    public Transform target;
    //how close before it notices you
    [SerializeField] float sightRange = 3;

    protected override void Move()
    {
        //if i have no player to follow
        if(target == null)
        {
            //just do the base movement on the patroller script
            base.Move();
            //return means exit the function here
            //dont do the rest of my function below because i dont have a player!
            return;
        }

        //find the distance betrween the guard and the player/target
        float distance = Vector2.Distance(transform.position, target.position);

        //if the distance is less than the sight range
        //so ex: if ditance is 4 and sightrange is 3 it will not follow
        if(distance <= sightRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position,
                moveSpeed * Time.deltaTime);
        }
        else
        {
            //player is far, go back to normal patrolling
            //base.move is the PATROLLERS MOVE
            base.Move();
        }
    }
    
}
