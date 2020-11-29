using Base.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
    [SerializeField]
    private string[] _levels;

    private int _curentlevelIndex = 0;

    internal Level _currentLevel;

    [SerializeField]
    private CharacterController2D _playerPrefab;

    internal CharacterController2D _playerInstance = null;

    public System.Action OnLevelLoaded;

    public bool isLevelLoaded = false;

    void Awake()
    {
        SceneManager.LoadSceneAsync(_levels[_curentlevelIndex], LoadSceneMode.Additive);
        _playerInstance = Instantiate<CharacterController2D>(_playerPrefab, Vector3.zero, Quaternion.identity);
    }

    public void NextLevel()
    {
        isLevelLoaded = false;
        SceneManager.UnloadSceneAsync(_levels[_curentlevelIndex]);
        _curentlevelIndex++;
        SceneManager.LoadSceneAsync(_levels[_curentlevelIndex], LoadSceneMode.Additive);

    }

    public void RegisterCurentLevel(Level a_level)
    {
        _currentLevel = a_level;
        isLevelLoaded = true;
        if (OnLevelLoaded != null)
            OnLevelLoaded();
    }
}
