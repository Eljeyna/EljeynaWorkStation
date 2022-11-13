using System;
using UnityEngine;

namespace ECS.Audio
{
    [Serializable]
    public struct AudioComponent
    {
        public AudioSource m_audioSource;
        public IntAudioClipDictionary m_sounds;
    }
}
