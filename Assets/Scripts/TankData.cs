using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    //Variable to hold attributes of each tank
    public float moveSpeed;

    public float rotateSpeed;

    public float shellForce = 1.0f;

    public float damageDone = 1.0f;

    public float fireRate = 1.0f;

    public float health;

    public float maxHealth = 10.0f;

    public int score = 0;

    public int playerNumber = 1;

    void Start()
    {
        //Each tank's health starts out at the maximum
        health = maxHealth;
    }

    //If a tank takes damage...
    public void TakeDamage(float damage)
    {
        //Reduce health by how much damage is done
        health -= damage;
        //If health lowers below 0...
        if (health <= 0)
        {
            //The tank dies
            Die();
        }
    }

    //Function for tank death
    public void Die()
    { 
       
        Destroy(this.gameObject);
    }

    //Function for tank score
    public void Score(int points)
    {
        score += points;
    }

}
