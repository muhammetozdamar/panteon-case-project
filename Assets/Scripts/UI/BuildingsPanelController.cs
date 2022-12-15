using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay.UI
{
    public class BuildingsPanelController : MonoBehaviour
    {
        [SerializeField] private BuildingController buildingController;
        [SerializeField] private RectTransform contentHolder;

        private void Awake()
        {
            BuildingUI[] buildingUIs = contentHolder.GetComponentsInChildren<BuildingUI>(true);
            foreach (BuildingUI buildingUI in buildingUIs)
            {
                buildingUI.SetOnClick(() => OnBuildingUIClicked(buildingUI));
            }
        }

        public void OnBuildingUIClicked(BuildingUI buildingUI)
        {
            buildingController.SelectBuilding(buildingUI.Building);
        }
    }
}

