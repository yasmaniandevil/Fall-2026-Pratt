using System.Collections;
using UnityEngine;

public class Freezer : Enemy
{
    public float freezeTime = 2;

    //we DON'T override Move, so it uses the parent's (stands still)

    protected override void TouchPlayer(PlayerClass player)
    {
        base.TouchPlayer(player); //still do the parent's Debug.Log
        StartCoroutine(Freeze(player));
    }

    IEnumerator Freeze(PlayerClass player)
    {
        float originalSpeed = player.moveSpeed;
        player.moveSpeed = 0;
        yield return new WaitForSeconds(freezeTime);
        player.moveSpeed = originalSpeed;
    }
}