using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Animator anim;

    private float speedDampTime = 0.3f;
    private float angularSpeedDampTime = 0.3f;
    private int speedId = Animator.StringToHash("Speed");
    private int angularspeedId = Animator.StringToHash("AngularSpeed");

    private void Awake()
    {
        navAgent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();

    }

    private void Update()
    {
        if (navAgent.desiredVelocity == Vector3.zero)
        {
            anim.SetFloat(speedId, 0);
            anim.SetFloat(angularspeedId, 0);
        }
        else
        {
            float angle = Vector3.Angle(transform.forward, navAgent.desiredVelocity);
            float angleRad = angle * Mathf.Deg2Rad;

            if (angle > 90)
            {
                anim.SetFloat(speedId, 0);
            }
            else
            {
                Vector3 projection = Vector3.Project(navAgent.desiredVelocity, transform.forward);
                anim.SetFloat(speedId, projection.magnitude);
            }
            Vector3 crossRes = Vector3.Cross(transform.forward, navAgent.desiredVelocity);
            if (crossRes.y < 0)
            {
                angleRad = -angleRad;
            }
            anim.SetFloat(angularspeedId, angleRad);
        }
        //Debug.Log(anim.GetFloat(speedId));
        //Debug.Log(anim.GetFloat(angularspeedId));
    }
}
