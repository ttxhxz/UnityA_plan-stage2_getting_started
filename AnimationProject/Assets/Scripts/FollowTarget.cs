using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;

    private float smoothing = 3;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = this.transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 target = player.position + player.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * smoothing);
        transform.LookAt(player);

    }
}
