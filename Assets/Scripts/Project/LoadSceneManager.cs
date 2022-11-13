using System.Threading.Tasks;
using ECS.Entity;
using Game.GameData;
using NTC.Global.System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Voody.UniLeo.Lite;

namespace Project
{
    public enum Scenes
    {
        Calculator  = 0,
        ImageGray   = 1,
        GUI         = 2,
        Maze        = 3
    }

    public sealed class LoadSceneManager : Singleton<LoadSceneManager>
    {
#region === VARIABLES ===
        public int m_currentScene { get; private set; } = -1;
        private AsyncOperationHandle<GameObject> _loadSceneAsync;
#endregion

#region === DO SOME CODE ===
        public async Task LoadScene(int scene)
        {
            if (m_currentScene == (int)Scenes.Maze)
            {
                KillAllEntities();
            }

            UnloadCurrentScene();

            _loadSceneAsync = Addressables.InstantiateAsync(GlobalDatabase.Instance.m_scenes[scene], GlobalDatabase.Instance.m_sceneLoaderEnvironment);
            await _loadSceneAsync.Task;

            m_currentScene = scene;

            if (m_currentScene == (int)Scenes.Maze)
            {
                GameManager.ResumeInput();
                GameManager.ResumeGame(WorldHandler.GetMainWorld());
            }
        }

        private void UnloadCurrentScene()
        {
            if (!_loadSceneAsync.IsValid())
            {
                return;
            }
            
            Addressables.ReleaseInstance(_loadSceneAsync);
        }

        private void KillAllEntities()
        {
            var world = WorldHandler.GetMainWorld();
            var allEntities = world.Filter<EntityComponent>().End();

            foreach (int entity in allEntities)
            {
                world.GetPool<EntityComponent>().Get(entity).DelEntity(world);
            }
        }

        private void OnDestroy() => UnloadCurrentScene();
#endregion
    }
}
