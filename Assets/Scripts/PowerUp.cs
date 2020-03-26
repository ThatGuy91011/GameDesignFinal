using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PowerUp
{
    public float speedModifier;
    public float healthModifier;
    public float maxHealthModifier;
    public float fireRateModifier;

    public float duration;
    public bool isPermanent;

    public void OnActivate(TankData target) //Fires when the player interacts with it
    {
        target.moveSpeed += speedModifier;
        target.health += healthModifier;
        target.maxHealth += maxHealthModifier;
        target.fireRate += fireRateModifier;
    }
    public void OnDeactivate(TankData target)
    {
        target.moveSpeed -= speedModifier;
        target.health -= healthModifier;
        target.maxHealth -= maxHealthModifier;
        target.fireRate -= fireRateModifier;
    }
}
