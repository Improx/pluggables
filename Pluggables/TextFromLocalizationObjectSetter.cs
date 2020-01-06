using TMPro;
using UnityEngine;

namespace Improx.TLC.Localization
{
    public class TextFromLocalizationObjectSetter : MonoBehaviour
    {
        [SerializeField]
        private LocalizationObject TextSource;
        private TMP_Text _text;

        private void Start()
        {
            TextSource.Init();

            _text = GetComponent<TMP_Text>();

            SetText(TextSource.Parse());

            TextSource.OnTextChanged += SetText;
        }

        private void OnValidate()
        {
            _text = GetComponent<TMP_Text>();

            if (TextSource)
            {
                SetText(TextSource.Parse());
            }
        }

        private void OnDestroy()
        {
            TextSource.OnTextChanged -= SetText;
            TextSource.UnInit();
        }

        private void SetText(string text)
        {
            _text.text = text;
        }
    }
}