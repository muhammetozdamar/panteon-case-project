using System.Collections.Generic;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay.Astar
{
    public class Grid
    {
        private GridTile[,] tiles;
        private float tileHalfSize = 0.5f;
        private float tileFullSize = 1f;
        private int width = 0;
        private int height = 0;
        private Vector2 bottomLeft;
        private Vector2 topRight;
        private Vector2 worldSize;
        internal GridTile[,] Tiles => tiles;

        internal Grid(Vector2 worldSize, float tileHalfSize)
        {
            this.tileHalfSize = tileHalfSize;
            this.worldSize = worldSize;
            tileFullSize = tileHalfSize * 2f;
            bottomLeft = Vector2.left * worldSize.x * 0.5f + Vector2.down * worldSize.y * 0.5f;
            topRight = bottomLeft * -1;
            width = Mathf.RoundToInt(worldSize.x / tileFullSize);
            height = Mathf.RoundToInt(worldSize.y / tileFullSize);
            tiles = new GridTile[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tiles[x, y] = new GridTile(x, y, GetWorldPositionFromGridPosition(x, y));
                }
            }
        }

        internal bool Contains(Vector2 position)
        {
            return (position.x >= bottomLeft.x && position.x <= topRight.x &&
                    position.y >= bottomLeft.y && position.y <= topRight.y);
        }

        private Vector2 GetWorldPositionFromGridPosition(int x, int y)
        {
            return (bottomLeft + Vector2.right * (x * tileFullSize + tileHalfSize) + Vector2.up * (y * tileFullSize + tileHalfSize));
        }

        private (int x, int y) GetGridPositionFromWorldPosition(Vector2 position)
        {
            float percentX = (position.x + worldSize.x * 0.5f) / worldSize.x;
            float percentY = (position.y + worldSize.y * 0.5f) / worldSize.y;
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((width - 1) * percentX);
            int y = Mathf.RoundToInt((height - 1) * percentY);

            return (x, y);
        }

        internal GridTile GetTileFromWorldPosition(Vector2 position)
        {
            (int x, int y) = GetGridPositionFromWorldPosition(position);
            return tiles[x, y];
        }

        internal Vector2 GetRoundedPositionFromWorldPosition(Vector2 position)
        {
            (int x, int y) = GetGridPositionFromWorldPosition(position);
            return GetWorldPositionFromGridPosition(x, y);
        }

        internal GridTile FindClosestAvailableTile(Vector2 position)
        {
            return FindClosestAvailableTile(GetTileFromWorldPosition(position));
        }

        internal GridTile FindClosestAvailableTile(GridTile tile)
        {
            Queue<GridTile> queue = new Queue<GridTile>();
            HashSet<GridTile> visited = new HashSet<GridTile>();
            queue.Enqueue(tile);
            visited.Add(tile);

            while (queue.Count > 0)
            {
                GridTile current = queue.Dequeue();
                if (!current.isOccupied)
                {
                    return current;
                }
                foreach (GridTile neighbour in GetNeighbours(current))
                {
                    if (!visited.Contains(neighbour))
                    {
                        queue.Enqueue(neighbour);
                        visited.Add(neighbour);
                    }
                }
            }
            return null;
        }

        internal List<GridTile> GetNeighbours(GridTile tile)
        {
            List<GridTile> neighbours = new List<GridTile>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = tile.xPosition + x;
                    int checkY = tile.yPosition + y;

                    if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
                    {
                        neighbours.Add(tiles[checkX, checkY]);
                    }
                }
            }

            return neighbours;
        }

        internal int GetDistance(GridTile tile0, GridTile tile1)
        {
            int dstX = Mathf.Abs(tile0.xPosition - tile1.xPosition);
            int dstY = Mathf.Abs(tile0.yPosition - tile1.yPosition);

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}