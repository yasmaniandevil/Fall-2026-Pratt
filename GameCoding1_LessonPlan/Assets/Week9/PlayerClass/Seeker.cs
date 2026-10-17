using UnityEngine;
using System.Collections;

public class Seeker : PlayerClass
{
    public Hider hider;            //drag the hider in the inspector
    public float revealRange = 5;  //how close the seeker has to be
    public float cooldownTime = 5; //how long before reveal can be used again
    public bool canReveal = true;
    
    //E Key
    protected override void UseAbility()
    {
        //if we're still cooling down, stop here
        if (!canReveal)
        {
            Debug.Log("reveal is cooling down");
            return; //return = leave the method early
        }
        
        //same distance check as ScoreManager
        float distance = Vector2.Distance(transform.position, hider.transform.position);

        if (distance <= revealRange)
        {
            //call the method that lives on the HIDER's script
            hider.Reveal();
        }

        //start the cooldown whether it hit or missed
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        canReveal = false;
        yield return new WaitForSeconds(cooldownTime);
        canReveal = true;
    }
    
    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hider != null)
        {
            Debug.Log("Caught!");
            //later: tell the GameManager the seeker won
        }
    }
}
