using Game.GameData;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Movement
{
    public class CharacterAirSystem : IEcsInitSystem, IEcsRunSystem
    {
        EcsFilter _jumpFilter;

        public void Init(Leopotam.EcsLite.IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _jumpFilter = world.Filter<CharacterControllerComponent>()
                .End();
        }

        public void Run(Leopotam.EcsLite.IEcsSystems systems)
        {
            var world = systems.GetWorld();

            foreach (int entity in _jumpFilter)
            {
                ref var controller = ref world.GetPool<CharacterControllerComponent>().Get(entity).m_characterController;
                ref var velocity = ref world.GetPool<CharacterControllerComponent>().Get(entity).m_velocity;

                if (controller.isGrounded && velocity < 0f)
                {
                    velocity = -3f;

                    continue;
                }

                velocity += GameManager.GRAVITY_VALUE * Time.deltaTime;
                controller.Move(Vector3.up * velocity * Time.deltaTime);
            }
        }
    }
}
