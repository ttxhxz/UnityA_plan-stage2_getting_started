using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private int closeId = Animator.StringToHash("Close");
    private Animator anim;
    private AudioSource audio;
    private int count = 0;//在Collider中的人物数量

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
        audio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool(closeId, count <= 0);
        if (anim.IsInTransition(0))
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.enemy || other.tag == Tags.player)
        {
            count++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.enemy || other.tag == Tags.player)
        {
            count--;
        }
    }
}
