using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class TankShooter : MonoBehaviour
{
    public GameObject cannonball;

    public GameObject firePoint;

    private TankData data;

    private float timeUntilCanShoot;

    public bool canShoot = true;

    public int despawnTime;

    private AudioSource shoot;
    // Start is called before the first frame update
    void Start()
    {
        data = gameObject.GetComponent<TankData>();
        shoot = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the game object is the enemy, have it shoot continuously
        if (this.gameObject.tag == "Enemy")
        {
            if (canShoot)
            {
                //Shooting
                    Shoot();
                    canShoot = false;
                    timeUntilCanShoot = data.fireRate;
            }

            if (timeUntilCanShoot > 0)
            {
                timeUntilCanShoot -= Time.deltaTime;
            }
            else
            {
                canShoot = true;
            }
        }
    }

    //Shooting function
    public void Shoot()
    {
        //Play sound of shooting
        shoot.Play();
        //Instantiate bullet
        GameObject newestCannonball = Instantiate(cannonball, firePoint.transform.position, firePoint.transform.rotation);
        Cannonball cannonBallComponent = newestCannonball.GetComponent<Cannonball>();
        Rigidbody cannonBody = newestCannonball.GetComponent<Rigidbody>();
        //Apply force
        //cannonBallComponent.ApplyForce(data.shell);
        cannonBody.AddForce(data.shellForce * transform.forward, ForceMode.Impulse);
        //Set damage
        cannonBallComponent.damage = data.damageDone;

        //Start the despawn timer
        StartCoroutine(Despawn(newestCannonball));
    }

    //Despawn time for bullet
    IEnumerator Despawn(GameObject ball)
    {
        yield return new WaitForSecondsRealtime(despawnTime);
        Destroy(ball);
    }
}
