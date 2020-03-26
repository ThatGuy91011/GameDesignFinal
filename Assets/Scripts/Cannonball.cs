using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public float damage;
    public float force;
    private Rigidbody rb;

    public int pointValue = 10;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
       
    }
    //Adds force to bullet
    public void ApplyForce(float force)
    {
        rb.AddForce(transform.forward * force);
    }


    //If the bullet hits something...
    void OnCollisionEnter(Collision other)
    {
        //If it's the player...
        if (other.gameObject.tag == "Player")
        {
            //Player takes damage
            other.gameObject.GetComponent<TankData>().TakeDamage(damage);
            //Enemy earns points
            GameObject.Find("Enemy").GetComponent<TankData>().Score(pointValue);
            //Destroy this bullet
            Destroy(this.gameObject);
        }
        //If it's the enemy...
        if (other.gameObject.tag == "Enemy")
        {
            //Enemy takes damage
            other.gameObject.GetComponent<TankData>().TakeDamage(damage);
            //Player earns points
            GameObject.FindWithTag("Player").GetComponent<TankData>().Score(pointValue);
            //Destroy this bullet
            Destroy(this.gameObject);
        }
        //If it's a wall...
        if (other.gameObject.tag == "Wall")
        {
            //Destroy the bullet
            Destroy(this.gameObject);
        }
        //Otherwise...
        else
        {
            //Do nothing
        }
        
    }


}
