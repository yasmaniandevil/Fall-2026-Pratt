using UnityEngine;
using TMPro;

/* 
Keeps track of the player's money and shows it on screen.
Money is a RESOURCE, not a stat: it doesn't drain over time,
 it only changes when the player does something (earns or spends).

*/

public class Wallet : MonoBehaviour
{
    // How much money the player has right now
    public int money = 0;

    // The text on screen that shows the money
    public TMP_Text moneyText;

    // Unity calls Start automatically, once, at the beginning
    void Start()
    {
        // Show the starting amount right away
        UpdateMoneyText();
    }

    // We call this ourselves (the Chore script calls it) when the player earns money.
    // amount is the argument: whoever calls this method decides how much to add.
    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyText();
    }

    // We call this ourselves (the Fridge calls it) when the player buys something
    public void SpendMoney(int amount)
    {
        money -= amount;
        UpdateMoneyText();
    }

    // A little helper method so we don't write the same line in two places
    void UpdateMoneyText()
    {
        moneyText.text = "$" + money;
    }
}
