using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool alermOn = false;
    private GameObject[] Sirens;

    // Use this for initialization
    void Start()
    {
        Sirens = GameObject.FindGameObjectsWithTag("Siren");
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

}
