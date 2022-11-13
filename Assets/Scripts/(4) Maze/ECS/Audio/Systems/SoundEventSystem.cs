using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Audio.SFX
{
    public sealed class SoundEventSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _soundEventFilter;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _soundEventFilter = world.Filter<SoundEventComponent>()
                .Inc<AudioComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            foreach (int entity in _soundEventFilter)
            {
                ref var key = ref world.GetPool<SoundEventComponent>().Get(entity).m_key;
                ref var sound = ref world.GetPool<AudioComponent>().Get(entity).m_audioSource;
                ref var librarySounds = ref world.GetPool<AudioComponent>().Get(entity).m_sounds;

                if (librarySounds.TryGetValue(key, out AudioClip clip))
                {
                    sound.clip = clip;
                    sound.Play();
                }

                world.GetPool<SoundEventComponent>().Del(entity);
            }
        }
    }
}
