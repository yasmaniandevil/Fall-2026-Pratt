using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RefillStation : MonoBehaviour
{
    //this script can be used for whatever is going to refill the stat
    //press e the stat refills a little at a time
    //ex: refill a glass of water, does not auto refill to the top, it refills little by little until its full

    public StatMeter statToRefill; //drag in the stat this station refills here
    public float amountPerTick = 5f; //how much each little refill adds
    public float timeBtwnTicks = 0.25f; //seconds to wait between refills
    public int numberOfTicks = 8; //how many refills per E press (8 X 5 = 40 total)
    private bool playerIsHere = false; //is the player standing in the trigger?
    private bool isRefilling = false; //is a refill already happening? so you cant refill forever unlimited times
    public TextMeshProUGUI eText; //text pops up to press E

    private Wallet playerWallet; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //debug here says enter trigg
        if (collision.CompareTag("Player"))
        {
            //debug here that says player in trigger
            playerIsHere = true;
            eText.enabled = true; //enabled is for UI its like set active but you turn off just the text component
            playerWallet = collision.GetComponent<Wallet>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsHere = false;
            eText.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //all three have to be true for the coroutine to start and refill the meter
        if(playerIsHere && Keyboard.current.eKey.isPressed && !isRefilling)
        {
            if(playerWallet != null)
            {
                playerWallet.SpendMoney(2);
            }
            StartCoroutine(RefillOverTime());
        }
    }

    //coroutine: a function that can be paused
    IEnumerator RefillOverTime()
    {
        isRefilling = true;

        //repeat the refill number of ticks time
        for(int i = 0; i < numberOfTicks; i++)
        {
            //if the player has walked away stop refilling early
            if (!playerIsHere)
            {
                break; //break jumps out of the loop
            }

            //add a little to the stat
            statToRefill.ChangeStat(amountPerTick);

            //pause this function here for a bit and the rest of the game keeps going
            //after the wait unity comes back and continues from this line
            yield return new WaitForSeconds(timeBtwnTicks);

        }

        isRefilling = false; //so the player can press e again 
    }
}
