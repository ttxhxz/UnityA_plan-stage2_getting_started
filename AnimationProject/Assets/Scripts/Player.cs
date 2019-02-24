using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Vector3 matchTarget;

    private Animator ani;
    private CharacterController controller;

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
                    Debug.Log(matchTarget);
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
}
