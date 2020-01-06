using System;
using UnityEngine;

namespace Improx.TLC.Localization
{
    [CreateAssetMenu(fileName = "ConstantTextPluggable", menuName = "Localization/ConstantTextPluggable", order = 0)]
    public class ConstantTextPluggable : TextPluggableBase
    {
        public override bool IsConstant => true;
        [TextArea]
        [SerializeField]
        private string _text;

        public override string GetText()
        {
            return _text;
        }

        public override void RegisterDependencies(Action Reparse) { }

        public override void UnregisterDependencies() { }
    }
}