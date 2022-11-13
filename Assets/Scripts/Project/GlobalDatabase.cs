using NTC.Global.System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Project
{
    public sealed class GlobalDatabase : Singleton<GlobalDatabase>
    {
        [field: SerializeField] public Transform m_sceneLoaderEnvironment { get; private set; }
        [field: SerializeField] public AssetReference[] m_scenes { get; private set; }

        private async void Awake()
        {
            await LoadSceneManager.Instance.LoadScene((int)Scenes.Calculator);
        }
    }
}
