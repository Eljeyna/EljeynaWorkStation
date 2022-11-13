using UnityEngine;

namespace Game.UserInterface
{
    public class SettingsMenuShow : MonoBehaviour
    {
        public void Exec()
        {
            UserInterfaceManager.HideQuickMenuInterface();
            UserInterfaceManager.ShowSettingsMenu();
        }
    }
}
