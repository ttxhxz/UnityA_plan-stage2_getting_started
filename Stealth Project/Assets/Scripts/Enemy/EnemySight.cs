using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{
    public float fieldOfView = 110;
    public bool playerSight = false;
    public Vector3 alertPosition = Vector3.zero;

    private NavMeshAgent navAgent;
    private SphereCollider collider;
    private Animator playerAnim;
    private Vector3 preLastPlayerPosition;

    private void Awake()
    {
        collider = this.GetComponent<SphereCollider>();
        playerAnim = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Animator>();
        navAgent = this.GetComponent<NavMeshAgent>();
    }

    //private void Start()
    //{
    //    preLastPlayerPosition = GameController._instance.lastPlayerPosition;
    //}

    //private void Update()
    //{
    //    if (preLastPlayerPosition != GameController._instance.lastPlayerPosition)
    //    {
    //        alertPosition = GameController._instance.lastPlayerPosition;
    //        preLastPlayerPosition = GameController._instance.lastPlayerPosition;
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == Tags.player)
    //    {
    //        Vector3 direction = other.transform.position - transform.position;
    //        float angle = Vector3.Angle(direction, transform.forward);

    //        if (angle <= 0.5f * fieldOfView)
    //        {
    //            RaycastHit hit;
    //            if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, collider.radius))
    //            {
    //                if (hit.collider.tag == Tags.player)
    //                {
    //                    playerSight = true;
    //                    alertPosition = other.transform.position;
    //                    GameController._instance.SeePlayer(other.transform.position);
    //                }
    //            }

    //        }
    //        //else
    //        //{
    //        //    playerSight = false;
    //        //}

    //        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion"))
    //        {
    //            if (CalcuatePathLength(other.transform.position) <= collider.radius)
    //            {
    //                alertPosition = other.transform.position;
    //            }
    //        }

    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.player)
        {
            playerSight = false;
            //alertPosition = other.transform.position;
        }
    }

    float CalcuatePathLength(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();

        if (navAgent.enabled)
        {
            navAgent.CalculatePath(targetPosition, path);
            //if (navAgent.CalculatePath(targetPosition, path))
            //{

            //}
        }
        Vector3[] wayPoints = new Vector3[path.corners.Length + 2];
        wayPoints[0] = transform.position;
        wayPoints[wayPoints.Length - 1] = targetPosition;

        //Vector3[] wayPoints = new Vector3[path.corners.Length];
        for (int i = 0; i < path.corners.Length; i++)
        {
            wayPoints[i + 1] = path.corners[i];
        }

        float pathLength = 0f;
        for (int i = 0; i < wayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(wayPoints[i], wayPoints[i + 1]);
        }
        return pathLength;
    }
}
