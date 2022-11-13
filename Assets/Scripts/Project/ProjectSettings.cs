using Game.GameData;
using Game.UserInterface;
using UnityEngine;
using UnityEngine.Audio;

namespace Project
{
    public sealed class ProjectSettings : MonoBehaviour
    {
#region === DEFAULT PROJECT SETTINGS ===
        public const float DefaultMasterVolume = -40f;
        public const float DefaultMinVolume = -80f;
        public const float DefaultVolume = 0f;
        public const float DefaultMouseSensitivity = 100f;
        public const int DefaultFramerate = 60;
        public const int DefaultInactiveFramerate = 10;
        public const int DefaultVsync = 0;
#endregion

#region === SOUNDS ID ===
        public const int ID_SOUND_MENU_OPEN = 1;
        public const int ID_SOUND_MENU_CLOSE = 2;
#endregion

#region === PROJECT SETTINGS ===
        public const string SettingsResolution = "[Project] Resolution";
        public const string SettingsScreenMode = "[Project] Screen Mode";
        public const string SettingsTargetFramerate = "[Project] Target Framerate";
        public const string SettingsMouseSensitivityX = "[Project] Mouse Sensitivity X";
        public const string SettingsMouseSensitivityY = "[Project] Mouse Sensitivity Y";
        public const string SettingsVsync = "[Project] Vsync";

        public const string SettingsMasterVolume = "[Project] Master Volume";
        public const string SettingsSFXVolume = "[Project] SFX Volume";
        public const string SettingsMusicVolume = "[Project] Music Volume";
        public const string SettingsGUIVolume = "[Project] GUI Volume";

        public const string SettingsMixerMasterVolume = "masterVolume";
        public const string SettingsMixerSFXVolume = "sfxVolume";
        public const string SettingsMixerMusicVolume = "musicVolume";
        public const string SettingsMixerGUIVolume = "guiVolume";
#endregion

#region === VARIABLES ===
        public AudioMixer m_audioMixer;
        public static ProjectSettings Instance;
#endregion

#region === DO SOME CODE ===
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            Application.targetFrameRate = PlayerPrefs.GetInt(SettingsTargetFramerate, DefaultFramerate);
            QualitySettings.vSyncCount = PlayerPrefs.GetInt(SettingsVsync, DefaultVsync);
        }

        private void Start()
        {
            m_audioMixer.SetFloat(SettingsMixerMasterVolume, PlayerPrefs.GetFloat(SettingsMasterVolume, DefaultMasterVolume));
            m_audioMixer.SetFloat(SettingsMixerSFXVolume, PlayerPrefs.GetFloat(SettingsSFXVolume, DefaultVolume));
            m_audioMixer.SetFloat(SettingsMixerMusicVolume, PlayerPrefs.GetFloat(SettingsMusicVolume, DefaultVolume));
            m_audioMixer.SetFloat(SettingsMixerGUIVolume, PlayerPrefs.GetFloat(SettingsGUIVolume, DefaultVolume));
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                Application.targetFrameRate = PlayerPrefs.GetInt(SettingsTargetFramerate, DefaultFramerate);
                return;
            }

            Application.targetFrameRate = DefaultInactiveFramerate;

            if (LoadSceneManager.Instance.m_currentScene != (int)Scenes.Maze)
            {
                return;
            }

            if (GameManager.IsPause)
            {
                return;
            }

            GameManager.PauseGame();
            UserInterfaceManager.ShowQuickMenuInterface();
        }
#endregion
    }
}
