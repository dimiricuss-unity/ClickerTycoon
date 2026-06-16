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

        public TextMeshProUGUI GoldText => _goldText;

        public Button ClickButton => _clickButton;
        
        public Image CooldownOverlay => _cooldownOverlay;
    }
}