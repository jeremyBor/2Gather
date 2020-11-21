using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeLayoutElement : MonoBehaviour
{
    [SerializeField]
    private RectTransform _rectTransform;

    [SerializeField]
    [Range(0, 1)]
    private float _horisontalSize = 1;

    [SerializeField]
    [Range(0, 1)]
    private float _verticalSize = 1;

    private void Start()
    {
        Vector2 newSize = new Vector2((float)Screen.width * _horisontalSize, 
            (float)Screen.height * _verticalSize);
        _rectTransform.sizeDelta = newSize;
    }

}
