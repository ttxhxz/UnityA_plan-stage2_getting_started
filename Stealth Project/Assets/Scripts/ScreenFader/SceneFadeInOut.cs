using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFadeInOut : MonoBehaviour
{
    public float fadeSpeed = 1.5f;
    private bool sceneStarting = true;
    private Image image;

    private void Awake()
    {
        image = this.GetComponent<Image>();
    }

    private void Update()
    {
        if (sceneStarting)
        {
            StartScene();
        }
    }

    void FadeToClear()
    {
        image.color = Color.Lerp(image.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    void FadeToBlack()
    {
        image.color = Color.Lerp(image.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    void StartScene()
    {
        FadeToClear();
        if (image.color.a <= 0.05f)
        {
            image.color = Color.clear;
            image.enabled = false;
            sceneStarting = false;
        }
    }

    public void EndScene()
    {
        image.enabled = true;
        FadeToBlack();
        if (image.color.a >= 0.95f)
        {
            SceneManager.LoadScene(0);
        }
    }
}
