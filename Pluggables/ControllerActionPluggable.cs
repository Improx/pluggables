using System;
using Rewired;
using UnityEngine;

namespace Improx.Pluggables
{
    [CreateAssetMenu(fileName = "ControllerTypePluggable", menuName = "Localization/ControllerActionPluggable", order = 0)]
    public class ControllerActionPluggable : TextPluggableBase
    {
        [ActionIdProperty(typeof(RewiredConsts.Action))]
        public int ActionId;
        public Pole ActionPole;

        private Action<Controller> _activeControllerChangedReparse;

        public override void RegisterDependencies(Action Reparse)
        {
            _activeControllerChangedReparse = _ =>
            {
                Reparse();
            };
            InputIconManager.ActiveControllerChanged += _activeControllerChangedReparse;
        }

        public override void UnregisterDependencies()
        {
            InputIconManager.ActiveControllerChanged -= _activeControllerChangedReparse;
        }

        public override string GetText()
        {
            if (InputManager.IsReady == false || InputIconManager.IsReady == false)
            {
                return "[ControllerActionPluggable]";
            }

            var action = ReInput.mapping.GetAction(ActionId);
            var glyphData = InputIconManager.GetGlyphStringForAction(action, ActionPole);
            return InputIconManager.WrapWithIconTag(glyphData);
        }
    }
}