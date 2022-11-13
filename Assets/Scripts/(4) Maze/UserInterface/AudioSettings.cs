using Project;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UserInterface
{
    public sealed class AudioSettings : MonoBehaviour
    {
        [SerializeField] private Slider sliderMasterVolume;
        [SerializeField] private Slider sliderSFXVolume;
        [SerializeField] private Slider sliderMusicVolume;
        [SerializeField] private Slider sliderGUIVolume;

        private void Awake()
        {
            sliderMasterVolume.value = TranslateToAudio(PlayerPrefs.GetFloat(ProjectSettings.SettingsMasterVolume, ProjectSettings.DefaultMasterVolume));
            ProjectSettings.Instance.m_audioMixer.SetFloat(ProjectSettings.SettingsMixerMasterVolume, sliderMasterVolume.value);

            sliderSFXVolume.value = TranslateToAudio(PlayerPrefs.GetFloat(ProjectSettings.SettingsSFXVolume, ProjectSettings.DefaultVolume));
            ProjectSettings.Instance.m_audioMixer.SetFloat(ProjectSettings.SettingsMixerSFXVolume, sliderSFXVolume.value);

            sliderMusicVolume.value = TranslateToAudio(PlayerPrefs.GetFloat(ProjectSettings.SettingsMusicVolume, ProjectSettings.DefaultVolume));
            ProjectSettings.Instance.m_audioMixer.SetFloat(ProjectSettings.SettingsMixerMusicVolume, sliderMusicVolume.value);

            sliderGUIVolume.value = TranslateToAudio(PlayerPrefs.GetFloat(ProjectSettings.SettingsGUIVolume, ProjectSettings.DefaultVolume));
            ProjectSettings.Instance.m_audioMixer.SetFloat(ProjectSettings.SettingsMixerGUIVolume, sliderGUIVolume.value);
        }

        public void SetMasterVolume(float volume)
        {
            ProjectSettings.Instance.m_audioMixer.SetFloat(ProjectSettings.SettingsMixerMasterVolume, TranslateToAudio(volume));
            PlayerPrefs.SetFloat(ProjectSettings.SettingsMasterVolume, volume);
        }

        public void SetSFXVolume(float volume)
        {
            ProjectSettings.Instance.m_audioMixer.SetFloat(ProjectSettings.SettingsMixerSFXVolume, TranslateToAudio(volume));
            PlayerPrefs.SetFloat(ProjectSettings.SettingsSFXVolume, volume);
        }

        public void SetMusicVolume(float volume)
        {
            ProjectSettings.Instance.m_audioMixer.SetFloat(ProjectSettings.SettingsMixerMusicVolume, TranslateToAudio(volume));
            PlayerPrefs.SetFloat(ProjectSettings.SettingsMusicVolume, volume);
        }

        public void SetGUIVolume(float volume)
        {
            ProjectSettings.Instance.m_audioMixer.SetFloat(ProjectSettings.SettingsMixerGUIVolume, TranslateToAudio(volume));
            PlayerPrefs.SetFloat(ProjectSettings.SettingsGUIVolume, volume);
        }

        private float TranslateToAudio(float volume)
        {
            if (Mathf.Approximately(volume, ProjectSettings.DefaultMinVolume))
            {
                return ProjectSettings.DefaultMinVolume;
            }

            return volume * 0.5f;
        }
    }
}
