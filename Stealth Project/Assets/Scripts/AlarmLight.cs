using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLight : MonoBehaviour
{
    public static AlarmLight _instance;

    public float fadeSpeed = 2f;
    private float hightIntensity = 2f;
    public float lowIntensity = 0.5f;
    public float changeMargin = 0.2f;
    public bool alermOn;
    private float targetIntensity;

    private Light light;

    private void Awake()
    {
        light = this.GetComponent<Light>();
        light.intensity = 0;
        targetIntensity = hightIntensity;

        //alermOn = false;

        _instance = this;
    }

    void Update()
    {
        if (alermOn)
        {
            light.intensity = Mathf.Lerp(light.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
            CheckTargetIntensity();
        }
        else
        {
            light.intensity = Mathf.Lerp(light.intensity, 0f, fadeSpeed * Time.deltaTime);
        }
    }

    void CheckTargetIntensity()
    {
        if (Mathf.Abs(light.intensity - targetIntensity) < changeMargin)
        {
            if (targetIntensity == hightIntensity)
            {
                targetIntensity = lowIntensity;
            }
            else
            {
                targetIntensity = hightIntensity;
            }
        }
    }
}
