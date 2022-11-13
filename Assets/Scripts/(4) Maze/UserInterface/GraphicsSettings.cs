using System.Collections.Generic;
using Project;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UserInterface
{
    public sealed class GraphicsSettings : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdownResolutions;
        [SerializeField] private TMP_Dropdown _dropdownScreenMode;
        [SerializeField] private Toggle _toggleVsyncMode;

        private void Awake()
        {
            _dropdownResolutions.ClearOptions();

            List<string> options = new List<string>();

            int currentResolutionIndex = 0;
            for (int i = 0; i < Screen.resolutions.Length; i++)
            {
                string option = $"{Screen.resolutions[i].width} x {Screen.resolutions[i].height}";
                options.Add(option);

                if (Screen.resolutions[i].width == Screen.currentResolution.width && Screen.resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            _dropdownResolutions.AddOptions(options);
            _dropdownResolutions.value = PlayerPrefs.GetInt(ProjectSettings.SettingsResolution, currentResolutionIndex);
            _dropdownResolutions.RefreshShownValue();

            _dropdownScreenMode.value = PlayerPrefs.GetInt(ProjectSettings.SettingsScreenMode, (int)FullScreenMode.FullScreenWindow);
            _dropdownResolutions.RefreshShownValue();

            if (PlayerPrefs.HasKey(ProjectSettings.SettingsVsync))
            {
                bool key = PlayerPrefs.GetInt(ProjectSettings.SettingsVsync) == 1;
                _toggleVsyncMode.isOn = key;

                return;
            }
        }
        
        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = Screen.resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
            PlayerPrefs.SetInt(ProjectSettings.SettingsResolution, resolutionIndex);
        }

        public void SetFullscreenMode(int fullscreenMode)
        {
            Screen.fullScreenMode = (FullScreenMode)fullscreenMode;
            PlayerPrefs.SetInt(ProjectSettings.SettingsScreenMode, fullscreenMode);
        }

        public void SetVsyncCount(bool vsyncCount)
        {
            QualitySettings.vSyncCount = GetVsyncInt(vsyncCount);
            PlayerPrefs.SetInt(ProjectSettings.SettingsVsync, GetVsyncInt(vsyncCount));
        }

        private int GetVsyncInt(bool vsyncCount)
        {
            return vsyncCount == false ? 0 : 1;
        }
    }
}
