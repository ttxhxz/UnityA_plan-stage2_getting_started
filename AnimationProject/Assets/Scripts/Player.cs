﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Player : MonoBehaviour
{
    //private int verticalId = Animator.StringToHash("Vertical");
    //private int horizontalId = Animator.StringToHash("Horizontal");
    //private int IsSpeedUpId = Animator.StringToHash("IsSpeedUp");

    private int SpeedRotateId = Animator.StringToHash("SpeedRotate");
    private int SpeedZId = Animator.StringToHash("SpeedZ");
    private int VaultId = Animator.StringToHash("Vault");
    private int SliderId = Animator.StringToHash("Slider");
    private int ColliderId = Animator.StringToHash("Collider");
    private int IsHoldLogId = Animator.StringToHash("IsHoldLog");

    private Animator ani;
    private CharacterController controller;

    public Vector3 matchTarget = Vector3.zero;
    public GameObject Log = null;
    public Transform LeftHand;
    public Transform RightHand;
    public PlayableDirector director;

    void Start()
    {
        ani = this.GetComponent<Animator>();
        controller = this.GetComponent<CharacterController>();
    }


    void Update()
    {
        ani.SetFloat(SpeedRotateId, Input.GetAxis("Horizontal") * 126);
        ani.SetFloat(SpeedZId, Input.GetAxis("Vertical") * 4.1f);

        //翻墙动作
        ProcessVault();

        //滑行动作
        ProcessSlider();


        #region 注释代码

        //ani.SetFloat("Vertical", Input.GetAxis("Vertical"));
        //ani.SetFloat(verticalId, Input.GetAxis("Vertical"));//StringToHash把参数字符串转换成ID 前后
        //ani.SetFloat(horizontalId, Input.GetAxis("Horizontal"));//左右

        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    ani.SetBool(IsSpeedUpId, true);
        //}

        //if (Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    ani.SetBool(IsSpeedUpId, false);
        //}

        //ani.SetFloat(verticalId, Input.GetAxis("Vertical") * 4.1f);//StringToHash把参数字符串转换成ID 前后
        #endregion

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Log")
        {
            Destroy(other.gameObject);
            CarryWood();
        }
        if (other.tag == "Playable")
        {
            director.Play();
        }
    }

    void CarryWood()
    {
        Log.SetActive(true);
        ani.SetBool(IsHoldLogId, true);
    }

    //翻墙动作
    private void ProcessVault()
    {
        bool isVault = false;
        //当速度大于3并且动画状态在第0层的Localmotion的时候才判断是否起跳
        if (SpeedZId > 3 && ani.GetCurrentAnimatorStateInfo(0).IsName("Localmotion"))
        {
            RaycastHit hit;
            bool isHit = Physics.Raycast(transform.position + Vector3.up * 0.3f, transform.forward, out hit, 4f);
            if (isHit && hit.collider.tag == "Obstacle")
            {
                if (hit.distance > 3f)
                {
                    isVault = true;
                    Vector3 point = hit.point;
                    //重置碰撞点的y为  (hit.transform.position.y + hit.collider.bounds.size.y) 代表障碍物的初始y值加上障碍物的y
                    point.y = (hit.transform.position.y + hit.collider.bounds.size.y) + 0.11f;
                    //为了扶到墙的中间，而不是墙的边缘加上墙的z的一半
                    //point.z = hit.transform.position.z + hit.collider.bounds.size.z / 2;
                    matchTarget = point;
                }
            }
        }
        ani.SetBool(VaultId, isVault);

        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Vault") && ani.IsInTransition(0) == false)
        {
            ani.MatchTarget(matchTarget, Quaternion.identity, AvatarTarget.LeftHand, new MatchTargetWeightMask(Vector3.one, 0), 0.32f, 0.4f);

            controller.enabled = ani.GetFloat(ColliderId) <= 0.5;
        }
    }

    //滑行动作
    private void ProcessSlider()
    {
        bool isSlider = false;
        if (SpeedZId > 3 && ani.GetCurrentAnimatorStateInfo(0).IsName("Localmotion"))
        {
            RaycastHit hit;
            bool isHit = Physics.Raycast(transform.position + Vector3.up * 1.5f, transform.forward, out hit, 3.0f);
            if (isHit && hit.collider.tag == "Obstacle")
            {
                if (hit.distance > 2)
                {
                    isSlider = true;
                    Vector3 point = hit.point;
                    matchTarget = point + transform.forward * 2.0f;
                }
            }
        }
        ani.SetBool(SliderId, isSlider);
        //滑行动作取消controller
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Slider") && ani.IsInTransition(0) == false)
        {
            ani.MatchTarget(matchTarget, Quaternion.identity, AvatarTarget.LeftHand, new MatchTargetWeightMask(new Vector3(1, 0, 1), 0), 0.16f, 0.67f);
            controller.enabled = ani.GetFloat(ColliderId) <= 0.5;
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (layerIndex == 1)
        {
            //通过是否拾取木头来赋值。。返回true赋值权重为1.否则权重为0
            int weight = ani.GetBool(IsHoldLogId) ? 1 : 0;

            //设置左手IK的坐标和旋转以及权重
            ani.SetIKPosition(AvatarIKGoal.LeftHand, LeftHand.position);
            ani.SetIKPositionWeight(AvatarIKGoal.LeftHand, weight);
            ani.SetIKRotation(AvatarIKGoal.LeftHand, LeftHand.rotation);
            ani.SetIKRotationWeight(AvatarIKGoal.LeftHand, weight);

            ani.SetIKPosition(AvatarIKGoal.RightHand, RightHand.position);
            ani.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
            ani.SetIKRotation(AvatarIKGoal.RightHand, RightHand.rotation);
            ani.SetIKRotationWeight(AvatarIKGoal.RightHand, weight);
        }
    }
}
