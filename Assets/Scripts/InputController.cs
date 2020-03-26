using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
[RequireComponent(typeof(TankShooter))]
public class InputController : MonoBehaviour
{
    //Allows the inspector to change the control scheme of the game
    public enum InputScheme { WASD, arrowKeys };
    public InputScheme input = InputScheme.WASD;


    private TankData data;

    private TankMotor motor;


    private float timeUntilCanShoot;

    public bool canShoot = true;

    private TankShooter shooter;
    // Start is called before the first frame update
    void Start()
    {
        data = gameObject.GetComponent<TankData>();
        motor = gameObject.GetComponent<TankMotor>();
        shooter = gameObject.GetComponent<TankShooter>();
    }

    // Update is called once per frame
    void Update()
    {

            //Switches from arrow keys or WASD depending on the inspector's choice
            switch (input)
            {
                //Control scheme for the arrow keys
                case InputScheme.arrowKeys:
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        motor.Move(data.moveSpeed);
                    }
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        motor.Move(-data.moveSpeed);
                    }
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        motor.Rotate(data.rotateSpeed);
                    }
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        motor.Rotate(-data.rotateSpeed);
                    }
                    break;

                //Control scheme for WASD
                case InputScheme.WASD:
                    if (Input.GetKey(KeyCode.W))
                    {
                        motor.Move(data.moveSpeed);
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        motor.Move(-data.moveSpeed);
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        motor.Rotate(data.rotateSpeed);
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        motor.Rotate(-data.rotateSpeed);
                    }
                    break;
            }
        
        
    
        //If the tank is able to shoot...
        if (canShoot)
        {
            //If the player presses space...
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Shooting
                shooter.Shoot();
                //Cooldown time
                canShoot = false;
                timeUntilCanShoot = data.fireRate;
            }
        }

        //Cooldown
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

