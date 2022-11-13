using NTC.Global.System;

namespace ECS.PlayerInput
{
    public sealed class Input : Singleton<Input>
    {
        public NewInputSystem m_controls;

        private void Awake()
        {
            m_controls = new NewInputSystem();
        }

        private void OnEnable()
        {
            m_controls.Enable();
        }

        private void OnDisable()
        {
            m_controls.Disable();
        }
    }
}
