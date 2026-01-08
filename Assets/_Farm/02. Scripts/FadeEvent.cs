using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeEvent : MonoBehaviour
{
    private Image fadeImage;
    public static Action<float, Color, bool> fadeAction;

    void Awake()
    {
        fadeImage = transform.Find("Fade Image").GetComponent<Image>();
    }

    void OnEnable()
    {
        fadeAction += Fade;
    }

    void OnDisable()
    {
        fadeAction -= Fade;
    }

    public void Fade(float fadeTime, Color fadeColor, bool isFade)
    {
        fadeImage.gameObject.SetActive(true);
        
        StartCoroutine(FadeRoutine(fadeTime, fadeColor, isFade));
    }

    public IEnumerator FadeRoutine(float fadeTime, Color fadeColor, bool isFade)
    {
        float timer = 0f;
        float percent = 0f;
        while (percent < 1f)
        {
            timer += Time.deltaTime;
            percent = timer / fadeTime;

            float value = isFade ? percent : percent - 1; // Fade 또는 Unfade를 실행하기 위한 삼항 연산자
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, value);
            yield return null;
        }

        if (!isFade)
            fadeImage.gameObject.SetActive(false);
    }
}