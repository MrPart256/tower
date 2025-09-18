using System;
using UniRx;

namespace Localization
{
    public class LocalizationSystemStub : ILocalizationSystem
    {
        public IObservable<string> OnLanguageChanged(string localizationKey)
        {
            return Observable.Return(localizationKey);
        }

        public string GetLocalization(string localizationKey)
        {
            return localizationKey;
        }
    }
}