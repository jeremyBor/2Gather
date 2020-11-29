using Base.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private Transform _levelStart;

    [SerializeField]
    public int levelId = 1;

    public LevelEnd levelEnd;

    public List<GameObject> objectsToReset = null;

    public List<string> _keys;

    public int heats = 4;
    public float time = 0f;
    public int CollectedLeafs = 0;
    public float timeLose = 15f;
    public float timeWin = 9f;

    bool isSet = false;

    private void Awake()
    {
        LevelManager.Instance.RegisterCurentLevel(this);
    }

    void Start()
    {
        ResetLv();
    }

    public void DoUpdate()
    {
        time += Time.deltaTime;
    }

    public void ResetLv()
    {
        if (!isSet)
        {
            isSet = true;
            time = 0;
            LevelManager.Instance._playerInstance.transform.position = _levelStart.position;
            
            for (int i = 0; i < objectsToReset.Count; ++i)
            {
                objectsToReset[i].SetActive(true);
            }
        }
    }
}