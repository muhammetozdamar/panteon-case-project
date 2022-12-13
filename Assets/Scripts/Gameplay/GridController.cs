using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay.Astar
{
    public class GridController : MonoBehaviour
    {
        //[SerializeField] private Rect bounds = new Rect(Vector2.zero, Vector2.one * 100f);
        [SerializeField] private Vector2 bounds;
        [SerializeField] private float tileHalfSize = 0.5f;
        [SerializeField] private SpriteRenderer ground = default;
        private Grid grid;
        private void Awake()
        {
            grid = new Grid(bounds, tileHalfSize);
            ground.size = bounds;
            ground.transform.localPosition = Vector3.zero;
            ground.GetComponent<BoxCollider2D>().size = bounds;
        }
        internal Vector2 ConvertToGridPosition(Vector2 worldPosition)
        {
            return grid.ConvertToGridPosition(worldPosition);
        }

        internal bool CanPlaceObject(Vector2 position, RectInt objBounds)
        {
            foreach (var pos in objBounds.allPositionsWithin)
            {
                Vector2 relativePos = position + pos;
                if (!grid.Contains(relativePos)) return false;
                GridTile tile = grid.GetTileFromWorldPosition(relativePos);
                if (tile == null || tile.isOccupied) return false;
            }
            return true;
        }
        internal void PlaceObject(Vector2 position, RectInt objBounds)
        {
            foreach (var pos in objBounds.allPositionsWithin)
            {
                GridTile tile = grid.GetTileFromWorldPosition(position + pos);
                Vector2 rounded = ConvertToGridPosition(position + pos);
                tile.isOccupied = true;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (grid != null)
            {
                foreach (GridTile n in grid.Tiles)
                {
                    Gizmos.color = (!n.isOccupied) ? Color.white : Color.red;
                    Gizmos.DrawWireCube(n.worldPosition, Vector2.one * (tileHalfSize));
                }
            }
        }
    }
}