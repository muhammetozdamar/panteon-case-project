using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class InformationPanelController : MonoBehaviour
    {
        public static InformationPanelController Instance;
        [SerializeField] private GameObject panel = default;
        [SerializeField] private TextMeshProUGUI unitNameText = default;
        [SerializeField] private Image unitIconImage = default;
        [SerializeField] private TextMeshProUGUI healthText = default;
        [SerializeField] private Image healthImage = default;
        [SerializeField] private GameObject productionsRoot = default;

        private void Awake()
        {
            Instance = this;
        }

        public void SetCurrentUnit(UnitBase unit)
        {
            panel.SetActive(true);

            unitNameText.text = unit.data.Name;
            unitIconImage.sprite = unit.data.Icon;

            healthText.text = $"{unit.Health}/{unit.data.MaxHealth}";
            healthImage.fillAmount = (unit.Health / unit.data.maxHealth);

            if (unit is BuildingBase)
            {
                productionsRoot.SetActive(true);
            }
        }

        internal void ResetPanel()
        {
            panel.SetActive(false);
        }
    }
}