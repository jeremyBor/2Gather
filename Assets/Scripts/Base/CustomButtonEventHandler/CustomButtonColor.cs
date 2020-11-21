using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CustomButtonColor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private float _tweenTime = 1f;
    [SerializeField]
    private Color _to = Color.white;
    [SerializeField]
    private Ease ease = Ease.Linear;
    [SerializeField]
    private Image _image = null;

    private Tweener _tweener;

    private void Start()
    {
        if (_image == null)
        {
            _image = GetComponent<Image>();
        }
        _tweener = _image.DOColor(_to, _tweenTime).SetAutoKill(false).SetEase(ease);
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