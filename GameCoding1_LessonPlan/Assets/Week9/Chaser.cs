using UnityEngine;

public class Chaser : Enemy
{
    public Transform chaseTarget;

    protected override void Move()
    {
        if (chaseTarget != null)
        {
            //move a little bit toward the target every frame
            transform.position = Vector2.MoveTowards(transform.position, chaseTarget.position, moveSpeed * Time.deltaTime);
        }
    }
}