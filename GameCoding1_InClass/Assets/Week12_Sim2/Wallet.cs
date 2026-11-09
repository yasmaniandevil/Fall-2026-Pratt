using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    /*this script will keep track of the players money and show it on the screen
     money is a RESOURCE not a stat, it does not drain over time
    it only changes when the player does something (earns or spends)*/

    //how much money the player has right now
    public int money = 0;

    public TMP_Text moneyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateMoneyText();
    }

    //we call this on different scripts when the player earns money
    //int amount is the parameter/argument that we have to pass to our function
    //when we call the function we decide how much to add ()
    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyText();
    }

    //we call this ourselves when the player buys something
    //we can collapse them into one function and just do ChangeMoney(subtract or add)
    public void SpendMoney(int amount)
    {
        money -= amount;
        UpdateMoneyText();

    }

    //helper function so we dont write the same line over and over again
    void UpdateMoneyText()
    {
        moneyText.text = "$" + money;
    }
    
}
