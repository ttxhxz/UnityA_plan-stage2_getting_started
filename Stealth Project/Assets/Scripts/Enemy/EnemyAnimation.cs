using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour
{
    public float deadZone = 5f;

    private Transform player;
    private EnemySight enemySight;
    private NavMeshAgent nav;
    private Animator anim;
    private HashIDs hash;
    private AnimatorSetup animSetup;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();

        nav.updateRotation = false;
        animSetup = new AnimatorSetup(anim, hash);

        anim.SetLayerWeight(1, 1f);
        anim.SetLayerWeight(2, 1f);

        //从度转化为弧度
        deadZone *= Mathf.Deg2Rad;
    }

    private void Update()
    {
        NavAnimSetup();
    }

    private void OnAnimatorMove()
    {
        nav.velocity = anim.deltaPosition / Time.deltaTime;
        transform.rotation = anim.rootRotation;
    }

    void NavAnimSetup()
    {
        float speed;
        float angle;

        if (enemySight.playerInSight)
        {
            speed = 0f;
            angle = FindAngle(transform.forward, player.position - transform.position, transform.up);
        }
        else
        {
            speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;

            angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);

            if (Mathf.Abs(angle) < deadZone)
            {
                transform.LookAt(transform.position + nav.desiredVelocity);
                angle = 0f;
            }
        }

        animSetup.Setup(speed, angle);
    }

    float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector)
    {
        if (toVector == Vector3.zero)
        {
            return 0f;
        }

        float angle = Vector3.Angle(fromVector, toVector);
        Vector3 normal = Vector3.Cross(fromVector, toVector);
        //Mathf.Sign当参数为正或零时，返回值为1，当参数为负时，返回值为-1。
        angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
        angle *= Mathf.Deg2Rad;

        return angle;
    }

    #region 注释代码
    //private NavMeshAgent navAgent;
    //private Animator anim;

    //private int speedId = Animator.StringToHash("Speed");
    //private int angularspeedId = Animator.StringToHash("AngularSpeed");

    //private void Awake()
    //{
    //    navAgent = this.GetComponent<NavMeshAgent>();
    //    anim = this.GetComponent<Animator>();

    //}

    //private void Update()
    //{
    //    if (navAgent.desiredVelocity == Vector3.zero)
    //    {
    //        anim.SetFloat(speedId, 0);
    //        anim.SetFloat(angularspeedId, 0);
    //    }
    //    else
    //    {
    //        float angle = Vector3.Angle(transform.forward, navAgent.desiredVelocity);
    //        float angleRad = angle * Mathf.Deg2Rad;

    //        if (angle > 90)
    //        {
    //            anim.SetFloat(speedId, 0);
    //        }
    //        else
    //        {
    //            Vector3 projection = Vector3.Project(navAgent.desiredVelocity, transform.forward);
    //            anim.SetFloat(speedId, projection.magnitude);
    //        }
    //        Vector3 crossRes = Vector3.Cross(transform.forward, navAgent.desiredVelocity);
    //        if (crossRes.y < 0)
    //        {
    //            angleRad = -angleRad;
    //        }
    //        anim.SetFloat(angularspeedId, angleRad);
    //    }
    //    //Debug.Log(anim.GetFloat(speedId));
    //    //Debug.Log(anim.GetFloat(angularspeedId));
    //}
    #endregion
}
