using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    #region 旧代码
    public float moveSpeed = 3;
    public float rotateSpeed = 7;
    private Animator anim;
    private AudioSource audio;
    public bool hasKey = false;

    private int speedId = Animator.StringToHash("Speed");
    private int sneakId = Animator.StringToHash("Sneaking");
    private int deadId = Animator.StringToHash("Dead");

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
        audio = this.GetComponent<AudioSource>();
    }

    private void Update()
    {
        #region 是否静走
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool(sneakId, true);
        }
        else
        {
            anim.SetBool(sneakId, false);
        }
        #endregion

        #region 人物行走
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //Debug.Log("v:" + v + "\th:" + h);

        if (Mathf.Abs(h) > 0.1 || Mathf.Abs(v) > 0.1)
        {
            //通过速度控制动画控制移动，不受h或者v影响,只要WASD按下就加速
            float newSpeed = Mathf.Lerp(anim.GetFloat(speedId), 5.6f, moveSpeed * Time.deltaTime);
            anim.SetFloat(speedId, newSpeed);

            //获取目标方向
            Vector3 targetDir = new Vector3(h, 0, v);
            //获取目标方向的四元数，绕着y轴旋转
            Quaternion newRotation = Quaternion.LookRotation(targetDir, Vector3.up);
            //通过Quaternion 的插值方式将玩家方向插值到目标位置
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetFloat(speedId, 0);
        }
        #endregion

        #region 脚步声
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion"))
        {
            PlayFootMusic();
        }
        else
        {
            StopFootMusic();
        }
        #endregion
    }

    private void PlayFootMusic()
    {
        if (!audio.isPlaying)
        {
            audio.Play();
        }
    }

    private void StopFootMusic()
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }
    }
    #endregion
}
