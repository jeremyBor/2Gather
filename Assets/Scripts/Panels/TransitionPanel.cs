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
            leafs[i].SetActive(Level.Instance.CollectedLeafs > i);
        }
        for (int i = 0; i < hearts.Length; ++i)
        {
            leafs[i].SetActive(Level.Instance.heats > i);
        }

        float finishTime = Level.Instance.time;
        finishTime -= Level.Instance.timeWin;
        for (int i = 0; i < time.Length; ++i)
        {
            time[i].SetActive((int)Mathf.Lerp(0, 4, finishTime / (Level.Instance.timeLose - Level.Instance.timeWin)) > i);
        }
            
    }
}