using ECS.Movement;
using Game.GameData;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Player
{
    public sealed class PlayerJumpSendEventSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            if (GameManager.IsPauseInput)
            {
                return;
            }
            
            if (PlayerInput.Input.Instance.m_controls.Player.Jump.WasPressedThisFrame())
            {
                var world = systems.GetWorld();
                var controller = world.GetPool<CharacterControllerComponent>().Get(GameManager.PlayerID).m_characterController;
                var jumpEventPool = world.GetPool<JumpEventComponent>();

                if (jumpEventPool.Has(GameManager.PlayerID) || !controller.isGrounded)
                {
                    return;
                }

                jumpEventPool.Add(GameManager.PlayerID);
            }
        }
    }
}
