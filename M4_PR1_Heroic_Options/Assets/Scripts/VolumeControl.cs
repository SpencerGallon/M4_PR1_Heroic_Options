using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] string _volumeParameter = "MasterVolume";
    [SerializeField] AudioMixer _mixer;
    [SerializeField] UnityEngine.UI.Slider _slider;
    [SerializeField] float _multiplier = 30f;
    [SerializeField] private UnityEngine.UI.Toggle _toggle;
    private bool _disableToggleEvent;

    void Awake()
    {
        _slider.onValueChanged.AddListener(HandleSliderValueChanged);
        _toggle.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    void HandleSliderValueChanged(bool enableSound)
    {
        if (_disableToggleEvent)
            return;
        if (enableSound)
            _slider.value = _slider.maxValue;
        else
            _slider.value = _slider.minValue;
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeParameter, _slider.value);
    }

    void HandleSliderValueChanged(float value)
    {
        _mixer.SetFloat(_volumeParameter, Mathf.Log10(value) * _multiplier);
        _disableToggleEvent = true;
        _toggle.isOn = _slider.value > _slider.minValue;
        _disableToggleEvent = false;
    }

    void Start()
    {
        _slider.value = PlayerPrefs.GetFloat(_volumeParameter, _slider.value);
    }
}