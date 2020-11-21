using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIPanel : MonoBehaviour
{
    protected bool _isShown;
    protected CanvasGroup _canvasGroup;

    protected CanvasGroup CanvasGroup => _canvasGroup;

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.interactable = false;
    }

    public virtual void ResetPanel()
    {
    }

    public virtual void Show()
    {
        _isShown = true;

        gameObject.SetActive(true);
        _canvasGroup.interactable = true;
    }

    public virtual void Hide()
    {
        if (!_isShown)
        {
            return;
        }

        _isShown = false;

        _canvasGroup.interactable = false;
        gameObject.SetActive(false);
    }
}