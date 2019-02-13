using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MyText : MonoBehaviour
{

    private Text myText;
    // Use this for initialization
    void Start()
    {
        myText = this.GetComponent<Text>();
        myText.DOText("接下来，我们开始第二章|接下来，我们开始第二章|接下来，我们开始第二章|接下来，我们开始第二章", 5);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
