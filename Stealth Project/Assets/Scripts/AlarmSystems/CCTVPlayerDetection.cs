using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVPlayerDetection : MonoBehaviour
{
    private GameObject player;
    private LastPlayerSighting lastPlayerSighting;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            Vector3 relPlayerPos = player.transform.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, relPlayerPos, out hit))
            {
                if (hit.collider.gameObject == player)
                {
                    lastPlayerSighting.position = player.transform.position;
                }
            }

            //GameController._instance.SeePlayer(other.transform.position);
        }
    }
}
