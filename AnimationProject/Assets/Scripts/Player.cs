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

    private Animator ani;
    // Use this for initialization
    void Start()
    {
        ani = this.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        ani.SetFloat(SpeedRotateId, Input.GetAxis("Horizontal") * 126);
        ani.SetFloat(SpeedZId, Input.GetAxis("Vertical") * 4.1f);

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

    }
}
