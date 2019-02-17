using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biker : MonoBehaviour
{

    private Animator ani;
    // Use this for initialization
    void Start()
    {
        ani = this.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        ani.SetInteger("Vertical", (int)v);
        //ani.SetFloat("Vertical01", v);

        Debug.Log("v:" + v + "\t " + (int)v);

    }
}
