using System;
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
        private Vector2 worldSize;
        private Rect bounds;
        internal GridTile[,] Tiles => tiles;

        internal Grid(Vector2 worldSize, float tileHalfSize)
        {
            this.tileHalfSize = tileHalfSize;
            this.worldSize = worldSize;
            bounds = new Rect(worldSize.x / -2, worldSize.y / -2, worldSize.x, worldSize.y);
            tileFullSize = tileHalfSize * 2;
            bottomLeft = Vector2.zero - Vector2.right * worldSize.x / 2 - Vector2.up * worldSize.y / 2;
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

        internal bool Contains(Vector2 relativePos)
        {
            return bounds.Contains(relativePos);
        }

        private Vector2 GetWorldPositionFromGridPosition(int x, int y)
        {
            return (bottomLeft + Vector2.right * (x * tileFullSize + tileHalfSize) + Vector2.up * (y * tileFullSize + tileHalfSize));
        }

        private (int x, int y) GetGridPositionFromWorldPosition(Vector2 worldPosition)
        {
            float percentX = (worldPosition.x + worldSize.x / 2) / worldSize.x;
            float percentY = (worldPosition.y + worldSize.y / 2) / worldSize.y;
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((width - 1) * percentX);
            int y = Mathf.RoundToInt((height - 1) * percentY);
            return (x, y);
        }

        internal GridTile GetTileFromWorldPosition(Vector2 worldPosition)
        {
            (int x, int y) = GetGridPositionFromWorldPosition(worldPosition);
            return tiles[x, y];
        }

        internal Vector2 GetRoundedPosition(Vector2 worldPosition)
        {
            (int x, int y) = GetGridPositionFromWorldPosition(worldPosition);
            return GetWorldPositionFromGridPosition(x, y);
        }

        internal GridTile FindClosestAvailableTile(Vector2 worldPosition)
        {
            GridTile tile = GetTileFromWorldPosition(worldPosition);
            return FindClosestAvailableTile(tile);
        }

        internal GridTile FindClosestAvailableTile(GridTile tile)
        {
            if (!tile.isOccupied) return tile;
            List<GridTile> neighbours = GetNeighbours(tile);
            foreach (var neighour in neighbours)
            {
                if (!neighour.isOccupied) return neighour;
            }
            foreach (var neighour in neighbours)
            {
                if (!neighour.isOccupied) return neighour;
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

        internal int GetDistance(GridTile nodeA, GridTile nodeB)
        {
            int dstX = Mathf.Abs(nodeA.xPosition - nodeB.xPosition);
            int dstY = Mathf.Abs(nodeA.yPosition - nodeB.yPosition);

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}