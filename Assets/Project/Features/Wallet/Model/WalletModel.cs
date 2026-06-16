using System;
using System.Numerics;
using UniRx;

namespace Project.Features.Wallet.Model
{
    public class WalletModel
    {
        private readonly ReactiveProperty<BigInteger> _gold = new ReactiveProperty<BigInteger>(0);

        public IReadOnlyReactiveProperty<BigInteger> Gold => _gold;

        // Готовность шахты.
        private readonly ReactiveProperty<bool> _isReady = new ReactiveProperty<bool>(true);

        public IReadOnlyReactiveProperty<bool> IsReady => _isReady;

        // Прогресс перезарядки шахты.
        private readonly ReactiveProperty<float> _cooldownProgress = new ReactiveProperty<float>(1f);

        public IReadOnlyReactiveProperty<float> CooldownProgress => _cooldownProgress;

        // Время перезарядки.
        private const float CooldownDuration = 5f;

        private IDisposable _cooldownDisposable;

        public void TryClickMine()
        {
            // Проверка перезарядки шахты.
            if (!_isReady.Value) return;

            // Начисляем золото за клик по шахте.
            AddGold(10);

            // Запускаем кулдаун шахты.
            StartCooldown();
         }

        private void StartCooldown()
        {
            _isReady.Value = false;

            _cooldownProgress.Value = 0f;

            // Сбросим старый таймер.
            _cooldownDisposable?.Dispose();

            float elapsed = 0f;

            // Обновляем таймер каждые 0.02 секунды.
            _cooldownDisposable = Observable.Interval(TimeSpan.FromSeconds(0.02f))
                .Subscribe(_ =>
                {
                    elapsed += 0.02f;
                    _cooldownProgress.Value = Math.Min(elapsed / CooldownDuration, 1f);

                    // Активируем шахту.
                    if (elapsed >= CooldownDuration)
                    {
                        _isReady.Value = true;
                        _cooldownProgress.Value = 1f;
                        _cooldownDisposable?.Dispose();
                    }
                });
        }

        public void AddGold(BigInteger amount)
        {
            if (amount <= 0) return;

            _gold.Value += amount;
        }

        public bool TrySpendGold (BigInteger amount)
        {
            if (amount <= 0) return false;

            if (_gold.Value >= amount)
            {
                _gold.Value -= amount;

                return true;
            }

            return false;
        }
    }
}