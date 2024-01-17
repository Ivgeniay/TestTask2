using BarrelHide.Services;
using MoreMountains.Tools;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace BarrelHide.Input
{
    internal class InputService : IService
    {
        public event Action<Vector2> OnJoystickInputEvent;
        public event Action OnActionBtnPressed;
        public event Action OnAttackBtnPressed;

        private MMTouchJoystick joystick;
        private Button actionBtn, attackBtn;
        public InputService(MMTouchJoystick joystick, Button actionBtn, Button attackBtn)
        { 
            this.joystick = joystick;
            this.actionBtn = actionBtn;
            this.attackBtn = attackBtn;
        }

        public void Initialize()
        {
            joystick.Initialize();

            joystick.JoystickValue.AddListener(JoystickInputCallback);
            actionBtn.onClick.AddListener(ActionBtnCallback);
            attackBtn.onClick.AddListener(AttackBtnCallback);
        }

        private void AttackBtnCallback() => OnAttackBtnPressed?.Invoke(); 
        private void ActionBtnCallback() => OnActionBtnPressed?.Invoke(); 
        private void JoystickInputCallback(Vector2 arg0) => OnJoystickInputEvent?.Invoke(arg0);

    }
}
