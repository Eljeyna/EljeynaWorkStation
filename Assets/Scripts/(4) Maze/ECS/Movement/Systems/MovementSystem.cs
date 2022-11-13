using ECS.Entity;
using ECS.StateMachine;
using Game.GameData;
using Leopotam.EcsLite;
using PlayerInput;
using UnityEngine;

namespace ECS.Movement
{
    public sealed class MovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _moveFilter;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _moveFilter = world.Filter<MoveComponent>()
                .Inc<CharacterControllerComponent>()
                .Inc<DirectionComponent>()
                .Inc<StateMachineComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            if (GameManager.IsPause)
            {
                return;
            }
            
            foreach (int entity in _moveFilter)
            {
                var world = systems.GetWorld();

                ref var moveComponent = ref world.GetPool<MoveComponent>().Get(entity);
                ref var directionComponent = ref world.GetPool<DirectionComponent>().Get(entity);

                var stateMachineWalkComponent = world.GetPool<StateMachineWalkComponent>();
                bool hasStateMachineWalkComponent = stateMachineWalkComponent.Has(entity);

                ref var direction = ref directionComponent.m_direction;

                if (direction == Vector2.zero)
                {
                    if (hasStateMachineWalkComponent)
                    {
                        stateMachineWalkComponent.Del(entity);
                    }

                    continue;
                }

                ref var entityReference = ref world.GetPool<EntityComponent>().Get(entity).m_entityReference;
                ref var directionLook = ref world.GetPool<DirectionTransformComponent>().Get(entity).m_cameraTransform;
                ref var characterController = ref world.GetPool<CharacterControllerComponent>().Get(entity).m_characterController;
                ref var speed = ref moveComponent.m_moveSpeed;

                var rawDirection = (directionLook.right * direction.x) + (directionLook.forward * direction.y);
                rawDirection.y = 0f;
                characterController.Move(rawDirection * speed * Time.deltaTime);

                if (!hasStateMachineWalkComponent)
                {
                    stateMachineWalkComponent.Add(entity);
                }
            }
        }
    }
}
