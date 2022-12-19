using System;
using BaridaGames.PanteonCaseProject.Data;
using BaridaGames.PanteonCaseProject.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaridaGames.PanteonCaseProject.Gameplay.UI
{
    public class InformationPanelController : MonoBehaviourSingleton<InformationPanelController>
    {
        [SerializeField] private GameObject panel = default;
        [SerializeField] private TextMeshProUGUI unitNameText = default;
        [SerializeField] private Image unitIconImage = default;
        [SerializeField] private TextMeshProUGUI healthText = default;
        [SerializeField] private Image healthImage = default;
        [SerializeField] private GameObject productionsRoot = default;
        [SerializeField] private RectTransform productionsContent = default;
        private IDamageable currentDamageable;
        public void SetCurrentUnit(UnitBase unit)
        {
            panel.SetActive(true);

            unitNameText.text = unit.data.Name;
            unitIconImage.sprite = unit.data.Icon;

            healthText.text = $"{unit.Health}/{unit.data.MaxHealth}";
            healthImage.fillAmount = (unit.Health / unit.data.maxHealth);

            if (unit is BuildingBase)
            {
                int childCount = productionsContent.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    DestroyImmediate(productionsContent.GetChild(0).gameObject);
                }

                productionsRoot.SetActive(true);
                BuildingBase building = unit as BuildingBase;
                for (int i = 0; i < building.Productions.Length; i++)
                {
                    ProductionSO production = building.Productions[i];
                    ProductUI productUI = ProductUIFactory.Instance.GetPrefab();
                    productUI.SetProduct(production.product.Name, production.product.Icon, () => building.AddProductionToQueue(production));
                }
            }
            else
            {
                productionsRoot.SetActive(false);
            }

            if (currentDamageable != null)
            {
                currentDamageable.OnDamaged -= UpdateInformation;
                currentDamageable.OnDied -= ResetPanel;

            }
            currentDamageable = (IDamageable)unit;
            currentDamageable.OnDamaged += UpdateInformation;
            currentDamageable.OnDied += ResetPanel;

        }

        private void UpdateInformation(object sender, EventArgs e)
        {
            var args = (DamageableEventArgs)e;
            healthText.text = $"{args.currentHealth}/{args.maxHealth}";
            healthImage.fillAmount = (args.currentHealth / args.maxHealth);
        }

        internal void ResetPanel(object sender, EventArgs e)
        {
            panel.SetActive(false);
            if (currentDamageable != null)
            {
                currentDamageable.OnDamaged -= UpdateInformation;
                currentDamageable.OnDied -= ResetPanel;
                currentDamageable = null;
            }
        }

        internal void ResetPanel()
        {
            panel.SetActive(false);
            if (currentDamageable != null)
            {
                currentDamageable.OnDamaged -= UpdateInformation;
                currentDamageable.OnDied -= ResetPanel;
                currentDamageable = null;
            }
        }
    }
}