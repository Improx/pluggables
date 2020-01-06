using System;
using UnityEngine;

namespace Improx.Pluggables
{
    [CreateAssetMenu(fileName = "ControllerTypePluggable", menuName = "Localization/ControllerTypePluggable", order = 0)]
    public class ControllerTypePluggable : TextPluggableBase
    {
        [SerializeField]
        private TextPluggableBase _keyboardContent;
        [SerializeField]
        private TextPluggableBase _mouseContent;
        [SerializeField]
        private TextPluggableBase _joystickContent;
        [SerializeField]
        private TextPluggableBase _customControllerContent;

        private Action<Rewired.Controller> _activeControllerChangedReparse;

        public override void RegisterDependencies(Action Reparse)
        {
            _activeControllerChangedReparse = _ => Reparse();
            InputIconManager.ActiveControllerChanged += _activeControllerChangedReparse;

            _mouseContent.RegisterDependencies(Reparse);
            _keyboardContent.RegisterDependencies(Reparse);
            _joystickContent.RegisterDependencies(Reparse);
            _customControllerContent.RegisterDependencies(Reparse);
        }

        public override void UnregisterDependencies()
        {
            InputIconManager.ActiveControllerChanged -= _activeControllerChangedReparse;

            _mouseContent.UnregisterDependencies();
            _keyboardContent.UnregisterDependencies();
            _joystickContent.UnregisterDependencies();
            _customControllerContent.UnregisterDependencies();
        }

        public override string GetText()
        {
            if (InputManager.IsReady == false)
            {
                return "[ControllerTypePluggable]";
            }

            var controller = InputIconManager.ActiveController;
            switch (controller.type)
            {
                case Rewired.ControllerType.Mouse:
                    return _mouseContent.GetText();
                case Rewired.ControllerType.Keyboard:
                    return _keyboardContent.GetText();
                case Rewired.ControllerType.Joystick:
                    return _joystickContent.GetText();
                case Rewired.ControllerType.Custom:
                    return _customControllerContent.GetText();
                default:
                    throw new System.NotImplementedException();
            }
        }
    }
}