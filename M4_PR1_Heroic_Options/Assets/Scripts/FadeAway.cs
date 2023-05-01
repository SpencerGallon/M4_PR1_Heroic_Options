using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeAway: MonoBehaviour
{
    public float delayTime = 30f;
    public float fadeTime = 1f;
    public CanvasGroup canvasGroup;
    private float timer = 0f;
    private bool isFading = false;

    void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= delayTime && !isFading)
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        isFading = true;
        float fadeAmount = Time.deltaTime / fadeTime;
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= fadeAmount;
            yield return null;
        }
        gameObject.SetActive(false);

        isFading = false;
    }
}