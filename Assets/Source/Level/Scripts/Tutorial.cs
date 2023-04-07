using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private TextMeshProUGUI _moveText;
    [SerializeField] private TextMeshProUGUI _rechargeText;
    [SerializeField] private TextMeshProUGUI _sprintText;
    [SerializeField] private TextMeshProUGUI _jumpText;
    [SerializeField] private TextMeshProUGUI _shotText;
    [SerializeField] private TextMeshProUGUI _aimText;
    [SerializeField] private TextMeshProUGUI _viewText;
    [SerializeField] private TextMeshProUGUI _weaponText;
    [SerializeField] private TextMeshProUGUI _grenadeText;

    private string _saveKey = "Levels";
    private int _firstLevel = 2;
    private int _currentLevel = 1;

    private void Start()
    {
        if (PlayerPrefs.GetInt(_saveKey) > _firstLevel)
        {
            StartGame();
        }
        else
        {
            Write();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_currentLevel);
    }

    public void SetCurrentLevel(int level)
    {
        _currentLevel = level;
    }

    private void Write()
    {
        _buttonText.text = Lean.Localization.LeanLocalization.GetTranslationText("Continue");
        _moveText.text = Lean.Localization.LeanLocalization.GetTranslationText("Move");
        _rechargeText.text = Lean.Localization.LeanLocalization.GetTranslationText("Recharge");
        _sprintText.text = Lean.Localization.LeanLocalization.GetTranslationText("Sprint");
        _jumpText.text = Lean.Localization.LeanLocalization.GetTranslationText("Jump");
        _shotText.text = Lean.Localization.LeanLocalization.GetTranslationText("Shot");
        _aimText.text = Lean.Localization.LeanLocalization.GetTranslationText("Aim");
        _viewText.text = Lean.Localization.LeanLocalization.GetTranslationText("View");
        _weaponText.text = Lean.Localization.LeanLocalization.GetTranslationText("Change Weapon");
        _grenadeText.text = Lean.Localization.LeanLocalization.GetTranslationText("Grenade");
    }
}