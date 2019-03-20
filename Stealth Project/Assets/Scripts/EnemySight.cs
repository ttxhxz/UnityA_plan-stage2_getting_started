using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{
    public bool playerSight = false;
    public float fieldOfView = 110;
    public Vector3 alertPosition = Vector3.zero;

    private SphereCollider collider;
    private Animator playerAnim;
    private NavMeshAgent navAgent;
    private Vector3 preLastPlayerPosition;

    private void Awake()
    {
        collider = this.GetComponent<SphereCollider>();
        playerAnim = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Animator>();
        navAgent = this.GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        preLastPlayerPosition = GameController._instance.lastPlayerPosition;
    }

    private void Update()
    {
        if (preLastPlayerPosition != GameController._instance.lastPlayerPosition)
        {
            alertPosition = GameController._instance.lastPlayerPosition;
            preLastPlayerPosition = GameController._instance.lastPlayerPosition;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == Tags.player)
        {
            Vector3 forward = transform.forward;
            Vector3 playerDir = other.transform.position - transform.position;
            float temp = Vector3.Angle(forward, playerDir);
            if (temp <= 0.5f * fieldOfView)
            {
                playerSight = true;
                alertPosition = other.transform.position;
                GameController._instance.SeePlayer(other.transform.position);
            }
            else
            {
                playerSight = false;
            }

            if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion"))
            {
                NavMeshPath path = new NavMeshPath();
                if (navAgent.CalculatePath(other.transform.position, path))
                {
                    //Vector3[] wayPoints = new Vector3[path.corners.Length + 2];
                    //wayPoints[0] = transform.position;
                    //wayPoints[wayPoints.Length - 1] = other.transform.position;

                    //path里面已经包含起始位置和结束位置
                    Vector3[] wayPoints = new Vector3[path.corners.Length];

                    for (int i = 0; i < path.corners.Length; i++)
                    {
                        wayPoints[i] = path.corners[i];
                    }

                    float length = 0;
                    for (int i = 1; i < wayPoints.Length; i++)
                    {
                        length += (wayPoints[i] - wayPoints[i - 1]).magnitude;
                    }

                    if (length < collider.radius)
                    {
                        alertPosition = other.transform.position;
                    }
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.player)
        {
            alertPosition = other.transform.position;
        }
    }

}
