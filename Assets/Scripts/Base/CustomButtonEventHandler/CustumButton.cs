using UnityEngine;
using UnityEngine.EventSystems;

public class CustumButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private EGenericButtonEvent _buttonType = EGenericButtonEvent.Interact;

    private ButtonEvent _buttonEventDown;
    private ButtonEvent _buttonEventUp;


    private void Start()
    {
        _buttonEventDown = new ButtonEvent(this,true);
        _buttonEventUp = new ButtonEvent(this, false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonEvents.instance.Raise(_buttonType, _buttonEventDown);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ButtonEvents.instance.Raise(_buttonType, _buttonEventUp);
    }
}
