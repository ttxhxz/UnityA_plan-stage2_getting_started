using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Panel2 : MonoBehaviour {

    private DOTweenAnimation animation;
    private bool isShow = false;

	// Use this for initialization
	void Start () {
        animation = this.GetComponent<DOTweenAnimation>();
        //animation.DOPlay();

    }
	
	public void OnClick()
    {
        if (isShow)
        {
            animation.DOPlayBackwards();
            isShow = false;
        }
        else
        {
            animation.DOPlayForward();
            isShow = true;
        }
    }
}
