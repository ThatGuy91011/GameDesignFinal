using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

[RequireComponent(typeof(TankData))]
public class SimpleAIController4 : MonoBehaviour
{
    private Transform tf;
    public enum AIState { Chase, Patrol, Hear };
    public AIState aiState = AIState.Patrol;
    public enum Personalities { Sense, Waypoint, Flee, Chasing };

    public Personalities personality = Personalities.Sense;
    public float stateEnterTime;
    public float aiSenseRadius;
    public float restingHealRate; // in hp/second 
    public GameObject player;
    public TankData data;
    public TankMotor motor;

    public float hearingDistance = 10f;

    public float inSight = 10f;
    public float distance;

    public Transform[] waypoints;
    private int currentWaypoint = 0;
    public enum LoopType { Stop, Loop, PingPong };
    public LoopType loopType = LoopType.Stop;
    private float closeEnough = 1.0f;
    private bool isPatrolForward = true;




    public enum AttackMode { Chase, Flee };
    public AttackMode attackMode;
    public Transform target;
    public float fleeDistance = 1.0f;



    public enum AvoidanceStage { None, Rotate, Move };
    public AvoidanceStage avoidanceStage;
    public float avoidanceTime = 2.0f;
    private float exitTime;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Always finds the distance between the player and the enemy
        distance = Vector3.Distance(player.GetComponent<Transform>().position, tf.position);
        switch (personality)
        {
            //Different personalities
            case Personalities.Sense:
                Sense();
                break;
            case Personalities.Waypoint:
                Waypoint();
                break;
            case Personalities.Flee:
                Flee();
                break;
            case Personalities.Chasing:
                Chasing();
                break;
        }
    }
    //Hearing/Seeing
    private void Sense()
    {
        switch (aiState)
        {
            case AIState.Patrol:
                //Always move in a circle in patrol mode
                motor.Move(data.moveSpeed);
                motor.Rotate(data.rotateSpeed);
                //If the player is able to be heard...
                if (distance <= hearingDistance)
                {
                    //Change to hear
                    ChangeState(AIState.Hear);
                }
                break;
            case AIState.Hear:
                //Look at the player
                motor.RotateTowards(player.GetComponent<Transform>().position, data.moveSpeed);
                //If the AI sees the player...
                if (CanMove(data.moveSpeed))
                {
                    //Chase the player
                    ChangeState(AIState.Chase);
                }
                //Otherwise...
                else
                {
                    //Go back to patrol
                    ChangeState(AIState.Patrol);
                }
                break;
            case AIState.Chase:
                //Chase the player
                Chase(player);
                //If the AI no longer sees the player
                if (!CanMove(data.moveSpeed))
                {
                    //If the player is still alive
                    if (player.GetComponent<TankData>().health > 0)
                    {
                        //Go back to patrol
                        ChangeState(AIState.Patrol);
                    }
                }
                break;
        }
    }
    //Waypoints
    private void Waypoint()
    {
        waypoints[0] = GameObject.Find("Waypoint (1)").transform;
        waypoints[1] = GameObject.Find("Waypoint (2)").transform;
        waypoints[2] = GameObject.Find("Waypoint (3)").transform;
        waypoints[3] = GameObject.Find("Waypoint").transform;
        //If the AI is already rotated towards the next waypoint...
        if (motor.RotateTowards(waypoints[currentWaypoint].position, data.rotateSpeed))
        {
            // Do nothing!
        }
        else
        {
            // Move forward
            if (CanMove(data.moveSpeed))
            {
                motor.Move(data.moveSpeed);
            }
        }
        // If we are close to the waypoint...
        if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) < (closeEnough * closeEnough))
        {
            switch (loopType)
            {
                //If the loop type is "stop"...
                case LoopType.Stop:

                    // Advance to the next waypoint, if we are still in range
                    if (currentWaypoint < waypoints.Length - 1)
                    {
                        currentWaypoint++;
                    }
                    //Once we reach the last waypoint, stop
                    break;

                //If the loop type is "Loop"
                case LoopType.Loop:
                    // Advance to the next waypoint, if we are still in range
                    if (currentWaypoint < waypoints.Length - 1)
                    {
                        currentWaypoint++;
                    }
                    //Otherwise...
                    else
                    {
                        //Start back at the beginning
                        currentWaypoint = 0;
                    }

                    break;
                case LoopType.PingPong:
                    //If we are moving forward...
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
                            //Otherwise reverse direction and increment our current waypoint
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
    //Flee only
    private void Flee()
    {
        switch (attackMode)
        {
            //If this AI is in chase mode...
            case AttackMode.Chase:
                //Do nothing
                if (distance < 10f)
                {
                    attackMode = AttackMode.Flee;
                }
                break;
            //If this AI is in flee mode...
            case AttackMode.Flee:
                //If there is something in the way...
                if (avoidanceStage != AvoidanceStage.None)
                {
                    //Avoid it
                    Avoid();
                }
                //Otherwise...
                else
                {
                    //Flee
                    Flee(player);
                    //If the AI is far enough away from the player...
                    if (distance >= 10f)
                    {
                        //Stop
                        attackMode = AttackMode.Chase;
                    }
                }
                break;
            default:
                Debug.LogError("Attack Mode not implemented");
                break;
        }
    }
    //Chase only
    private void Chasing()
    {
        switch (attackMode)
        {
            case AttackMode.Chase:
                //If there is something in the way...
                if (avoidanceStage != AvoidanceStage.None)
                {
                    //Avoid it
                    Avoid();
                }
                else
                {
                    Chase(player);
                }
                break;
            case AttackMode.Flee:
                //Do nothing
                break;
        }
    }

    private void Flee(GameObject target)
    {
        // The vector from ai to target is target position minus our position.
        Vector3 vectorToTarget = target.GetComponent<Transform>().position - tf.position;
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
    }
    private void Avoid()
    {
        switch (avoidanceStage)
        {
            case AvoidanceStage.Rotate:
                //Rotate
                motor.Rotate(data.rotateSpeed);
                //If there is nothing in the way...
                if (CanMove(data.moveSpeed))
                {
                    //Move
                    avoidanceStage = AvoidanceStage.Move;
                    exitTime = avoidanceTime;
                }
                break;
            case AvoidanceStage.Move:
                //If there is nothing in the way...
                if (CanMove(data.rotateSpeed))
                {
                    exitTime -= Time.deltaTime;
                    //Move forwards
                    motor.Move(data.moveSpeed);

                    if (exitTime <= 0f)
                    {
                        avoidanceStage = AvoidanceStage.None;
                    }
                }
                //Otherwise...
                else
                {
                    //Rotate
                    avoidanceStage = AvoidanceStage.Rotate;
                }
                break;
        }
    }

    public bool CanMove(float speed)
    {
        RaycastHit hit;
        //If we hit something...
        if (Physics.Raycast(tf.position, tf.forward, out hit, speed))
        {
            // ... and if what we hit is not the player...
            if (!hit.collider.CompareTag("Player") && !hit.collider.CompareTag("Enemy"))
            {
                // ... then we can't move
                return false;
            }
        }

        // otherwise, return true
        return true;
    }

    public void Chase(GameObject target)
    {
        target = GameObject.FindWithTag("Player");
        //Rotate towards player
        motor.RotateTowards(target.GetComponent<Transform>().position, data.rotateSpeed);
        if (CanMove(data.moveSpeed))
        {
            motor.Move(data.moveSpeed);
        }
        else
        {
            avoidanceStage = AvoidanceStage.Rotate;
        }
    }

    public void ChangeState(AIState newState)
    {

        // Change our state
        aiState = newState;

        // save the time we changed states
        stateEnterTime = Time.time;
    }
}
