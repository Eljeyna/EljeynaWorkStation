using System;
using UnityEngine;

namespace ECS.Movement
{
    [Serializable]
    public struct CharacterControllerComponent
    {
        public CharacterController m_characterController;
        [HideInInspector] public float m_velocity;
    }
}
