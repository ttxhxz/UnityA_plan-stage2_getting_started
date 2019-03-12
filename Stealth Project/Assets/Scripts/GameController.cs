using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController _instance;

    public bool alermOn = false;
    public Vector3 lastPlayerPosition = Vector3.zero;
    public float musicFadeSpeed = 1;
    public AudioSource musicNormal;
    public AudioSource musicPanic;

    private GameObject[] Sirens;
    
    void Awake()
    {
        _instance = this;
        Sirens = GameObject.FindGameObjectsWithTag(Tags.siren);
    }
    
    void Update()
    {
        AlermLight._instance.alermOn = this.alermOn;
        if (alermOn)
        {
            //缓慢切换普通背景音乐
            musicNormal.volume = Mathf.Lerp(musicNormal.volume, 0f, Time.deltaTime * musicFadeSpeed);
            musicPanic.volume = Mathf.Lerp(musicPanic.volume, 0.5f, Time.deltaTime * musicFadeSpeed);

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
            //缓慢切换警报背景音乐
            musicNormal.volume = Mathf.Lerp(musicNormal.volume, 0.5f, Time.deltaTime * musicFadeSpeed);
            musicPanic.volume = Mathf.Lerp(musicPanic.volume, 0f, Time.deltaTime * musicFadeSpeed);

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
