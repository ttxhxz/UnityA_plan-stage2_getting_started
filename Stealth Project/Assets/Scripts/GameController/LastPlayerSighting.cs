using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlayerSighting : MonoBehaviour
{
    //玩家坐标
    public Vector3 position = new Vector3(1000f, 1000f, 1000f);
    //玩家重置坐标
    public Vector3 resetPosition = new Vector3(1000f, 1000f, 1000f);
    //主灯光(mainLight)亮度最大,最小值,更改速度，音乐更改速度
    public float lightHighIntensity = 0.75f;
    public float lightLowIntensity = 0f;
    public float fadeSpeed = 7f;
    public float musicFadeSpeed = 1f;

    private AlarmLight alarm;
    private Light mainLight;
    private AudioSource musicNormal;
    private AudioSource musicPanic;
    private AudioSource[] sirens;

    private void Awake()
    {
        alarm = GameObject.FindGameObjectWithTag(Tags.alarm).GetComponent<AlarmLight>();
        mainLight = GameObject.FindGameObjectWithTag(Tags.mainLight).GetComponent<Light>();
        musicNormal = this.GetComponent<AudioSource>();
        musicPanic = transform.Find("secondaryMusic").GetComponent<AudioSource>();
        GameObject[] sirenGamObjects = GameObject.FindGameObjectsWithTag(Tags.siren);
        sirens = new AudioSource[sirenGamObjects.Length];

        for (int i = 0; i < sirens.Length; i++)
        {
            sirens[i] = sirenGamObjects[i].GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        SwitchAlarms();
        MusicFading();
    }

    void SwitchAlarms()
    {
        alarm.alermOn = (position != resetPosition);

        float newIntensity;
        if (position != resetPosition)
        {
            newIntensity = lightLowIntensity;
        }
        else
        {
            newIntensity = lightHighIntensity;
        }

        mainLight.intensity = Mathf.Lerp(mainLight.intensity, newIntensity, fadeSpeed * Time.deltaTime);

        for (int i = 0; i < sirens.Length; i++)
        {
            if (position != resetPosition && !sirens[i].isPlaying)
            {
                sirens[i].Play();
            }
            else if (position == resetPosition)
            {
                sirens[i].Stop();
            }
        }
    }

    void MusicFading()
    {
        if (position != resetPosition)
        {
            musicNormal.volume = Mathf.Lerp(musicNormal.volume, 0f, musicFadeSpeed * Time.deltaTime);
            musicPanic.volume = Mathf.Lerp(musicPanic.volume, 0.5f, musicFadeSpeed * Time.deltaTime);
        }
        else
        {
            musicNormal.volume = Mathf.Lerp(musicNormal.volume, 0.5f, musicFadeSpeed * Time.deltaTime);
            musicPanic.volume = Mathf.Lerp(musicPanic.volume, 0f, musicFadeSpeed * Time.deltaTime);
        }
    }
}
