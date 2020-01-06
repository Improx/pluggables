using System;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using NaughtyAttributes;
using UnityEngine;

namespace Improx.Pluggables
{
    [CreateAssetMenu(fileName = "LocalizationObject", menuName = "Localization/LocalizationObject", order = 0)]
    public class LocalizationObject : ScriptableObject
    {
        [SerializeField]
        private string Key;
        [ReorderableList]
        [SerializeField]
        private List<TextPluggableBase> Pluggables;

        [ReadOnly]
        [TextArea]
        public string Text;

        public event Action<string> OnTextChanged;

        private bool _initialized;
        private string _languageOverride;

        private void OnValidate()
        {
            Text = ParseWithoutRegistering();
        }

        private void Reparse()
        {
            Text = Parse();
            OnTextChanged?.Invoke(Text);
        }

        private string ParseWithoutRegistering(string langCode = null)
        {
            string[] texts = new string[Pluggables.Count];
            for (int i = 0; i < Pluggables.Count; i++)
            {
                TextPluggableBase item = Pluggables[i];

                texts[i] = item?.GetText();
            }

            return FormatWithLang(Key, langCode, texts);
        }

        public string Parse(string langCode = null)
        {
            // Only init in play mode
            if (!_initialized && Application.isPlaying)
            {
                Init();
            }

            return ParseWithoutRegistering(langCode);
        }

        public void Init()
        {
            foreach (var item in Pluggables)
            {
                item.RegisterDependencies(Reparse);
            }

            LocalizationManager.LocalizationChanged += Reparse;
            _initialized = true;
        }

        public void UnInit()
        {
            if (_initialized == false) return;

            foreach (var item in Pluggables)
            {
                item.UnregisterDependencies();
            }
            LocalizationManager.LocalizationChanged -= Reparse;
            _initialized = false;
        }

        private static string FormatWithLang(string localizationKey, string langCode, params object[] inputs)
        {
            string tempText = langCode == null ?
                LocalizationManager.Localize(localizationKey) :
                LocalizationManager.Localize(localizationKey, langCode);

            return ReplacePlaceholders(tempText, inputs);
        }

        public static string Format(string localizationKey, params object[] inputs)
        {
            string tempText = LocalizationManager.Localize(localizationKey);

            return ReplacePlaceholders(tempText, inputs);
        }

        private static string ReplacePlaceholders(string placeholderText, object[] inputs)
        {
            foreach (var item in inputs)
            {
                placeholderText = placeholderText.ReplaceFirst("[]", item?.ToString());
            }
            return placeholderText;
        }
    }
}