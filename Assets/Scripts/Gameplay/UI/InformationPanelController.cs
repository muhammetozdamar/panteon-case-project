using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class InformationPanelController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI unitNameText = default;
        [SerializeField] private Image unitIconImage = default;
        [SerializeField] private TextMeshProUGUI healthText = default;
        [SerializeField] private Image healthImage = default;
        [SerializeField] private GameObject productionsRoot = default;

        public void SetCurrentUnit(UnitBase unit)
        {
            unitNameText.text = unit.data.name;
            unitIconImage.sprite = unit.data.icon;

            healthText.text = $"{unit.Health}/{unit.data.maxHealth}";
            healthImage.fillAmount = (unit.Health / unit.data.maxHealth);

            if (unit is BuildingBase)
            {
                productionsRoot.SetActive(true);
                
            }
        }
    }
}