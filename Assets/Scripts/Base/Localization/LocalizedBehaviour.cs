using UnityEngine;

namespace Base.Loacalization
{
    public abstract class LocalizedBehaviour : MonoBehaviour
    {
        [LocalizationKeyAtribute]
        [SerializeField]
        private string _key;

        public string Key
        {
            get { return _key; }

            set
            {
                _key = value;

                UpdateLocalization();
            }
        }

        public abstract void UpdateLocalization();

        protected virtual void OnEnable()
        {
            Localization.Instance.OnLocalizationChanged += UpdateLocalization;

            UpdateLocalization();
        }

        protected void OnDisable()
        {
            // TODO isinstantiated?
            Localization.Instance.OnLocalizationChanged -= UpdateLocalization;
        }
    }
}