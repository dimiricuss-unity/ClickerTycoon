using UnityEngine;
using Zenject;
using Project.Features.Wallet.Model;
using Project.Features.Wallet.View;
using Project.Features.Wallet.Presenter;
using Project.Core.Services.AssetManagement;
using Project.Core.Services.WindowManagement;
using Project.Features.HUD.View;
using Project.Features.HUD.Presenter;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private WalletView _walletView;
    [SerializeField] private HUDView _hudView;

    public override void InstallBindings()
    {
        Debug.Log("--- Zenject подключен! ---");

        Container
            .Bind<IAssetProvider>()
            .To<AddressablesAssetProvider>()
            .AsSingle();

        Container
            .Bind<IWindowService>()
            .To<WindowService>()
            .AsSingle();

        Container
            .Bind<WalletModel>()
            .AsSingle()
            .NonLazy();

        Container
            .BindInstance(_walletView)
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<WalletPresenter>()
            .AsSingle()
            .NonLazy();

        Container
            .BindInstance(_hudView)
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<HUDPresenter>()
            .AsSingle()
            .NonLazy();
    }
}
