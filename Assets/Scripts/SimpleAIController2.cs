using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimpleAIController2 : MonoBehaviour
{
    public enum AttackMode { Chase, Flee };

    public AttackMode attackMode;
    public Transform target;



    public float fleeDistance = 1.0f;

    private Transform tf;

    private TankData data;

    private TankMotor motor;
    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (attackMode)
        {
            case AttackMode.Chase:
                target = GameObject.Find("Player").GetComponent<Transform>();
                //Rotate towards player
                motor.RotateTowards(target.position, data.rotateSpeed);
                //Move towards player
                motor.Move(data.moveSpeed);
                break;
            case AttackMode.Flee:
                // The vector from ai to target is target position minus our position.
                Vector3 vectorToTarget = target.position - tf.position;
                // We can flip this vector by -1 to get a vector AWAY from our target
                Vector3 vectorAway = vectorToTarget * -1;
                // Now, we can normalize that vector to give it a magnitude of 1
                vectorAway.Normalize();
                // A normalized vector can be multiplied by a length to make a vector of that length.
                vectorAway *= fleeDistance;
                // We can find the position in space we want to move to by adding our vector away from our AI to our AI's position.
                //This gives us a point that is "that vector away" from our current position.
                Vector3 fleePosition = vectorAway + tf.position;
                motor.RotateTowards(fleePosition, data.rotateSpeed);
                motor.Move(data.moveSpeed);
                break;
            default:
                Debug.LogError("Attack Mode not implemented");
                break;
        }
    }
}
