using UnityEngine;
using System.Collections;

public class Hider : PlayerClass
{
    public float hideTime = 3;
    bool isHidden = false;
    private SpriteRenderer spriteRendy;
    public GameObject hiderDecoy;
    private bool droppedDecoy = false;
    public float decoyDuration = 3;

    
    protected override void Start()
    {
        base.Start();
        moveSpeed = 7;
        spriteRendy = GetComponent<SpriteRenderer>();
    }

    //shift key
    protected override void UseAbility()
    {
        if (!isHidden)
        {
            StartCoroutine(Hide());
        }
        
    }

    //enter key
    protected override void AbilityTwo()
    {
        if (!droppedDecoy)
        {
            StartCoroutine(DropDecoy());
            
        }

    }

    IEnumerator DropDecoy()
    {
        droppedDecoy = true;
        Vector3 offset = new Vector3(2, 0, 0);
        GameObject hiderDecoySpawn = Instantiate(hiderDecoy, transform.position + offset, Quaternion.identity);
        yield return new WaitForSeconds(decoyDuration);
        Destroy(hiderDecoySpawn);
        droppedDecoy = false;
    }

    IEnumerator Hide()
    {
        
        spriteRendy.enabled = false;
        isHidden = true;
        yield return new WaitForSeconds(hideTime);
        spriteRendy.enabled = true;
        isHidden = false;
    }
    
    //PUBLIC so the seeker's script is allowed to call it
    public void Reveal()
    {
        if (isHidden)
        {
            //stop the hide timer so it doesn't keep running
            //note: this stops EVERY coroutine on the hider
            StopAllCoroutines();
            spriteRendy.enabled = true;
            isHidden = false;
            Debug.Log("hider revealed!");
        }
    }
}
