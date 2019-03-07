using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public bool isFlicker = false;
    public float onTime;
    public float offTime;

    private float timer = 0;

    private Renderer renderer;
    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlicker)
        {
            timer += Time.deltaTime;
            if (renderer.enabled)
            {
                if (timer > onTime)
                {
                    renderer.enabled = false;
                    timer = 0;
                }
            }
            else
            {
                if (timer > offTime)
                {
                    renderer.enabled = true;
                    timer = 0;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == Tags.player)
        {
            GameController._instance.SeePlayer(other.transform.position);
        }
    }
}
