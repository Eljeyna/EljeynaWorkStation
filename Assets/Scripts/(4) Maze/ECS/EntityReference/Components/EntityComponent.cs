using System;
using Leopotam.EcsLite;

namespace ECS.Entity
{
    [Serializable]
    public struct EntityComponent
    {
        public EntityReference m_entityReference;

        public void DelEntity(EcsWorld world)
        {
            m_entityReference.DelEntity();
            world.DelEntity(m_entityReference.m_entity);
        }
    }
}
