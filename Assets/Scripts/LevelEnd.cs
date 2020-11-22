using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public Action OnLevelEnd;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (OnLevelEnd != null)
            OnLevelEnd();
    }
}
