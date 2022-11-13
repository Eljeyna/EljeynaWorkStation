using System;
using System.Threading.Tasks;
using ECS.Audio.SFX;
using Game.GameData;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Audio
{
    public enum MusicList
    {
        None = -1,
        ActionMusic = 0,
    }
    
    [Serializable]
    public sealed class MusicManagerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private int _prevMusic = GameManager.NoMusic;
        private int _nextMusic = GameManager.NoMusic;
        private int _backupMusic = GameManager.NoMusic;

        private float _nextMusicTime;

        private EcsFilter _musicFilter;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _musicFilter = world.Filter<MusicTagComponent>()
                .Inc<AudioComponent>()
                .Inc<MusicEventComponent>()
                .End();
        }

        public async void Run(IEcsSystems systems)
        {
            foreach (int entity in _musicFilter)
            {
                var world = systems.GetWorld();

                if (world.GetPool<MusicChangeComponent>().Has(entity))
                {
                    await ChangeMusic(world, entity, world.GetPool<MusicChangeComponent>().Get(entity).m_music);
                    world.GetPool<MusicChangeComponent>().Del(entity);
                    return;
                }

                if (world.GetPool<MusicStopComponent>().Has(entity))
                {
                    StopMusic(ref world, entity);
                    world.GetPool<MusicStopComponent>().Del(entity);
                    return;
                }

                if (_nextMusicTime > Time.unscaledTime)
                {
                    if (_prevMusic >= 0)
                    {
                        world.GetPool<AudioComponent>().Get(entity).m_audioSource.volume = _nextMusicTime - Time.unscaledTime;
                    }

                    continue;
                }

                if (_prevMusic >= 0)
                {
                    StopMusicImmediately(ref world.GetPool<AudioComponent>().Get(entity).m_audioSource);
                }

                if (_nextMusic >= 0)
                {
                    PlayMusic(ref world, entity, _nextMusic);
                }

                _prevMusic = _nextMusic;

                world.GetPool<MusicEventComponent>().Del(entity);
            }
        }

        private void PlayMusic(ref EcsWorld world, int entity, int music)
        {
            if (_prevMusic != GameManager.NoMusic && _nextMusic != GameManager.NoMusic && _prevMusic == music)
            {
                return;
            }

            _nextMusic = music;
            var sound = world.GetPool<SoundEventComponent>().Add(entity);
            sound.m_key = music;
        }

        private async Task ChangeMusic(EcsWorld world, int entity, int music)
        {
            if (world.GetPool<MusicEventComponent>().Has(entity))
            {
                _backupMusic = music;
                
                await Task.Delay(GameManager.MusicTransitionTimeInMilliseconds);

                if (_backupMusic != music)
                {
                    music = _backupMusic;
                }
            }
            
            if (_prevMusic != GameManager.NoMusic && _nextMusic != GameManager.NoMusic && _prevMusic == music)
            {
                return;
            }

            _nextMusic = music;
            _nextMusicTime = Time.unscaledTime + GameManager.MusicTransitionTime;
            
            if (world.GetPool<MusicEventComponent>().Has(entity))
            {
                return;
            }

            world.GetPool<MusicEventComponent>().Add(entity);
        }

        private void StopMusic(ref EcsWorld world, int entity)
        {
            _nextMusic = GameManager.NoMusic;
            _nextMusicTime = Time.unscaledTime + GameManager.MusicTransitionTime;
            
            if (world.GetPool<MusicEventComponent>().Has(entity))
            {
                return;
            }

            world.GetPool<MusicEventComponent>().Add(entity);
        }

        private void StopMusicImmediately(ref AudioSource audioSource)
        {
            audioSource.Stop();
            audioSource.volume = 1f;
        }
    }
}
