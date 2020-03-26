using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PowerUp powerup;
    public int RespawnTime;

    public AudioClip FeedbackAudioClip;

    public void OnTriggerEnter(Collider other)
    {
        // variable to store other object's PowerupController - if it has one
        PowerUpController powCon = other.GetComponent<PowerUpController>();

        // If the other object has a PowerupController
        if (powCon != null)
        {
            // Add the powerup
            powCon.Add(powerup);
            if (FeedbackAudioClip != null)
            {
                AudioSource.PlayClipAtPoint(FeedbackAudioClip, gameObject.GetComponent<Transform>().position, 1.0f);
            }
            // Destroy this pickup
            GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(Respawn(powerup));
        }

        IEnumerator Respawn(PowerUp power)
        {
            yield return new WaitForSeconds(RespawnTime);
            GetComponent<MeshRenderer>().enabled = true;
            powerup.duration = 5;
        }
    }
}
