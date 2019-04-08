using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{
    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
    public Vector3 personalLastSighting;

    private NavMeshAgent nav;
    private SphereCollider col;
    private Animator anim;
    private LastPlayerSighting lastPlayerSighting;
    private GameObject player;
    private Animator playerAnim;
    private PlayerHealth playerHealth;
    private HashIDs hash;
    private Vector3 previousSighting;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerAnim = player.GetComponent<Animator>();
        playerHealth = player.GetComponent<PlayerHealth>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();

        personalLastSighting = lastPlayerSighting.resetPosition;
        previousSighting = lastPlayerSighting.resetPosition;
    }

    private void Update()
    {
        if (lastPlayerSighting.position != previousSighting)
        {
            personalLastSighting = lastPlayerSighting.position;
        }

        previousSighting = lastPlayerSighting.position;

        if (playerHealth.health > 0f)
        {
            anim.SetBool(hash.playerInSightBool, playerInSight);
        }
        else
        {
            anim.SetBool(hash.playerInSightBool, false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = false;

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
                {
                    if (hit.collider.gameObject == player)
                    {
                        playerInSight = true;
                        lastPlayerSighting.position = player.transform.position;
                    }
                }
            }

            int playerLayerZeroStateHash = playerAnim.GetCurrentAnimatorStateInfo(0).fullPathHash;
            int playerLayerOneStateHash = playerAnim.GetCurrentAnimatorStateInfo(1).fullPathHash;

            if(playerLayerZeroStateHash==hash.locomotionState|| playerLayerOneStateHash == hash.shoutState)
            {
                if (CalculatePathLength(player.transform.position) <= col.radius)
                {
                    personalLastSighting = player.transform.position;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = false;
        }
    }

    float CalculatePathLength(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();

        if (nav.enabled)
        {
            nav.CalculatePath(targetPosition, path);
        }

        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        float pathLength = 0f;

        for (int i = 0; i < allWayPoints.Length-1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
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

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == Tags.player)
    //    {
    //        playerSight = false;
    //        //alertPosition = other.transform.position;
    //    }
    //}

    //float CalcuatePathLength(Vector3 targetPosition)
    //{
    //    NavMeshPath path = new NavMeshPath();

    //    if (navAgent.enabled)
    //    {
    //        navAgent.CalculatePath(targetPosition, path);
    //        //if (navAgent.CalculatePath(targetPosition, path))
    //        //{

    //        //}
    //    }
    //    Vector3[] wayPoints = new Vector3[path.corners.Length + 2];
    //    wayPoints[0] = transform.position;
    //    wayPoints[wayPoints.Length - 1] = targetPosition;

    //    //Vector3[] wayPoints = new Vector3[path.corners.Length];
    //    for (int i = 0; i < path.corners.Length; i++)
    //    {
    //        wayPoints[i + 1] = path.corners[i];
    //    }

    //    float pathLength = 0f;
    //    for (int i = 0; i < wayPoints.Length - 1; i++)
    //    {
    //        pathLength += Vector3.Distance(wayPoints[i], wayPoints[i + 1]);
    //    }
    //    return pathLength;
    //}
}
