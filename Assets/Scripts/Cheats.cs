using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PowerUpController))]
public class Cheats : MonoBehaviour
{
    private PowerUpController powCon;

    public PowerUp pow;
    // Start is called before the first frame update
    void Start()
    {
        if (powCon == null)
        {
            powCon = gameObject.GetComponent<PowerUpController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            powCon.Add(pow);
        }
    }
}
