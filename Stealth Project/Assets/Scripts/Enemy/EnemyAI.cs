using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //追踪速度
    public float chaseSpeed = 5f;
    //巡逻速度
    public float patrolSpeed = 2f;
    //追踪等待时间
    public float chaseWaitTime = 5f;
    //巡逻等待时间
    public float patrolWaitTime = 1f;
    //巡逻路径点
    public Transform[] patrolWayPoints;
    private EnemySight enemySight;
    private NavMeshAgent nav;
    private Transform player;
    private PlayerHealth playerHealth;
    private LastPlayerSighting lastPlayerSighting;
    //追踪Timer
    private float chaseTimer;
    //巡逻Timer
    private float patrolTimer;
    //路径点index
    private int wayPointIndex;

    private void Awake()
    {
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();

    }

    private void Update()
    {
        if (enemySight.playerInSight && playerHealth.health > 0f)
        {
            Shooting();
        }
        else if (enemySight.personalLastSighting != lastPlayerSighting.resetPosition && playerHealth.health > 0f)
        {
            Chasing();
        }
        else
        {
            Patrolling();
        }
    }

    void Shooting()
    {
        nav.isStopped = true;
    }

    void Chasing()
    {
        nav.isStopped = false;
        Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;
        //sqrMagnitude返回向量的平方
        if (sightingDeltaPos.sqrMagnitude > 4f)
        {
            nav.destination = enemySight.personalLastSighting;
        }

        nav.speed = chaseSpeed;

        if (nav.remainingDistance < nav.stoppingDistance)
        {
            chaseTimer += Time.deltaTime;
            //一定时间内没有新的坐标，则重置相关的坐标
            if (chaseTimer > chaseWaitTime)
            {
                lastPlayerSighting.position = lastPlayerSighting.resetPosition;
                enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
                chaseTimer = 0f;
            }
        }
        else
        {
            chaseTimer = 0f;
        }
    }

    void Patrolling()
    {
        nav.speed = patrolSpeed;

        if (nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;

            if (patrolTimer >= patrolWaitTime)
            {
                if (wayPointIndex == patrolWayPoints.Length - 1)
                {
                    wayPointIndex = 0;
                }
                else
                {
                    wayPointIndex++;
                }

                patrolTimer = 0f;
            }
        }
        else
        {
            patrolTimer = 0f;
        }

        nav.destination = patrolWayPoints[wayPointIndex].position;
    }
}
