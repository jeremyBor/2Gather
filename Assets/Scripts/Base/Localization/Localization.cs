using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace Base.Loacalization
{
    public sealed class Localization
    {
        #region Singleton

        private static Localization _instance = null;

        public static Localization Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Localization();
                return _instance;
            }
        }

        private Localization()
        {
        }

        ~Localization()
        {
            if (_fontLinker != null)
            {
                for (int i = 0; i < _fontLinker.allFonts.basesFonts.Count; ++i)
                {
                    _fontLinker.allFonts.basesFonts[i].fallbackFontAssetTable.Clear();
                }
            }
        }

        #endregion

        public Action OnLocalizationChanged;

        #region Private Proptertys
        private Dictionary<string, Dictionary<string, string>> localizeDic = 
            new Dictionary<string, Dictionary<string, string>>(); 

        private List<string> _languages;

        private string _defaultLanguage = "French";

        private string _currentLanguage;
        private FontLinker _fontLinker = null;
        private LanguagueFonts _languagueFonts = null;
        #endregion

        #region Public Functions
        public string CurrentLanguage
        {
            set
            {
                if (CurrentLanguage != value)
                {
                    if (localizeDic.ContainsKey(value))
                    {
                        _currentLanguage = value;
                        UpdateLocalization();

                        PlayerPrefs.SetString("CurrentLanguage", value);
                    }
                    else
                    {
                        Debug.LogError("Asked language : " + value +
                                       " isn't in the localization file. Go back to previous language");
                    }
                }
            }

            get { return _currentLanguage; }
        }

        public void Init(FontLinker a_fontLinker, Action action, Action<float> onProgress = null)
        {
            // In case init was already done
            if (localizeDic.Count > 0)
            {
                action?.Invoke();
                return;
            }

            // Setup
            if (PlayerPrefs.HasKey("CurrentLanguage"))
            {
                _currentLanguage = PlayerPrefs.GetString("CurrentLanguage");
            }
            else
            {
                _currentLanguage = GetSystemLanguage();
            }

            _fontLinker = a_fontLinker;

            // Load
            GameStateManager.Instance.StartCoroutine(LoadCsvFile(() => LoadMainFont(action),
                Application.streamingAssetsPath + "/Localization.csv", onProgress));

            // Wait for changes
            OnLocalizationChanged?.Invoke();
        }
        
        public void SetNextLanguage()
        {
            for (int i = 0; i < _languages.Count; ++i)
            {
                if (CurrentLanguage == _languages[i])
                {
                    if (i + 1 < _languages.Count)
                    {
                        CurrentLanguage = _languages[i + 1];
                    }
                    else
                    {
                        CurrentLanguage = _languages[0];
                    }

                    break;
                }
            }
        }

        public string GetTranslationText(string key)
        {
            string text = string.Empty;

            localizeDic[_currentLanguage].TryGetValue(key, out text);

            if (text == string.Empty)
            {
                text = key;
                Debug.LogError("key " + key + " not found");
            }

            return text;
        }

#if UNITY_EDITOR
        public List<string> GetKeys()
        {
            List<string> keys = new List<string>();
            LoadCsvFile(Application.streamingAssetsPath + "/Localization.csv");
            foreach (string key in localizeDic[_defaultLanguage].Keys)
            {
                keys.Add(key);
            }

            return keys;
        }
