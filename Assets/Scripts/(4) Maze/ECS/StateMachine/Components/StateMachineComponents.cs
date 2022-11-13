using System;

namespace ECS.StateMachine
{
    [Serializable]
    public enum StateMachine
    {
        Idle,
        Walk,
    }
    
    public struct StateMachineComponent
    {
        public StateMachine stateMachine;
    }

    public struct StateMachineWalkComponent {}

}
