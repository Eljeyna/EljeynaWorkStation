using Game.GameData;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Movement
{
    public class JumpSystem : IEcsInitSystem, IEcsRunSystem
    {
        EcsFilter _jumpFilter;

        public void Init(Leopotam.EcsLite.IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _jumpFilter = world.Filter<CharacterControllerComponent>()
                .Inc<JumpEventComponent>()
                .End();
        }

        public void Run(Leopotam.EcsLite.IEcsSystems systems)
        {
            var world = systems.GetWorld();

            foreach (int entity in _jumpFilter)
            {
                world.GetPool<JumpEventComponent>().Del(entity);

                ref var controller = ref world.GetPool<CharacterControllerComponent>().Get(entity).m_characterController;

                if (controller.isGrounded)
                {
                    ref var jumpHeight = ref world.GetPool<JumpComponent>().Get(entity).m_jumpHeight;
                    ref var velocity = ref world.GetPool<CharacterControllerComponent>().Get(entity).m_velocity;

                    velocity = Mathf.Sqrt(jumpHeight * -3f * GameManager.GRAVITY_VALUE);
                }
            }
        }
    }
}
