using Base.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviourSingleton<Level>
{
    [SerializeField]
    private Transform _levelStart;

    [SerializeField]
    private int levelId = 1;

    [SerializeField]
    private CharacterController2D _playerPrefab;

    public CharacterController2D _playerInstance = null;

    public LevelEnd levelEnd;

    public List<GameObject> objectsToReset = null;

    public List<string> _keys;

    public int heats = 4;
    public float time = 0f;
    public int CollectedLeafs = 0;
    public float timeLose = 15f;
    public float timeWin = 9f;

    void Awake()
    {
        ResetLv();
    }

    public void DoUpdate()
    {
        time += Time.deltaTime;
    }

    public void ResetLv()
    {
        time = 0;
        _playerInstance = Instantiate<CharacterController2D>(_playerPrefab, _levelStart.position, Quaternion.identity);
        for(int i = 0; i < objectsToReset.Count; ++i)
        {
            objectsToReset[i].SetActive(true);
        }
    }


}
