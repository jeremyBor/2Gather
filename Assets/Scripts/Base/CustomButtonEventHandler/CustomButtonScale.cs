using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CustomButtonScale : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private float _tweenTime = 1f;
    [SerializeField]
    private Vector3 _to = Vector3.one;
    [SerializeField]
    private Ease ease = Ease.Linear;

    private Tweener _tweener;

    private void Start()
    {
        _tweener = transform.DOScale(_to, _tweenTime).SetAutoKill(false).SetEase(ease);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _tweener.PlayForward();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _tweener.PlayBackwards();
    }
}
