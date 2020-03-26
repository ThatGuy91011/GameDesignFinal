using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
public class SimpleAIController3 : MonoBehaviour
{
    public Transform target;
    private TankMotor motor;
    private TankData data;
    private Transform tf;

    public enum AvoidanceStage { None, Rotate, Move };

    public AvoidanceStage avoidanceStage;
    public float avoidanceTime = 2.0f;
    private float exitTime;
    public enum AttackMode { Chase };
    public AttackMode attackMode;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<TankMotor>();
        data = GetComponent<TankData>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackMode == AttackMode.Chase)
        {
            if (avoidanceStage != AvoidanceStage.None)
            {
                DoAvoid();
            }
            else
            {
                DoChase();
            }
        }

    }

    private void DoAvoid()
    {
        switch (avoidanceStage)
        {
            case AvoidanceStage.Rotate:
                motor.Rotate(data.rotateSpeed);
                if (CanMove(data.moveSpeed))
                {
                    avoidanceStage = AvoidanceStage.Move;
                    exitTime = avoidanceTime;
                }
                break;
            case AvoidanceStage.Move:
                if (CanMove(data.rotateSpeed))
                {
                    exitTime -= Time.deltaTime;
                    motor.Move(data.moveSpeed);

                    if (exitTime <= 0f)
                    {
                        avoidanceStage = AvoidanceStage.None;
                    }
                }
                else
                {
                    avoidanceStage = AvoidanceStage.Rotate;
                }
                break;
        }
    }

    private void DoChase()
    {
        motor.RotateTowards(target.position, data.rotateSpeed);

        if (CanMove(data.moveSpeed))
        {
            motor.Move(data.moveSpeed);
        }
        else
        {
            avoidanceStage = AvoidanceStage.Rotate;
        }
    }

    public bool CanMove(float speed)
    {
        RaycastHit hit;
        if (Physics.Raycast(tf.position, tf.forward, out hit, speed))
        {
            // ... and if what we hit is not the player...
            if (!hit.collider.CompareTag("Player"))
            {
                // ... then we can't move
                return false;
            }
        }

        // otherwise, return true
        return true;
    }
}
