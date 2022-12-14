using System.Collections.Generic;
using BaridaGames.PanteonCaseProject.Utilities;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay.Astar
{
    public class GridController : MonoBehaviourSingleton<GridController>
    {
        public Vector2 Offset => Vector2.left * tileHalfSize + Vector2.down * tileHalfSize;
        [SerializeField] private Vector2 bounds;
        [SerializeField] private float tileHalfSize = 0.5f;
        [SerializeField] private SpriteRenderer ground = default;
        [SerializeField] private CameraController cameraController = default;
        private Grid grid;
        private Pathfinder pathfinder;
        private void Awake()
        {
            grid = new Grid(bounds, tileHalfSize);
            pathfinder = new Pathfinder(grid);

            ground.size = bounds;
            ground.transform.localPosition = Vector3.zero;
            ground.GetComponent<BoxCollider2D>().size = bounds;

            cameraController.SetBounds(-bounds * 0.5f, bounds);
        }
        internal Vector2 ConvertToGridPosition(Vector2 worldPosition)
        {
            return grid.GetRoundedPositionFromWorldPosition(worldPosition);
        }

        internal Vector2 GetClosestAvailablePoint(Vector2 worldPosition)
        {
            return grid.FindClosestAvailableTile(worldPosition).worldPosition;
        }

        internal GridTile GetTile(Vector2 worldPosition)
        {
            return grid.GetTileFromWorldPosition(worldPosition);
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
                tile.isOccupied = true;
            }
        }

        internal void RemoveObject(Vector2 position, RectInt objBounds)
        {
            foreach (var pos in objBounds.allPositionsWithin)
            {
                GridTile tile = grid.GetTileFromWorldPosition(position + pos);
                tile.isOccupied = false;
            }
        }

        internal List<GridTile> GetPath(Vector2 startPosition, Vector2 endPosition)
        {
            return pathfinder.GetPath(startPosition, endPosition);
        }

        private void OnDrawGizmosSelected()
        {
            if (grid != null)
            {
                Vector2 size = Vector2.one * (tileHalfSize);
                foreach (GridTile n in grid.Tiles)
                {
                    Gizmos.color = (!n.isOccupied) ? Color.white : Color.red;
                    Gizmos.DrawWireCube(n.worldPosition, size);
                }
            }
        }
    }
}