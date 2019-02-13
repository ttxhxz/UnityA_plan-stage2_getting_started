using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyPanel : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Tween tween = transform.DOLocalMoveX(0, 2);
        tween.SetEase(Ease.InBounce);
        tween.SetLoops(2);
        tween.OnComplete(OnTweenComplete);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTweenComplete()
    {
        Debug.Log("动画播放完成");

    }
}
