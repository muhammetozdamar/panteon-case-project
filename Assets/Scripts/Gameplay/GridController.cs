using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay.Astar
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private Rect bounds = new Rect(Vector2.zero, Vector2.one * 100f);
        [SerializeField] private float tileHalfSize = 0.5f;
        [SerializeField] private SpriteRenderer ground = default;
        [SerializeField] private LayerMask groundLayerMask = default;



        private Grid grid;
        private void Awake()
        {
            grid = new Grid(bounds, tileHalfSize);
            ground.size = bounds.size;
            ground.transform.localPosition = bounds.center;
            ground.GetComponent<BoxCollider2D>().size = bounds.size;
        }
        RectInt rect4x4 = new RectInt(-2, -2, 4, 4);
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, groundLayerMask);
                if (hit.transform != null)
                {
                    if (CanPlaceObject(hit.point, rect4x4))
                    {
                        PlaceObject(hit.point, rect4x4);
                        print("Placed!");
                    }
                    else
                    {
                        print("Can't place!");
                    }
                }
            }

        }

        internal bool CanPlaceObject(Vector2 position, RectInt objBounds)
        {
            GridTile clickedTile = grid.GetTileFromWorldPosition(position);
            foreach (var pos in objBounds.allPositionsWithin)
            {
                if (grid.GetTileFromWorldPosition(position + pos).isOccupied) return false;
            }
            return true;
        }

        internal void PlaceObject(Vector2 position, RectInt objBounds)
        {
            foreach (var pos in objBounds.allPositionsWithin)
            {
                grid.GetTileFromWorldPosition(position + pos).isOccupied = true;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (grid != null)
            {
                grid.Tiles[5, 4].isOccupied = true;
                foreach (GridTile n in grid.Tiles)
                {
                    Gizmos.color = (!n.isOccupied) ? Color.white : Color.red;
                    Gizmos.DrawWireCube(n.worldPosition, Vector2.one * (tileHalfSize));
                }
            }
        }
    }
}