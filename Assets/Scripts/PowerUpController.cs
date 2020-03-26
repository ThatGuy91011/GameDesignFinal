using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

[RequireComponent(typeof(TankData))]
public class PowerUpController : MonoBehaviour
{
    public List<PowerUp> powerups;
    

    public TankData data;
    // Start is called before the first frame update
    void Start()
    {
        powerups = new List<PowerUp>();
        data = GetComponent<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        List<PowerUp> expiredPowerUps = new List<PowerUp>();

        foreach (PowerUp power in powerups)
        {
            power.duration -= Time.deltaTime;

            if (power.duration <= 0)
            {
                expiredPowerUps.Add(power);
                
            }
        }

        foreach (PowerUp power in expiredPowerUps)
        {
            power.OnDeactivate(data);
            powerups.Remove(power);
        }

        expiredPowerUps.Clear();
    }

    public void Add(PowerUp powerup)
    {
        powerup.OnActivate(data);
        if (!powerup.isPermanent)
        {
            powerups.Add(powerup);
        }
    }


}
