using Cinemachine;
using Leopotam.EcsLite;
using Project;
using UnityEngine;

namespace Game.GameData
{
    public static class GameManager
    {
#region === VARIABLES ===
        //Game
        public static bool IsPause;
        public static bool IsPauseInput;
        
        //ID
        public static int PlayerID = 0;
        
        //Speed
        private const float DefaultGamePauseTime = 0f;
        private const float DefaultGameSpeedTime = 1f;
        public static float m_gameSpeedTime = 1f;

        //Gravity
        public const float GRAVITY_VALUE = -9.81f;
        
        //Layers
        public const int EntityLayer = 3;
        
        //Layers Mask
        public const int EntityMaskLayer = 1 << EntityLayer;

        //Music
        public const int NoMusic = -1;
        public const float MusicTransitionTime = 1f;
        public const int MusicTransitionTimeInMilliseconds = (int)(MusicTransitionTime * 1000f);
#endregion

#region === DO SOME CODE ===
        public static void PauseInput()
        {
            IsPauseInput = true;
        }

        public static void ResumeInput()
        {
            IsPauseInput = false;
        }
        
        public static void PauseGame()
        {
            IsPause = true;
            Time.timeScale = DefaultGamePauseTime;

            if (Camera.main.TryGetComponent(out CinemachineBrain brain))
            {
                brain.enabled = false;
            }

            Cursor.lockState = CursorLockMode.Confined;
        }

        public static void ResumeGame(EcsWorld world)
        {
            if (Camera.main.TryGetComponent(out CinemachineBrain brain))
            {
                brain.enabled = true;
            }

            UpdateMouseSensitivity();

            Time.timeScale = DefaultGameSpeedTime;
            GameEntity.ApplyPlayerInputZero(world);
            IsPause = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public static void UpdateMouseSensitivity()
        {
            if (Camera.main.TryGetComponent(out CinemachineVirtualCamera virtualCamera))
            {
                virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisValue =
                    PlayerPrefs.GetFloat(ProjectSettings.SettingsMouseSensitivityX, ProjectSettings.DefaultMouseSensitivity) / 100f;

                virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisValue =
                    PlayerPrefs.GetFloat(ProjectSettings.SettingsMouseSensitivityY, ProjectSettings.DefaultMouseSensitivity) / 100f;
            }
        }
#endregion
    }
}
