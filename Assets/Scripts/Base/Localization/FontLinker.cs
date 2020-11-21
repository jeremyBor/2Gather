using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Base.Loacalization
{
    [CreateAssetMenu(fileName = "Font Linker", menuName = "Data/Localization/FontLinker", order = 1)]
    public class FontLinker : ScriptableObject
    {
        public AllFonts allFonts;
    }

    [System.Serializable]
    public class AllFonts
    {
        public TMPFontReference[] basesFontsRef;
        internal List<TMPro.TMP_FontAsset> basesFonts;
        public LanguagueFonts[] languagueFonts;
    }

    [System.Serializable]
    public class LanguagueFonts
    {
        public List<string> usedInLanguages;
        public TMPFontReference[] replaceFontRef;
        internal List<TMPro.TMP_FontAsset> replaceFont;
    }

    [Serializable]
    public class TMPFontReference : AssetReferenceT<TMP_FontAsset>
    {
        public TMPFontReference(string guid) : base(guid) { }
    }
}