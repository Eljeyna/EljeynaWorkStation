using ECS.Entity;
using Leopotam.EcsLite;

namespace ECS.Trigger
{
    public class TriggersInitializeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter triggerInitializeFilter;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            triggerInitializeFilter = world.Filter<EntityComponent>().Inc<TriggerInitializeComponent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in triggerInitializeFilter)
            {
                var world = systems.GetWorld();

                if (world.GetPool<EntityComponent>().Get(entity).m_entityReference
                    .TryGetComponent(out TriggerInitialize triggersInitialize))
                {
                    triggersInitialize.Execute();
                    world.GetPool<TriggerInitializeComponent>().Del(entity);
                }
            }
        }
    }
}