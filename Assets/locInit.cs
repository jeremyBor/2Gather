using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Loacalization;
using System;

public class locInit : MonoBehaviour
{
    public FontLinker fontLinker;
    Action machin;
    void Awake()
    {
        Localization.Instance.Init(fontLinker, machin);
    }

}
