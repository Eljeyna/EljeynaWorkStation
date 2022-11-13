using Game.GameData;
using Leopotam.EcsLite;

namespace ECS.StateMachine
{
    public sealed class StateMachineSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _stateMachineFilter;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _stateMachineFilter = world.Filter<StateMachineComponent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            if (GameManager.IsPause)
            {
                return;
            }

            var world = systems.GetWorld();
            
            foreach (int entity in _stateMachineFilter)
            {
                ref var stateMachineComponent = ref world.GetPool<StateMachineComponent>().Get(entity);

                if (world.GetPool<StateMachineWalkComponent>().Has(entity))
                {
                    stateMachineComponent.stateMachine = StateMachine.Walk;
                    continue;
                }
                
                stateMachineComponent.stateMachine = StateMachine.Idle;
            }
        }
    }
}
