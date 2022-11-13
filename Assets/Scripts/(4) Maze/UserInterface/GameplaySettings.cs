using Game.GameData;
using Project;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UserInterface
{
    public sealed class GameplaySettings : MonoBehaviour
    {
        [SerializeField] private Slider _sliderTargetFramerate;
        [SerializeField] private TMP_Text _sliderTargetFramerateValue;
        [SerializeField] private Slider _sliderMouseSensitivityX;
        [SerializeField] private TMP_Text _sliderMouseSensitivityValueX;
        [SerializeField] private Slider _sliderMouseSensitivityY;
        [SerializeField] private TMP_Text _sliderMouseSensitivityValueY;

        private void Awake()
        {
            _sliderTargetFramerate.value = PlayerPrefs.GetInt(ProjectSettings.SettingsTargetFramerate, ProjectSettings.DefaultFramerate);
            _sliderTargetFramerateValue.text = _sliderTargetFramerate.value.ToString();

            _sliderMouseSensitivityX.value = PlayerPrefs.GetFloat(ProjectSettings.SettingsMouseSensitivityX, ProjectSettings.DefaultMouseSensitivity);
            _sliderMouseSensitivityValueX.text = _sliderMouseSensitivityX.value.ToString();

            _sliderMouseSensitivityY.value = PlayerPrefs.GetFloat(ProjectSettings.SettingsMouseSensitivityY, ProjectSettings.DefaultMouseSensitivity);
            _sliderMouseSensitivityValueY.text = _sliderMouseSensitivityY.value.ToString();

            GameManager.UpdateMouseSensitivity();
        }

        public void SetTargetFramerate(float targetFramerate)
        {
            Application.targetFrameRate = (int)targetFramerate;
            PlayerPrefs.SetInt(ProjectSettings.SettingsTargetFramerate, (int)targetFramerate);
            _sliderTargetFramerateValue.text = targetFramerate.ToString();
        }

        public void SetMouseSensitivityX(float sensitivity)
        {
            PlayerPrefs.SetFloat(ProjectSettings.SettingsMouseSensitivityX, sensitivity);
            _sliderMouseSensitivityValueX.text = sensitivity.ToString();
        }

        public void SetMouseSensitivityY(float sensitivity)
        {
            PlayerPrefs.SetFloat(ProjectSettings.SettingsMouseSensitivityY, sensitivity);
            _sliderMouseSensitivityValueY.text = sensitivity.ToString();
        }
    }
}
