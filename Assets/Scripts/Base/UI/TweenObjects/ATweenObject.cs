using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ATweenObject : MonoBehaviour
{
    [SerializeField]
    private Ease _ease = Ease.Linear;
    [SerializeField]
    private bool _playOnEnable = true;


    private Tweener _tweener;

    protected virtual void OnEnable()
    {
        if(_playOnEnable)
        {
            _tweener.PlayForward();
        }
    }

    protected virtual void OnDisable()
    {
        _tweener.Kill();
    }
}
