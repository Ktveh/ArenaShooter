using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelName;
    [SerializeField] private Image _lockImage;
    [SerializeField] private Level[] _levels;

    private int _currentIndex;
    private bool _isLock;
    private int _scene;

    private void Start()
    {
        if (_levels.Length > 0)
        {
            ChangeLevel(_levels[0]);
            _currentIndex = 0;
        }
    }

    public void ChooseLevel()
    {
        if (!_isLock)
        {
            SceneManager.LoadScene(_scene);
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
        _levelName.text = level.Name;
        _isLock = level.IsLock;
        _scene = level.Scene;
        _lockImage.gameObject.SetActive(_isLock);
    }
}
