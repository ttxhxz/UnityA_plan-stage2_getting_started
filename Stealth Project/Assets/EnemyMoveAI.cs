using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveAI : MonoBehaviour
{
    public Transform[] wayPoints;

    public float patrolTime = 3f;
    public float patrolTimer = 0f;

    private NavMeshAgent navAgent;
    private int index = 0;

    private void Awake()
    {
        navAgent = this.GetComponent<NavMeshAgent>();
        navAgent.destination = wayPoints[index].position;
    }

    // Update is called once per frame
    void Update()
    {
        Patrolling();
    }

    //巡逻
    void Patrolling()
    {
        if (navAgent.remainingDistance < 0.5f)
        {
            navAgent.isStopped = true;
            patrolTimer += Time.deltaTime;
            if (patrolTimer > patrolTime)
            {
                index++;
                index %= wayPoints.Length;
                navAgent.destination = wayPoints[index].position;
                patrolTimer = 0;
                navAgent.isStopped = false;
            }
        }
    }
}
