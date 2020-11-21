using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent
{
    private CustumButton _button;
    public CustumButton Button => _button;

    private bool _buttonDown;
    public bool ButtonDown => _buttonDown;

    public ButtonEvent(CustumButton a_button, bool a_buttonDown)
    {
        _button = a_button;
        _buttonDown = a_buttonDown;
    }
}

public enum EGenericButtonEvent
{
    Start,
    Pause,
    Quit,
    Resume,
    SelectPlayer,
    Interact,
    RightAction,
    LeftAction
}

public class ButtonEvents
{
    static ButtonEvents instanceInternal = null;
    public static ButtonEvents instance
    {
        get
        {
            if (instanceInternal == null)
            {
                instanceInternal = new ButtonEvents();
            }

            return instanceInternal;
        }
    }

    public delegate void EventDelegate<ButtonEvent>(ButtonEvent buttonEvent);
    private delegate void EventDelegate(ButtonEvent buttonEvent);

    private Dictionary<EGenericButtonEvent, EventDelegate> delegates = new Dictionary<EGenericButtonEvent, EventDelegate>();
    private Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();

    public void AddListener(EventDelegate<ButtonEvent> del, EGenericButtonEvent a_buttonType)
    {
        // Early-out if we've already registered this delegate
        if (delegateLookup.ContainsKey(del))
        {
            Debug.LogWarning("Warning: Button already registered on this event : " + a_buttonType);
            return;
        }
        
        EventDelegate internalDelegate = (e) => del(e);
        delegateLookup[del] = internalDelegate;

        EventDelegate tempDel;
        if (delegates.TryGetValue(a_buttonType, out tempDel))
        {
            delegates[a_buttonType] = tempDel += internalDelegate;
        }
        else
        {
            delegates[a_buttonType] = internalDelegate;
        }
    }

    public void RemoveListener(EventDelegate<ButtonEvent> del, EGenericButtonEvent a_buttonType)
    {
        EventDelegate internalDelegate;
        if (delegateLookup.TryGetValue(del, out internalDelegate))
        {
            EventDelegate tempDel;
            if (delegates.TryGetValue(a_buttonType, out tempDel))
            {
                tempDel -= internalDelegate;
                if (tempDel == null)
                {
                    delegates.Remove(a_buttonType);
                }
                else
                {
                    delegates[a_buttonType] = tempDel;
                }
            }

            delegateLookup.Remove(del);
        }
    }

    public void Raise(EGenericButtonEvent a_buttonType, ButtonEvent buttonEvent)
    {
        EventDelegate del;
        if (delegates.TryGetValue(a_buttonType, out del))
        {
            del.Invoke(buttonEvent);
        }
    }
}