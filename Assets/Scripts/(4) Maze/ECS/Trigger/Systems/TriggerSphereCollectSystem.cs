using ECS.Audio.SFX;
using ECS.Entity;
using ECS.EntityTags;
using Leopotam.EcsLite;

namespace ECS.Trigger
{
    public class TriggerSphereCollectSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _triggerFilter;

        public void Init(Leopotam.EcsLite.IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _triggerFilter = world.Filter<TriggerEnterEventSendComponent>()
                .Inc<TriggerSphereCollectComponent>()
                .End();
        }

        public void Run(Leopotam.EcsLite.IEcsSystems systems)
        {
            var world = systems.GetWorld();

            foreach (int entity in _triggerFilter)
            {
                var soundEventPool = world.GetPool<SoundEventComponent>();
                ref var trigger = ref world.GetPool<TriggerSphereCollectComponent>().Get(entity).trigger;
                ref var entityTrigger = ref world.GetPool<EntityComponent>().Get(entity);

                if (trigger.triggerObject.TryGetComponent(out EntityReference reference))
                {
                    if (world.GetPool<PlayerTagComponent>().Has(reference.m_entity))
                    {
                        if (!soundEventPool.Has(reference.m_entity))
                        {
                            soundEventPool.Add(reference.m_entity);
                        }

                        entityTrigger.DelEntity(world);
                    }
                }
            }
        }
    }
}
