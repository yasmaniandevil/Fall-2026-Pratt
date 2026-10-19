using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2;
    //serializefield means i can see it in the inspector
    //protected means my subclasses can access this variable
    [SerializeField] protected Color enemyColor;
    //i dont need to see spriterender in the inspector but i do need to let the sub classes access it
    //these are called access modifiers public, private, serial, protected the access it gives to other classes or not
    protected SpriteRenderer spriteRendy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    //make my start virtual so i can override it later in sub classes
    //i can either say public if other classes not related to this one needs it
    //or protected if only my subclasses need it
    protected virtual void Start()
    {
        spriteRendy = GetComponent<SpriteRenderer>();
        spriteRendy.color = enemyColor;
    }

    void Update()
    {
        Move();
    }



    //every enemy moves but HOW is up to the sub classes
    //by default enemy stands still

    //we can use abstract or virtual 
    //if we use abstract that means it HAS TO BE OVERRIDEN
    //for the enemy example because they have different behavior, i dont need to constantly override one specific thing
    //it is optional
    //when we use abstract the class also has to become abstract
    protected virtual void Move() { }

    //what happens when wwe touch a player, sub classes can replace this too
    //but what is better to pass is if your two players share a base class called player
    //playerone and two derive from that base class ex: PlayerOne : PlayerBaseClass
    //then in this function i would just pass in the base class "PlayerBaseClass" any script that
    //derrives from it, will count
    protected virtual void TouchPlayer(PlayerOne player)
    {
        Debug.Log(gameObject.name + "touched");
    }

    //we could also make this protected virtual void, and i would assume you might have to at some point
    //so we can override it in the subclasses
    //but its better to keep it like this, and only ovverride touch player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //need to make sure that whatever i collide with has this player script
        //if you make a base class it should be PlayerBaseClass instead so it counts for player1 and 2
        PlayerOne player = collision.gameObject.GetComponent<PlayerOne>();
        //if we actually grabbed it
        if(player != null)
        {
            //call our function and pass in the player 
            TouchPlayer(player);
        }
    }

}
