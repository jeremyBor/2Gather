using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Base.Loacalization;

public class WinPanel : UIPanel
{
    public TextMeshProUGUI[] dialgues;

    public void SetDialoges (List<string> a_keys)
    {
        for(int i = 0; i< dialgues.Length; ++i )
        {
            dialgues[i].text = Localization.Instance.GetTranslationText(a_keys[i]);
        }
    }
}
