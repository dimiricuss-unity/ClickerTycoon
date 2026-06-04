using System;
using Project.Features.Shop.View;
using UniRx;
using Zenject;

namespace Project.Features.Shop.Presenter
{
    public class ShopPresenter : IInitializable, IDisposable
    {
        private readonly ShopView _view;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public ShopPresenter(ShopView view)
        {
            _view = view;
        }

        public void Initialize()
        {
            _view.CloseButton
                .OnClickAsObservable()
                .Subscribe(_ => 
                {
                    _view.DestroyWindow();
                })
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}