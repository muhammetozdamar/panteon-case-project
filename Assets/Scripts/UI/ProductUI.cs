using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaridaGames.PanteonCaseProject.Gameplay.UI
{
    public class ProductUI : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI productNameText;
        [SerializeField] private Image productIconImage;

        public void SetProduct(string productName, Sprite productIcon, Action onClick)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClick.Invoke());
            productNameText.text = productName;
            productIconImage.sprite = productIcon;
        }
    }
}