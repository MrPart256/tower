using System;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Localization
{
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField] private TMP_Text _text;

        private ILocalizationSystem _localizationSystem;

        private IDisposable _languageStream;
        
        [Inject]
        private void Construct(ILocalizationSystem localizationSystem)
        {
            _localizationSystem = localizationSystem;
        }
        
        private void OnValidate()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void Init(string key)
        {
            _text.text = _localizationSystem.GetLocalization(key);

            _languageStream?.Dispose();
            
            _languageStream = _localizationSystem.OnLanguageChanged(key)
                .Subscribe(text => _text.text = text);
        }
        
        private void OnEnable()
        {
            if(_key == string.Empty)
                return;
            
            Init(_key);
        }

        private void OnDisable()
        {
            _languageStream?.Dispose();
        }
    }
}