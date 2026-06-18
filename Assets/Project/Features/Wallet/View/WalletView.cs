using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Features.Wallet.View
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _goldText;
        [SerializeField] private Button _clickButton;
        [SerializeField] private Image _cooldownOverlay;
        [SerializeField] private GameObject _floatingCoin;
        [SerializeField] private Image _miningGlow;

        public TextMeshProUGUI GoldText => _goldText;

        public Button ClickButton => _clickButton;
        
        public Image CooldownOverlay => _cooldownOverlay;

        public GameObject FloatingCoin => _floatingCoin;

        public Image MiningGlow => _miningGlow;
    }
}