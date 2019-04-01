using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    public bool requireKey;
    public AudioClip doorSwishClip;
    public AudioClip accessDeniedClip;

    private Animator anim;
    private AudioSource audio;
    private HashIDs hash;
    private GameObject player;
    private PlayerInventory playerInventory;
    //玩家和敌人都可以开门  count计算碰撞器中人物的个数
    private int count;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
        audio = this.GetComponent<AudioSource>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            //如果门需要钥匙开启
            if (requireKey)
            {
                //如果玩家有钥匙
                if (playerInventory.hasKey)
                {
                    count++;
                }
                else
                {
                    audio.clip = accessDeniedClip;
                    audio.Play();
                }
            }
            else
            {
                count++;
            }
        }
        else if (other.tag == Tags.enemy)
        {
            if (other is CapsuleCollider)
            {
                count++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject == player) || (other.tag == Tags.enemy && other is CapsuleCollider))
        {
            if (count > 0)
            {
                count = Mathf.Max(0, count - 1);
            }
        }
    }

    private void Update()
    {
        anim.SetBool(hash.openBool, count > 0);
        if (anim.IsInTransition(0) && !audio.isPlaying)
        {
            audio.clip = doorSwishClip;
            audio.Play();
        }
    }
}
