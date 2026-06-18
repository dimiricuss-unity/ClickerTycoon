using System;
using UnityEngine;
using Project.Features.Wallet.Model;
using Project.Features.Wallet.View;
using UniRx;
using Zenject;

namespace Project.Features.Wallet.Presenter
{
    public class WalletPresenter : IInitializable, IDisposable
    {
        private readonly WalletModel _model;
        private readonly WalletView _view;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public WalletPresenter(WalletModel model, WalletView view)
        {
            _model = model;
            _view = view;
        }

        public void Initialize()
        {
            _model.Gold
                .Subscribe(goldAmount =>
                {
                    _view.GoldText.text = $"Gold: {goldAmount}";
                })
                .AddTo(_disposables);

            _model.CooldownProgress
                .Subscribe(progress =>
                {
                    if (progress >= 1f)
                    {
                        _view.CooldownOverlay.fillAmount = 0f;
                    }
                    else
                    {
                        _view.CooldownOverlay.fillAmount = progress;

                        float alpha = 0.1f + Mathf.PingPong(progress * 5f, 0.25f);

                        SetGlowAlpha(alpha);
                    }
                    
                })
                .AddTo(_disposables);

            _model.IsReady
                .Subscribe(isReady =>
                {
                    _view.ClickButton.interactable = isReady;

                    _view.FloatingCoin.SetActive(isReady);

                    _view.CooldownOverlay.gameObject.SetActive(!isReady);

                    _view.MiningGlow.gameObject.SetActive(!isReady);

                    if (isReady)
                    {
                        SetGlowAlpha(0f);
                    }
                })
                .AddTo(_disposables);

            _view.ClickButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _model.TryClickMine();
                })
                .AddTo(_disposables);           
        }

        private void SetGlowAlpha(float alpha)
        {
            Color color = _view.MiningGlow.color;
            color.a = alpha;
            _view.MiningGlow.color = color;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}