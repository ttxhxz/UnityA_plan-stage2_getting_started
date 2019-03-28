using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlinking : MonoBehaviour
{
    public float onTime;
    public float offTime;

    private float timer;

    private Renderer renderer;
    private Light light;

    private void Awake()
    {
        renderer = this.GetComponent<Renderer>();
        light = this.GetComponent<Light>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (renderer.enabled && timer >= onTime)
        {
            SwitchBeam();
        }

        if (!renderer.enabled && timer >= offTime)
        {
            SwitchBeam();
        }
    }

    void SwitchBeam()
    {
        timer = 0f;

        renderer.enabled = !renderer.enabled;
        light.enabled = !light.enabled;
    }
}
