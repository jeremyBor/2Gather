using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private Transform _levelStart;

    [SerializeField]
    private CharacterController2D _playerPrefab;

    private CharacterController2D _playerInstance = null;

    void Awake()
    {
        _playerInstance = Instantiate<CharacterController2D>(_playerPrefab, _levelStart.position, Quaternion.identity);
    }

}
