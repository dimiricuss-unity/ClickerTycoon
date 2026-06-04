using Zenject;
using UnityEngine;
using Project.Features.Shop.View;
using Project.Features.Shop.Presenter;

public class ShopInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var shopView = GetComponent<ShopView>();

        Container.BindInstance(shopView).AsSingle();

        Container
            .BindInterfacesAndSelfTo<ShopPresenter>()
            .AsSingle()
            .NonLazy();
    }
}