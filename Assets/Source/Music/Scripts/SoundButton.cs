using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private Sprite _soundOn;
    [SerializeField] private Sprite _soundOff;
    [SerializeField] private Button _soundButton;
    [SerializeField] private AudioListener _soundListener;
    [SerializeField] private bool _hasText;
    [SerializeField] private TextMeshProUGUI _buttonText;

    private static bool _isOn;
    private static bool _isAdShow;

    private const string SoundOn = "Sound On";
    private const string SoundOff = "Sound Off";

    private void OnEnable()
    {
        _soundButton.onClick.AddListener(OnButtonClick);
        CheckButtonSprite();
    }

    private void OnDisable()
    {
        _soundButton.onClick.RemoveListener(OnButtonClick);
    }

    private void Start()
    {
        CheckVolume();
    }

    public void OnAdShow()
    {
        _isAdShow = true;
        AudioListener.volume = 0;
    }

    public void OnAdClose()
    {
        _isAdShow = false;
        if (_isOn)
        {
            AudioListener.volume = 1;
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (_isOn)
        {
            if (focus)
            {
                AudioListener.volume = 1;
            }
            else
            {
                AudioListener.volume = 0;
            }
        }

        if (_isAdShow)
        {
            AudioListener.volume = 0;
        }
    }

    private void OnButtonClick()
    {
        if (_isOn)
        {
            _isOn = false;
            AudioListener.volume = 0;
        }
        else
        {
            _isOn = true;
            AudioListener.volume = 1;
        }

        if (_isAdShow)
        {
            AudioListener.volume = 0;
        }

        CheckButtonSprite();
    }

    private void CheckButtonSprite()
    {
        if (_isOn)
        {
            _soundButton.image.sprite = _soundOn;
            if (_hasText)
                _buttonText.text = Lean.Localization.LeanLocalization.GetTranslationText(SoundOn);
        }
        else
        {
            _soundButton.image.sprite = _soundOff;
            if (_hasText)
                _buttonText.text = Lean.Localization.LeanLocalization.GetTranslationText(SoundOff);
        }
    }

    private void CheckVolume()
    {
        if (!_isOn)
        {
            AudioListener.volume = 0;
        }
    }
}
