using BaridaGames.PanteonCaseProject.Gameplay.Astar;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField] private GridController gridController;
        [SerializeField] private SpriteRenderer preview;
        [SerializeField] private LayerMask groundLayerMask = default;
        private BuildingBase currentBuilding;
        private void Update()
        {
            if (currentBuilding == null) return;
            if (EventSystem.current.IsPointerOverGameObject()) return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, groundLayerMask);
            if (hit.transform != null)
            {
                Vector2 snapPosition = gridController.ConvertToGridPosition(hit.point);
                preview.transform.position = snapPosition + (Vector2)currentBuilding.Bounds.size * 0.5f + Vector2.left * 0.5f + Vector2.down * 0.5f;
                bool canPlace = gridController.CanPlaceObject(snapPosition, currentBuilding.Bounds);
                preview.color = canPlace ? Color.green : Color.red;
                if (Input.GetMouseButtonDown(0) && canPlace)
                {
                    gridController.PlaceObject(snapPosition, currentBuilding.Bounds);
                    Instantiate(currentBuilding, preview.transform.position, Quaternion.identity);
                    currentBuilding = null;
                    preview.gameObject.SetActive(false);
                }
            }
        }

        public void SelectBuilding(BuildingBase building)
        {
            currentBuilding = building;
            preview.sprite = building.Gfx;
            preview.gameObject.SetActive(true);
        }
    }
}