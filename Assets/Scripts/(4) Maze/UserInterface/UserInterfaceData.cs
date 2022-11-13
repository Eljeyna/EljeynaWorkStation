using ECS.Audio;
using NTC.Global.System;
using TMPro;
using UnityEngine;

namespace Game.UserInterface
{
    public sealed class UserInterfaceData : Singleton<UserInterfaceData>
    {
        [field: SerializeField] public Canvas m_quickMenuCanvas { get; private set; }
        [field: SerializeField] public Canvas m_settingsCanvas { get; private set; }
        [field: SerializeField] public AudioManager m_userInterfaceAudioManager { get; private set; }
        [field: SerializeField] public TMP_Text m_questProgress { get; private set; }
    }
}
