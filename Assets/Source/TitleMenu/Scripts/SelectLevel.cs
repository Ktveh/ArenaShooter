using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] private Canvas _tutorial;
    [SerializeField] private TextMeshProUGUI _titleName;
    [SerializeField] private TextMeshProUGUI _levelName;
    [SerializeField] private Image _lockImage;
    [SerializeField] private Image _levelImage;
    [SerializeField] private Level[] _levels;

    public static string SaveKey = "Levels";
    private string _nameOfGame = "Arena Shooter";
    private int _currentIndex;
    private bool _isLock;
    private int _scene;

    private void Start()
    {
        _titleName.text = Lean.Localization.LeanLocalization.GetTranslationText(_nameOfGame);
        if (_levels.Length > 0)
        {
            ChangeLevel(_levels[0]);
            _currentIndex = 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ChooseLevel();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            PreviousLevel();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            NextLevel();
        }
    }

    public void ChooseLevel()
    {
        if (!_isLock)
        {
            if (PlayerPrefs.GetInt(SaveKey) > 1)
            {
                SceneManager.LoadScene(_scene);
            }
            else
            {
                _tutorial.gameObject.SetActive(true);
            }
        }
    }

    public void PreviousLevel()
    {
        _currentIndex--;
        if (_currentIndex < 0)
        {
            _currentIndex = _levels.Length - 1;
        }
        ChangeLevel(_levels[_currentIndex]);
    }

    public void NextLevel()
    {
        _currentIndex++;
        if (_currentIndex > _levels.Length - 1)
        {
            _currentIndex = 0;
        }
        ChangeLevel(_levels[_currentIndex]);
    }

    private void ChangeLevel(Level level)
    {
        _levelName.text = Lean.Localization.LeanLocalization.GetTranslationText(level.Name);
        _isLock = level.IsLock;
        _scene = level.Scene;
        _levelImage.sprite = level.Icon;
        _lockImage.gameObject.SetActive(_isLock);
    }
}
