using System.Threading.Tasks;
using UnityEngine;

namespace Project.Core.Services.AssetManagement
{
    public interface IAssetProvider
    {
        Task<T> LoadAsset<T>(string key) where T : Object;

        Task<GameObject> InstantiatePrefab(string key, Transform parent = null);

        void Release(string key);
    }
}
