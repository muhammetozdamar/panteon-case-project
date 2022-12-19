using BaridaGames.PanteonCaseProject.Gameplay.Astar;
using BaridaGames.PanteonCaseProject.Utilities;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField] private GridController gridController;
        [SerializeField] private SpriteRenderer preview;
        [SerializeField] private LayerMask groundLayerMask = default;
        [SerializeField] private BuildingFactory buildingFactory = default;
        private BuildingBase currentBuilding;
        private void Update()
        {
            if (currentBuilding == null) return;
            if (MouseHelper.OnUI) return;

            Vector2 hitPoint = RayHelper.GetHitPointFromMouse(groundLayerMask);
            Vector2 snapPosition = gridController.ConvertToGridPosition(hitPoint);

            preview.transform.position = snapPosition + (Vector2)currentBuilding.Bounds.size * 0.5f + GridController.Instance.Offset;

            bool canPlace = gridController.CanPlaceObject(snapPosition, currentBuilding.Bounds);
            preview.color = canPlace ? Color.green : Color.red;

            if (MouseHelper.LeftClickDown && canPlace)
            {
                gridController.PlaceObject(snapPosition, currentBuilding.Bounds);
                BuildingBase building = buildingFactory.GetBuilding(currentBuilding, preview.transform.position);
                building.position = snapPosition;
                currentBuilding = null;
                preview.gameObject.SetActive(false);
            }
        }

        public void SelectBuilding(BuildingBase building)
        {
            currentBuilding = building;
            preview.sprite = building.data.Gfx;
            preview.gameObject.SetActive(true);
        }
    }
}