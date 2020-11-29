using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPanel : UIPanel
{
    public GameObject[] leafs;
    public GameObject[] hearts;
    public GameObject[] time;
    public override void Show()
    {
        base.Show();
        //Level.Instance.heats;
        for(int i = 0; i< leafs.Length; ++i)
        {
            leafs[i].SetActive(LevelManager.Instance._currentLevel.CollectedLeafs > i);
        }
        for (int i = 0; i < hearts.Length; ++i)
        {
            hearts[i].SetActive(LevelManager.Instance._currentLevel.heats > i);
        }

        float finishTime = LevelManager.Instance._currentLevel.time;
        finishTime -= LevelManager.Instance._currentLevel.timeWin;

        for (int i = 0; i < time.Length; ++i)
        {
            if (LevelManager.Instance._currentLevel.time < LevelManager.Instance._currentLevel.timeLose)
            {
                time[i].SetActive(Mathf.CeilToInt(Mathf.Lerp(0, 3, finishTime / (LevelManager.Instance._currentLevel.timeLose - LevelManager.Instance._currentLevel.timeWin))) <= i);
            }
            else
            {
                time[i].SetActive(false);
            }
        }
            
    }
}