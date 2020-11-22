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

    void Awake()
    {
        SceneManager.LoadSceneAsync(_levels[_curentlevelIndex], LoadSceneMode.Additive);
    }

    public void NextLevel()
    {
        SceneManager.UnloadSceneAsync(_levels[_curentlevelIndex]);
        _curentlevelIndex++;
        SceneManager.LoadSceneAsync(_levels[_curentlevelIndex], LoadSceneMode.Additive);
    }
}
