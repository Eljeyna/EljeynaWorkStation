using ECS.Audio;
using ECS.Audio.SFX;
using ECS.Entity;
using ECS.Movement;
using ECS.Player;
using ECS.PlayerInput;
using ECS.StateMachine;
using ECS.Trigger;
using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace ECS
{
    public sealed class ECSManager : MonoBehaviour
    {
        public EcsWorld m_world;
        public IEcsSystems m_systems;
#if UNITY_EDITOR
        private IEcsSystems _editorSystems;
#endif

#region === DO SOME CODE ===
        private void Awake()
        {
            m_world = new EcsWorld();
            m_systems = new EcsSystems(m_world);

            m_systems.ConvertScene();

            AddSystems();

            m_systems.Init();

#if UNITY_EDITOR
            // Создаем отдельную группу для отладочных систем.
            _editorSystems = new EcsSystems(m_world);
            _editorSystems
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Init();
#endif
        }

        private void AddSystems()
        {
            m_systems
                    
                .Add(new EntityInitializeSystem())
                .Add(new TriggersInitializeSystem())
                .Add(new MusicManagerSystem())

                .Add(new PlayerCursorLockSystem())
                .Add(new PlayerDirectionInputSystem())
                .Add(new PlayerCancelSendEventSystem())
                .Add(new PlayerJumpSendEventSystem())

                .Add(new MovementSystem())
                .Add(new CharacterAirSystem())
                .Add(new JumpSystem())

                .Add(new StateMachineSystem())

                .Add(new SoundEventSystem())

                .Add(new TriggerSphereCollectSystem())

            ;
        }

        private void Update()
        {
            m_systems?.Run();
            
#if UNITY_EDITOR
            // Выполняем обновление состояния отладочных систем. 
            _editorSystems?.Run();
#endif
        }

        private void OnDestroy()
        {
            m_systems?.Destroy();
            m_systems = null;

            m_world?.Destroy();
            m_world = null;
            
#if UNITY_EDITOR
            // Выполняем очистку отладочных систем.
            _editorSystems?.Destroy();
            _editorSystems = null;
#endif
        }
#endregion
    }
}
