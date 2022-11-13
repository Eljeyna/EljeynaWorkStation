using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.PlayerInput
{
    public class PlayerCursorLockSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
