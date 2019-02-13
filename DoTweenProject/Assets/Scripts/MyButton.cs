using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyButton : MonoBehaviour
{

    public RectTransform panelTransform;

    private bool isIn = false;

    private void Start()
    {
        Tween tween = panelTransform.DOLocalMove(new Vector3(0, 0, 0), 0.3f);//本地坐标
        tween.SetAutoKill(false);
        tween.Pause();
    }

    public void OnClick()
    {
        if (isIn == false)
        {
            //panelTransform.DOMove(new Vector3(0, 0, 0), 1f);//世界坐标
            //panelTransform.DOPlay();
            panelTransform.DOPlayForward();
            isIn = true;
        }
        else
        {
            panelTransform.DOPlayBackwards();
            isIn = false;
        }

    }
}
