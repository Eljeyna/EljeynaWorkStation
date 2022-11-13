using ECS.Audio;
using ECS.EntityTags;
using Game.GameData;
using Leopotam.EcsLite;

using IEcsRunSystem = Leopotam.EcsLite.IEcsRunSystem;

namespace ECS.Entity
{
    public sealed class EntityInitializeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _initFilter;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _initFilter = world.Filter<EntityComponent>()
                .Inc<EntityInitializeComponent>()
                .End();

            var _playerFilter = world.Filter<EntityComponent>().Inc<PlayerTagComponent>().End();

            foreach (int entity in _playerFilter)
            {
                GameManager.PlayerID = entity;
            }

            var _musicFilter = world.Filter<MusicTagComponent>()
                .End();

            foreach (int entity in _musicFilter)
            {
                var musicComponent = world.GetPool<MusicChangeComponent>().Add(entity);
                musicComponent.m_music = (int)MusicList.ActionMusic;
                world.GetPool<MusicEventComponent>().Add(entity);
            }
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entityPool = world.GetPool<EntityComponent>();
            var entityInitializePool = world.GetPool<EntityInitializeComponent>();

            foreach (int entity in _initFilter)
            {
                var request = entityPool.Get(entity);
                request.m_entityReference.m_entity = entity;
                entityInitializePool.Del(entity);
            }
        }
    }
}