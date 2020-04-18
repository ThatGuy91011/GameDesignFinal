using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public float damage;
    public float force;
    private Rigidbody rb;

    public int pointValue = 10;

    public AudioSource audio;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        audio = GameObject.Find("Hit").GetComponent<AudioSource>();
        audio.volume = 1;
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
            audio.Play();
            //Player takes damage
            other.gameObject.GetComponent<TankData>().TakeDamage(damage);
            //Enemy earns points
            //GameObject.FindWithTag("Enemy").GetComponent<TankData>().Score(pointValue);
            //Destroy this bullet
            Destroy(this.gameObject);
        }
        //If it's the enemy...
        if (other.gameObject.tag == "Enemy")
        {
            audio.Play();
            //Enemy takes damage
            other.gameObject.GetComponent<TankData>().TakeDamage(damage);
            //Player earns points
            GameObject.FindWithTag("Player").GetComponent<TankData>().Score(pointValue);
            //Destroy this bullet
            Destroy(this.gameObject);
        }
        if (other.gameObject.name == "Player 1")
        {
            audio.Play();
            //Enemy takes damage
            other.gameObject.GetComponent<TankData>().TakeDamage(damage);
            //Player earns points
            GameObject.Find("Player 2").GetComponent<TankData>().Score(pointValue);
            //Destroy this bullet
            Destroy(this.gameObject);
        }
        if (other.gameObject.name == "Player 2")
        {
            audio.Play();
            //Enemy takes damage
            other.gameObject.GetComponent<TankData>().TakeDamage(damage);
            //Player earns points
            Debug.Log("Hit Player 2");
            GameObject.Find("Player 1").GetComponent<TankData>().Score(pointValue);
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
