using UnityEngine;

namespace ECS.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource m_source;

        [SerializeField] private AudioClip[] m_sounds;

        private void PreloadAudio(int index)
        {
            m_source.clip = m_sounds[index];
        }

        public void Play(int index)
        {
            PreloadAudio(index);
            m_source.Play();
        }

        public void Stop()
        {
            m_source.Stop();
        }
    }
}
