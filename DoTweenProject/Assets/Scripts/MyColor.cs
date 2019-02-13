using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MyColor : MonoBehaviour {

    private Text text;
	// Use this for initialization
	void Start () {
        text = this.GetComponent<Text>();
        //text.DOColor(Color.red, 2);
        text.DOFade(1, 2);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
