using System;
using Project.Core.Services.WindowManagement;
using Project.Features.HUD.View;
using UniRx;
using Zenject;

namespace Project.Features.HUD.Presenter
{
    public class HUDPresenter : IInitializable, IDisposable
    {
        private readonly HUDView _view;
        private readonly WindowService _windowService;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public HUDPresenter(HUDView view, IWindowService winodowService)
        {
            _view = view;

            _windowService = winodowService as WindowService; 
        }

        public void Initialize()
        {
            _windowService.RegisterUIRoot(_view.UIRoot);

            _view.OpenShopButton
                .OnClickAsObservable()
                .Subscribe(async _ => 
                {
                    await _windowService.OpenWindow("ShopWindow");
                })
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}