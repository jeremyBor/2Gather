using UnityEngine;

public static class UIGlobal
{
    public static Color basicCTAColor { get
        {
            return ColorUtility.TryParseHtmlString("#68C4FFFF", out Color basicCTA)? basicCTA : Color.blue;
        }
    }

    public static Color achatColor
    {
        get
        {
            return ColorUtility.TryParseHtmlString("#15DF00FF", out Color achat) ? achat : Color.green;
        }
    }

    public static Color mainCTAColor
    {
        get
        {
            return ColorUtility.TryParseHtmlString("#FFC600FF", out Color mainCTA) ? mainCTA : Color.yellow;
        }
    }

    public static Color missionDefault
    {
        get
        {
            return ColorUtility.TryParseHtmlString("#68C4FFFF", out Color missiondefault) ? missiondefault : Color.blue;
        }
    }

    public static Color missionMedium
    {
        get
        {
            return ColorUtility.TryParseHtmlString("#002E84FF", out Color missionMedium) ? missionMedium : Color.cyan;
        }
    }

    public static Color missionHard
    {
        get
        {
            return ColorUtility.TryParseHtmlString("#060A7BFF", out Color missionHard) ? missionHard : Color.black;
        }
    }

}
