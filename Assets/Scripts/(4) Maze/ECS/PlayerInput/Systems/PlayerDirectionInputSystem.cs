using ECS.EntityTags;
using ECS.Movement;
using Game.GameData;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.PlayerInput
{
    sealed class PlayerDirectionInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _directionFilter;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _directionFilter = world.Filter<PlayerTagComponent>().Inc<DirectionComponent>().End();
        }
        
        public void Run(IEcsSystems systems)
        {
            if (GameManager.IsPause || GameManager.IsPauseInput)
            {
                return;
            }
            
            foreach (int entity in _directionFilter)
            {
                var world = systems.GetWorld();

                world.GetPool<DirectionComponent>().Get(entity).m_direction =
                    Input.Instance.m_controls.Player.Move.ReadValue<Vector2>();
            }
        }
    }
}
