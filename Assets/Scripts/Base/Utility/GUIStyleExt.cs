using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public static class GUIStyleExt
{
    private static GUIStyle ButtonToOn(GUIStyle style)
    {
        style.normal = style.onNormal;
        style.active = style.onActive;
        return style;
    }

    public static GUIStyle GetButtonLarge(int margin = 0)
    {
        GUIStyle style = new GUIStyle("LargeButton");
        style.margin = new RectOffset(margin, margin, margin, margin);
        return style;
    }

    public static GUIStyle GetButtonLargeLeft(int margin = 0)
    {
        GUIStyle style = new GUIStyle("LargeButtonLeft");
        style.margin = new RectOffset(margin, 0, margin, margin);
        return style;
    }

    public static GUIStyle GetButtonLargeMid(int margin = 0)
    {
        GUIStyle style = new GUIStyle("LargeButtonMid");
        style.margin = new RectOffset(0, 0, margin, margin);
        return style;
    }

    public static GUIStyle GetButtonLargeRight(int margin = 0)
    {
        GUIStyle style = new GUIStyle("LargeButtonRight");
        style.margin = new RectOffset(0, margin, margin, margin);
        return style;
    }

    public static GUIStyle GetButtonLargeOn(int margin = 0)
    {
        GUIStyle style = GetButtonLarge(margin);
        style = ButtonToOn(style);
        return style;
    }

    public static GUIStyle GetButtonLargeLeftOn(int margin = 0)
    {
        GUIStyle style = GetButtonLargeLeft(margin);
        style = ButtonToOn(style);
        return style;
    }

    public static GUIStyle GetButtonLargeMidOn(int margin = 0)
    {
        GUIStyle style = GetButtonLargeMid(margin);
        style = ButtonToOn(style);
        return style;
    }

    public static GUIStyle GetButtonLargeRightOn(int margin = 0)
    {
        GUIStyle style = GetButtonLargeRight(margin);
        style = ButtonToOn(style);
        return style;
    }

    public static GUIStyle GetStyleButton()
    {
        GUIStyle style = new GUIStyle("Button");
        return style;
    }

    public static GUIStyle GetStyleButtonOn()
    {
        GUIStyle style = GetStyleButton();
        style = ButtonToOn(style);
        return style;
    }

    public static GUIStyle GetStyleButtonLeft()
    {
        GUIStyle style = new GUIStyle("ButtonLeft");
        return style;
    }

    public static GUIStyle GetStyleButtonMid()
    {
        GUIStyle style = new GUIStyle("ButtonMid");
        return style;
    }

    public static GUIStyle GetStyleButtonRight()
    {
        GUIStyle style = new GUIStyle("ButtonRight");
        return style;
    }

    public static GUIStyle GetStyleButtonLeftOn()
    {
        GUIStyle style = GetStyleButtonLeft();
        style = ButtonToOn(style);
        return style;
    }

    public static GUIStyle GetStyleButtonMidOn()
    {
        GUIStyle style = GetStyleButtonMid();
        style = ButtonToOn(style);
        return style;
    }

    public static GUIStyle GetStyleButtonRightOn()
    {
        GUIStyle style = GetStyleButtonRight();
        style = ButtonToOn(style);
        return style;
    }

    public static GUIStyle GetStyleBoxBackground(int margin = 0, int padding = 0)
    {
        GUIStyle style = new GUIStyle(EditorStyles.helpBox);
        style.margin = new RectOffset(margin, margin, margin, margin);
        style.padding = new RectOffset(padding, padding, padding, padding);
        return style;
    }
}
#endif