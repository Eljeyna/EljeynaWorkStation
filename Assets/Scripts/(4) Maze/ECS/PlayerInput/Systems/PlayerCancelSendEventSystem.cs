using Game.GameData;
using Game.UserInterface;
using Leopotam.EcsLite;
using Voody.UniLeo.Lite;

namespace ECS.Player
{
    public sealed class PlayerCancelSendEventSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            if (GameManager.IsPauseInput)
            {
                return;
            }
            
            if (!PlayerInput.Input.Instance.m_controls.Player.Cancel.WasPressedThisFrame())
            {
                return;
            }

            if (UserInterfaceData.Instance.m_quickMenuCanvas.enabled)
            {
                UserInterfaceManager.HideQuickMenuInterface();
                GameManager.ResumeGame(WorldHandler.GetMainWorld());

                return;
            }

            if (UserInterfaceData.Instance.m_settingsCanvas.enabled)
            {
                UserInterfaceManager.HideSettingsMenu();
            }

            GameManager.PauseGame();
            UserInterfaceManager.ShowQuickMenuInterface();
        }
    }
}
