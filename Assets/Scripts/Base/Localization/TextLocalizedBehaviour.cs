using UnityEngine;
using TMPro;

namespace Base.Loacalization
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextLocalizedBehaviour : LocalizedBehaviour
    {
        public TextMeshProUGUI text = null;

        protected override void OnEnable()
        {
            if (text == null)
            {
                text = GetComponent<TextMeshProUGUI>();
            }

            base.OnEnable();
        }

        public override void UpdateLocalization()
        {
            text.text = Localization.Instance.GetTranslationText(Key);
        }
    }
}