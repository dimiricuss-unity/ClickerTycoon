using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Project.Core.Services.AssetManagement
{
    public class AddressablesAssetProvider : IAssetProvider
    {
        private readonly DiContainer _diContainer;

        private readonly Dictionary<string, AsyncOperationHandle> _completedHandles = new Dictionary<string, AsyncOperationHandle>();

        public AddressablesAssetProvider (DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public async Task<T> LoadAsset<T>(string key) where T : Object
        {
            // Если ассет загружен - отдаём его из кэша.
            if (_completedHandles.TryGetValue(key, out var completedHandle))
            {
                return completedHandle.Result as T;
            }

            // Иначе загружаем через Addressables API.
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
            _completedHandles[key] = handle;

            // Ждём окончания загрузки.
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result;
            }

            Debug.Log($"Не удалось загрузить ассет по ключу: {key}");

            return null;
        }

        // Объединение Addressables и Zenject.
        public async Task<GameObject> InstantiatePrefab(string key, Transform parent = null)
        {
            GameObject prefab = await LoadAsset<GameObject>(key);

            if (prefab == null) return null;

            return _diContainer.InstantiatePrefab(prefab, parent);
        }

        public void Release(string key)
        {
            if (_completedHandles.TryGetValue(key, out var handle))
            {
                Addressables.Release(handle);
                _completedHandles.Remove(key);
                Debug.Log($"Ассет {key} успешно выгружен из памяти.");
            }
        }
    }
}