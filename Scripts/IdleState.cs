using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IdleState : State
{
    public bool canSeePlayer;
    public ChaseState chaseState;
    private Vector3 startingPosition;
    private Vector3 roamingPosition;

    public void Start()
    {
        startingPosition = transform.position;
        roamingPosition = GetRoamingPosition();
    }

    public void Update()
    {

    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized * Random.Range(10f,70f);
    }

    public override State RunCurrentState()
    {
        if (canSeePlayer)
        {
            return chaseState;
        }
        else
        {
            return this;
        }
    }
}
