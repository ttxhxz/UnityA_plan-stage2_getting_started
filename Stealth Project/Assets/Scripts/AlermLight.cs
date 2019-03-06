using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlermLight : MonoBehaviour
{
    public static AlermLight _instance;

    public bool alermOn;

    private float lowIntensity = 0f;
    private float hightIntensity = 1f;
    private float targetIntensity;

    public float animationSpeed = 3f;
    private Light light;

    private void Awake()
    {
        alermOn = false;
        targetIntensity = hightIntensity;
        light = this.GetComponent<Light>();

        _instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (alermOn)
        {
            light.intensity = Mathf.Lerp(light.intensity, targetIntensity, Time.deltaTime * animationSpeed);
            if (Mathf.Abs(light.intensity - targetIntensity) < 0.05f)
            {
                if (targetIntensity == hightIntensity)
                {
                    targetIntensity = lowIntensity;
                }
                else if (targetIntensity == lowIntensity)
                {
                    targetIntensity = hightIntensity;
                }
            }
        }
        else
        {
            light.intensity = Mathf.Lerp(light.intensity, lowIntensity, Time.deltaTime * animationSpeed);
        }
    }
}
