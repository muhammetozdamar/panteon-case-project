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
        Rect rect4x4 = new Rect(0, 0, 4, 4);
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, groundLayerMask);
                if (hit.transform != null)
                {
                    print(hit.point);
                    PlaceObject(hit.point, rect4x4);
                }
            }

        }

        internal bool CanPlaceObject(Vector2 position, Rect objBounds)
        {
            Rect temp = objBounds;
            temp.center += position;

            if (!bounds.Overlaps(temp, false)) return false;

            for (int x = 0; x < objBounds.width; x++)
            {
                for (int y = 0; y < objBounds.height; y++)
                {
                    GridTile tile = grid.GetTileFromWorldPosition(position);
                    if (tile.isOccupied) return false;
                }
            }
            return true;
        }

        internal void PlaceObject(Vector2 position, Rect objBounds)
        {
            for (int x = 0; x < objBounds.width; x++)
            {
                for (int y = 0; y < objBounds.height; y++)
                {
                    Vector2 relativePosition =
                        position;
                    GridTile tile = grid.GetTileFromWorldPosition(relativePosition);
                    tile.isOccupied = true;
                    print($"{tile.xPosition}:{tile.yPosition} - {tile.worldPosition}");
                }
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