using UnityEngine;
using UnityEngine.UI;

namespace Project.Features.HUD.View
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private Button _openShopButton;
        [SerializeField] private Transform _uiRoot;

        public Button OpenShopButton => _openShopButton;

        public Transform UIRoot => _uiRoot;
    }
}