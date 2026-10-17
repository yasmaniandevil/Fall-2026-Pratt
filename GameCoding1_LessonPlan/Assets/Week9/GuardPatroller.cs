using UnityEngine;

//GuardPatroller IS A Patroller, which IS AN Enemy
public class GuardPatroller : Patroller
{
    public Transform target;     //drag a player in the inspector
    public float sightRange = 3; //how close before it notices you

    protected override void Move()
    {
        if (target == null)
        {
            base.Move(); //no target, just patrol
            return;
        }

        //find the distance between the guard and the player/target
        float distance = Vector2.Distance(transform.position, target.position);

        //if distance is less than sightrange
        //so if distance if 4 and sightrange is 3, will not follow
        if (distance <= sightRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            //player is far, go back to normal patrolling
            //base.Move() = run the PATROLLER's Move
            base.Move();
        }
    }
}