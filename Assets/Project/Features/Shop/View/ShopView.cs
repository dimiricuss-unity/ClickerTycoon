using UnityEngine;
using UnityEngine.UI;

namespace Project.Features.Shop.View
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        public Button CloseButton => _closeButton;

        public void DestroyWindow()
        {
            Destroy(gameObject);
        }
    }
}
