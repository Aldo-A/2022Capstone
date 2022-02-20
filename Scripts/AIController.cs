using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public float startWaitTime = 4;
    public float timeToRotate = 2;
    public float speedWalk = 6;
    public float speedRun = 9;

    public float viewRadius = 15;
    public float viewAngle = 90;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution;
    public int edgeIterations;
    public float edgeDistance;

    public Transform[] waypoints;
    int currentWayPointIndex;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 PlayerPosition;

    float waitTime;
    float TimeToRotate;
    bool m_PlayerInRange;
    bool playerNear;
    bool isPatrol;
    bool caughtPlayer;


    // Start is called before the first frame update
    void Start()
    {
        PlayerPosition = Vector3.zero;
        isPatrol = true;
        caughtPlayer = false;
        m_PlayerInRange = false;
        waitTime = startWaitTime;
        TimeToRotate=timeToRotate;

        currentWayPointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(waypoints[currentWayPointIndex].position);
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(waypoints.Length + "LENGTH");
        Debug.Log(currentWayPointIndex+"CURRENT WAYPOINT");
        EnvironmentView();
        if (!isPatrol)
        {
            Chase();
            Debug.Log("CHASING");
        }
        else
        {
            Patrol();
            Debug.Log("Patrolling");
        }
    }



    void CaughtPlayer()
    {
        caughtPlayer = true;
    }
    
    void LookingPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3)
        {
            if (waitTime <= 0)
            {
                playerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[currentWayPointIndex].position);
                waitTime = startWaitTime;
                TimeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                waitTime -= Time.deltaTime;
            }
        }
    }

    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    public void NextPoint()
    {
        currentWayPointIndex = (currentWayPointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[currentWayPointIndex].position);
    }

    void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }

    void EnvironmentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    m_PlayerInRange = true;
                    isPatrol = false;
                }
                else
                {
                    m_PlayerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                m_PlayerInRange = false;

            }
            if (m_PlayerInRange)
            {
                PlayerPosition = player.transform.position;
            }
        }
    }

    private void Patrol()
    {
        if (playerNear)
        {
            if (TimeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            playerNear = false;
            playerLastPosition = Vector3.zero;
            navMeshAgent.SetDestination(waypoints[currentWayPointIndex].position);
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (waitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    waitTime = startWaitTime;
                }
                else
                {
                    playerNear = false;
                }
            }
        }
    }

    private void Chase()
    {
        playerNear = false;
        playerLastPosition = Vector3.zero;

        if (!caughtPlayer)
        {
            Move(speedRun);
            navMeshAgent.SetDestination(PlayerPosition);
        }
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if(waitTime<=0 && !caughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
            {
                isPatrol = true;
                playerNear = false;
                Move(speedWalk);
                TimeToRotate = timeToRotate;
                waitTime = startWaitTime;
                navMeshAgent.SetDestination(waypoints[currentWayPointIndex].position);
            }
            else
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                {
                    Stop();
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }
}
