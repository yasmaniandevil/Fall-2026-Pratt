using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //every enemy has these
    public float moveSpeed = 2;
    [SerializeField] protected Color enemyColor;
    protected SpriteRenderer spriteRendy;

    protected virtual void Start()
    {
        spriteRendy = GetComponent<SpriteRenderer>();
        spriteRendy.color = enemyColor;
    }

    void Update()
    {
        Move();
    }

    //every enemy moves, but HOW is up to the child
    //by default the enemy just stands still
    protected virtual void Move()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //is the thing we touched a Player? (works for Seeker AND Hider)
        PlayerClass player = other.gameObject.GetComponent<PlayerClass>();
        if (player != null)
        {
            TouchPlayer(player);
        }
    }

    //what happens when we touch a player, children can replace this
    protected virtual void TouchPlayer(PlayerClass player)
    {
        Debug.Log(gameObject.name + " touched " + player.name);
    }
}