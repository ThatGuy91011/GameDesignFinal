using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
public class SimpleAIController : MonoBehaviour
{
    public Transform[] waypoints;

    public TankData data;

    public TankMotor motor;

    private Transform tf;

    public int currentWaypoint = 0;

    public float closeEnough = 1.0f;

    public bool isPatrolForward = true;

    public enum LoopType
    {
        Stop,
        Loop,
        PingPong
    };

    public LoopType loopType = LoopType.Stop;
    // Start is called before the first frame update
    void Start()
    {
        data = gameObject.GetComponent<TankData>();
        motor = gameObject.GetComponent<TankMotor>();
        tf = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (motor.RotateTowards(waypoints[currentWaypoint].position, data.rotateSpeed))
        {
            // Do nothing!
        }
        else
        {
            // Move forward
            motor.Move(data.moveSpeed);
        }
        // If we are close to the waypoint,
        if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) < (closeEnough * closeEnough))
        {
            switch (loopType)
            {
                case LoopType.Stop:

                // Advance to the next waypoint, if we are still in range
                    if (currentWaypoint < waypoints.Length - 1)
                    {
                        currentWaypoint++;
                    }

                break;

                case LoopType.Loop:
                    if (currentWaypoint < waypoints.Length - 1)
                    {
                        currentWaypoint++;
                    }
                    else
                    {
                        currentWaypoint = 0;
                    }

                    break;
                case LoopType.PingPong:
                    if (isPatrolForward)
                    {
                        // Advance to the next waypoint, if we are still in range
                        if (currentWaypoint < waypoints.Length - 1)
                        {
                            currentWaypoint++;
                        }
                        else
                        {
                            //Otherwise reverse direction and decrement our current waypoint
                            isPatrolForward = false;
                            currentWaypoint--;
                        }
                    }
                    else
                    {
                        // Advance to the next waypoint, if we are still in range
                        if (currentWaypoint > 0)
                        {
                            currentWaypoint--;
                        }
                        else
                        {
                            //Otherwise reverse direction and decrement our current waypoint
                            isPatrolForward = true;
                            currentWaypoint++;
                        }
                    }

                    break;
                default:
                    Debug.LogError("LoopType not implemented.");
                    break;
            }
        }
    }
}
