using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //rotate
        //transform.DOLookAt(Vector3.one, 2);

        //scale
        //transform.DOScale(2, 1);

        transform.DOPunchPosition(new Vector3(0, 1, 0), 2, 2, 1);

	}
}