#endif
        #endregion

        #region Private Functions
        private void UpdateLocalization()
        {
            for (int i = 0; i < _fontLinker.allFonts.basesFonts.Count; ++i)
            {
                TMPro.TMP_FontAsset baseFont = _fontLinker.allFonts.basesFonts[i];
                baseFont.fallbackFontAssetTable.Clear();
            }

            LoadAditiveFont(OnLocalizationChanged);
        }

        private string GetSystemLanguage()
        {
            if (localizeDic.ContainsKey(Application.systemLanguage.ToString()))
            {
                return Application.systemLanguage.ToString();
            }

            return _defaultLanguage;
        }
        #endregion

        #region Lading Fonts & CSV File
        private async void LoadMainFont(Action action)
        {
            _fontLinker.allFonts.basesFonts = new List<TMPro.TMP_FontAsset>();
            for (int i = 0; i < _fontLinker.allFonts.basesFontsRef.Length; ++i)
            {
                TMPro.TMP_FontAsset baseFont = await _fontLinker.allFonts.basesFontsRef[i].LoadAssetAsync<TMPro.TMP_FontAsset>().Task;
                _fontLinker.allFonts.basesFonts.Add(baseFont);
            }
            LoadAditiveFont(action);
        }

        private async void LoadAditiveFont(Action action)
        {
            if (_languagueFonts != null)
            {
                _languagueFonts.replaceFont.Clear();
                for (int i = 0; i < _languagueFonts.replaceFontRef.Length; ++i)
                {
                    _languagueFonts.replaceFontRef[i].ReleaseAsset();
                }
            }

            for (int i = 0; i < _fontLinker.allFonts.languagueFonts.Length; ++i)
            {
                if (_fontLinker.allFonts.languagueFonts[i].usedInLanguages.Contains(CurrentLanguage))
                {
                    _languagueFonts = _fontLinker.allFonts.languagueFonts[i];
                    break;
                }
            }

            if (_languagueFonts == null)
            {
                Debug.LogError("The language " + CurrentLanguage + " as no font assossied!");
                action?.Invoke();
            }
            else
            {
                _languagueFonts.replaceFont = new List<TMPro.TMP_FontAsset>();
                for (int i = 0; i < _languagueFonts.replaceFontRef.Length; ++i)
                {
                    TMPro.TMP_FontAsset replaceFont = await _languagueFonts.replaceFontRef[i].LoadAssetAsync<TMPro.TMP_FontAsset>().Task;
                    _languagueFonts.replaceFont.Add(replaceFont);
                }

                if (_fontLinker.allFonts.basesFonts.Count <= _languagueFonts.replaceFont.Count)
                {
                    for (int i = 0; i < _fontLinker.allFonts.basesFonts.Count; ++i)
                    {
                        TMPro.TMP_FontAsset baseFont = _fontLinker.allFonts.basesFonts[i];
                        TMPro.TMP_FontAsset aditiveFont = _languagueFonts.replaceFont[i];
                        baseFont.fallbackFontAssetTable.Add(aditiveFont);
                    }
                }
                else
                {
                    Debug.LogError("The language " + CurrentLanguage + " must have the same number of font has base fonts");
                }

                action?.Invoke();
            }
        }

        private void LoadCsvFile(string path)
        {
            string text;
            localizeDic = new Dictionary<string, Dictionary<string, string>>();
            _languages = new List<string>();
            text = File.ReadAllText(path);
            FillLocalisationDic(text);

        }

        private IEnumerator LoadCsvFile(Action nextAction, string path, Action<float> onProgress = null)
        {
            string text;
            localizeDic = new Dictionary<string, Dictionary<string, string>>();
            _languages = new List<string>();
            if (path.Contains("://") || path.Contains(":///"))
            {
                UnityEngine.Networking.UnityWebRequest data = UnityEngine.Networking.UnityWebRequest.Get(path);

                yield return data.SendWebRequest();


                text = data.downloadHandler.text;
            }
            else
            {
                text = File.ReadAllText(path);
            }

            onProgress?.Invoke(1f);

            FillLocalisationDic(text);

            nextAction?.Invoke();
        }

        private void FillLocalisationDic(string fileText)
        {
            fileText = fileText.Replace("\r", "").Replace("[", "").Replace("]", "");

            string[] locFile = fileText.Split('\n');

            string firstLine = locFile[0];
            string[] languagesValues = firstLine.Split(',');

            for (int i = 1; i < languagesValues.Length; ++i) // ignor the first element
            {
                localizeDic.Add(languagesValues[i], new Dictionary<string, string>());
                _languages.Add(languagesValues[i]);
            }

            for (int i = 1; i < locFile.Length; ++i)
            {
                string line = locFile[i];
                string[] values = line.Split(',');

                int j = 1;
                foreach (Dictionary<string, string> dic in localizeDic.Values)
                {
                    dic.Add(values[0], values[j].Replace("\\n", "\n").Replace("{DOT}", ",").Replace("{QUOTE}", Char.ToString('"')));
                    ++j;
                }
            }
        }
        #endregion
    }
}