using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float moveSpeed = 3;
    public float rotateSpeed = 3;

    private Vector3 offset;
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        offset = transform.position - player.position;
        offset = new Vector3(0, offset.y, offset.z);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 beginPos = player.position + offset;

        Vector3 endPos = player.position + offset.magnitude * Vector3.up;
        Vector3 pos1 = Vector3.Lerp(beginPos, endPos, 0.25f);
        Vector3 pos2 = Vector3.Lerp(beginPos, endPos, 0.5f);
        Vector3 pos3 = Vector3.Lerp(beginPos, endPos, 0.75f);

        Vector3[] posArray = new Vector3[] { beginPos, pos1, pos2, pos3, endPos };
        Vector3 targetPos = posArray[0];
        for (int i = 0; i < posArray.Length; i++)
        {
            RaycastHit hitinfo;
            //射线的方向
            Vector3 dir = player.position - posArray[i];
            if (Physics.Raycast(posArray[i], dir, out hitinfo))
            {
                Debug.DrawRay(posArray[i], dir, Color.yellow, 0.0f, true);
                if (hitinfo.collider.tag == Tags.player)
                {
                    targetPos = posArray[i];
                    break;
                }
            }
        }

        //transform.position = targetPos;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);

        //transform.LookAt(player.position);
        //
        Quaternion nowRotation = transform.rotation;
        transform.LookAt(player.position);
        transform.rotation = Quaternion.Lerp(nowRotation, transform.rotation, Time.deltaTime * rotateSpeed);
    }
}
