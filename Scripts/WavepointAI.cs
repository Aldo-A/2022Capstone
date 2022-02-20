using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavepointAI : MonoBehaviour
{
    public GameObject[] wayPoints;
    public int num = 0;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;

    public float minDist;
    public float speed;

    public bool random = false;
    public bool go = true;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(gameObject.transform.position, wayPoints[num].transform.position);
        if (go)
        {
            if (distance > minDist)
            {
                Move();
            }
            else
            {
                if (!random)
                {
                    if (num + 1 == wayPoints.Length)
                    {
                        num = 0;
                    }
                    else
                    {
                        num++;
                    }
                }
                else
                {
                    num = Random.Range(0, wayPoints.Length);
                }
            }
        }

    }

    public void Move()
    {
        //gameObject.transform.LookAt(wayPoints[num].transform.position);
        //gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
        navMeshAgent.destination = wayPoints[num].transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        //Output the Collider's GameObject's name
        Debug.Log(collision.collider.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        //wayPoints[num]= other.gameObject;
        Destroy(other);
    }

}
