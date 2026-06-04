using System.Numerics;
using UniRx;

namespace Project.Features.Wallet.Model
{
    public class WalletModel
    {
        private readonly ReactiveProperty<BigInteger> _gold = new ReactiveProperty<BigInteger>(0);

        public IReadOnlyReactiveProperty<BigInteger> Gold => _gold;

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