using System.Collections;
using UnityEngine;

public class Freezer : Enemy
{
    //how long i freeze the player for
    //i need to see it in the inspector
    //but other scripts dont need to access it
    [SerializeField] float freezeTime = 2;

    
    protected override void TouchPlayer(PlayerOne player)
    {
        //i want the base because i still want the debug log
        base.TouchPlayer(player);
        StartCoroutine(Freeze(player));
    }

    //i am passing in the player
    //to access the players speed
    IEnumerator Freeze(PlayerOne player)
    {
        //first we store the players speed in a variable called original speed
        float originalSpeed = player.moveSpeed;
        //then we change the player speed to 0
        player.moveSpeed = 0;
        //we wait
        yield return new WaitForSeconds(freezeTime);
        //now we give the player back their original speed (that is why we stored it)
        player.moveSpeed = originalSpeed;
    }
}
