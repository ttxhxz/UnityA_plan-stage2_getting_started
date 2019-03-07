using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController _instance;

    public bool alermOn = false;
    public Vector3 lastPlayerPosition = Vector3.zero;
    private GameObject[] Sirens;

    // Use this for initialization
    void Start()
    {
        _instance = this;
        Sirens = GameObject.FindGameObjectsWithTag(Tags.siren);
    }

    // Update is called once per frame
    void Update()
    {
        AlermLight._instance.alermOn = this.alermOn;
        if (alermOn)
        {
            foreach (GameObject item in Sirens)
            {
                AudioSource audio = item.GetComponent<AudioSource>();
                if (!audio.isPlaying)
                {
                    audio.Play();
                }
            }
        }
        else
        {
            foreach (GameObject item in Sirens)
            {
                item.GetComponent<AudioSource>().Stop();
            }
        }
    }

    /// <summary>
    /// 警报响起，更新玩家位置
    /// </summary>
    /// <param name="playerPosition">玩家最后出现的坐标</param>
    public void SeePlayer(Vector3 playerPosition)
    {
        alermOn = true;
        lastPlayerPosition = playerPosition;
    }
}
