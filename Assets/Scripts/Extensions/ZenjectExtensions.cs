using Saves;
using Zenject;

namespace Extensions
{
    public static class ZenjectExtensions
    {
        public static ScopeConcreteIdArgConditionCopyNonLazyBinder BindSavable(this ScopeConcreteIdArgConditionCopyNonLazyBinder binder)
        {
            binder.OnInstantiated((context, savable) =>
            {
                var saveSystemFacade = context.Container.Resolve<SaveSystemFacade>();
                saveSystemFacade.Add(savable as ISavable);
            });
            return binder;
        }
    }
}