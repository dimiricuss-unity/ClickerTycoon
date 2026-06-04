using System.Threading.Tasks;
using Project.Core.Services.AssetManagement;
using UnityEngine;

namespace Project.Core.Services.WindowManagement
{
    public class WindowService : IWindowService
    {
        private readonly IAssetProvider _assetProvider;
        private Transform _uiRoot;

        public WindowService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void RegisterUIRoot(Transform uiRoot)
        {
            _uiRoot = uiRoot;
        }

        public async Task OpenWindow(string windowId)
        {
            if (_uiRoot == null)
            {
                return;
            }

            await _assetProvider.InstantiatePrefab(windowId, _uiRoot);
        }
    }
}