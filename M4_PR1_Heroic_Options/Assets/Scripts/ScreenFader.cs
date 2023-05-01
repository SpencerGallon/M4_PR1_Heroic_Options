using System;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader:MonoBehaviour
{
    public static ScreenFader Instance { get; private set; }

    private Image _image;
    private Action _onFadeComplete;

    private void Awake()
    {
        Instance = this;
        _image = GetComponentInChildren<Image>();
        _image.gameObject.SetActive(false);
    }

    public void FadeToBlack(float duration, Action onFadeComplete)
    {
        _onFadeComplete = onFadeComplete;
        _image.gameObject.SetActive(true);
        _image.CrossFadeAlpha(1f, duration, false);
        Invoke(nameof(OnFadeComplete), duration);
    }

    private void OnFadeComplete()
    {
        _onFadeComplete?.Invoke();
        _onFadeComplete = null;
        _image.CrossFadeAlpha(0f, 0f, false);
        _image.gameObject.SetActive(false);
    }
}