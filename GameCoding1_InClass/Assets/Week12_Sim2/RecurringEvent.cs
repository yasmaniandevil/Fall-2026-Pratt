using System.Collections;
using UnityEngine;

public class RecurringEvent : MonoBehaviour
{
    //we need a ref to our script, so we can do a reccuringevent for that stat
    public StatMeter statToAffect;
    //how often is the event happening
    public float secondsBetweenEvents = 10f;
    //you can also do amount gained
    public float amountLost = 15f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //must do start couroutine!
        //can also do pause
        StartCoroutine(EventLoop());
    }

    IEnumerator EventLoop()
    {
        //while loop
        //true just means repeat forever
        //normally a forever loop would freeze unity, but this one is safe
        //because yield return pauses it 
        while (true)
        {
            //wait first so the event doesnt happen the second the level loads
            yield return new WaitForSeconds(secondsBetweenEvents);
            Debug.Log("stomach sounds");
            //our function from stat meter!
            statToAffect.ChangeStat(-amountLost);

        }
    }
}
