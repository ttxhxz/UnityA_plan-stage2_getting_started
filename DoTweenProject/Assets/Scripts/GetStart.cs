using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GetStart : MonoBehaviour
{
    public Vector3 myValue = new Vector3(0, 0, 0);

    public Transform cubeTransform;

    public RectTransform taskPanelTransform;

    public float myValue2 = 0;
    // Use this for initialization
    void Start()
    {
        DOTween.To(() => myValue, x => myValue = x, new Vector3(0, 0, 0), 2.0f);
        DOTween.To(() => myValue2, x => myValue2 = x, 10, 2);

    }

    // Update is called once per frame
    void Update()
    {
        //cubeTransform.position = myValue;
        taskPanelTransform.localPosition = myValue;
    }
}
