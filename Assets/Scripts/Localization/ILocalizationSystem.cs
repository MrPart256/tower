using System;

namespace Localization
{
    public interface ILocalizationSystem
    {
        public IObservable<string> OnLanguageChanged(string localizationKey);
        public string GetLocalization(string localizationKey);
    }
}