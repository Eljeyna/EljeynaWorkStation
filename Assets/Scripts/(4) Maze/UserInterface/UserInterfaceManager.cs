using Project;

namespace Game.UserInterface
{
    public static class UserInterfaceManager
    {
        public static void ShowQuickMenuInterface()
        {
            UserInterfaceData.Instance.m_quickMenuCanvas.enabled = true;
            UserInterfaceData.Instance.m_userInterfaceAudioManager.Play(ProjectSettings.ID_SOUND_MENU_OPEN);
        }

        public static void HideQuickMenuInterface()
        {
            UserInterfaceData.Instance.m_quickMenuCanvas.enabled = false;
            UserInterfaceData.Instance.m_userInterfaceAudioManager.Play(ProjectSettings.ID_SOUND_MENU_CLOSE);
        }

        public static void ShowSettingsMenu()
        {
            UserInterfaceData.Instance.m_settingsCanvas.enabled = true;
            UserInterfaceData.Instance.m_userInterfaceAudioManager.Play(ProjectSettings.ID_SOUND_MENU_OPEN);
        }

        public static void HideSettingsMenu()
        {
            UserInterfaceData.Instance.m_settingsCanvas.enabled = false;
        }
    }
}
