using Base.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviourSingleton<Level>
{
    [SerializeField]
    private Transform _levelStart;

    [SerializeField]
    private CharacterController2D _playerPrefab;

    private CharacterController2D _playerInstance = null;

    public List<GameObject> objectsToReset = null;

    public int heats = 4;
    public int time = 90;
    public int CollectedLeafs = 0;



    void Awake()
    {
        ResetLv();
    }

    public void ResetLv()
    {
        _playerInstance = Instantiate<CharacterController2D>(_playerPrefab, _levelStart.position, Quaternion.identity);
        for(int i = 0; i < objectsToReset.Count; ++i)
        {
            objectsToReset[i].SetActive(true);
        }
    }


}
