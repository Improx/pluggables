using UnityEngine;

namespace Improx.TLC.Localization
{
    public abstract class TextPluggableBase : ScriptableObject
    {
        public virtual bool IsConstant => false;
        public abstract string GetText();
        public abstract void RegisterDependencies(System.Action Reparse);
        public abstract void UnregisterDependencies();
    }
}