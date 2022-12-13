using System;
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

        internal Vector2 ConvertToGridPosition(Vector2 worldPosition)
        {
            //return new Vector2(RoundToNearestGrid(worldPosition.x), RoundToNearestGrid(worldPosition.y));
            return GetTileFromWorldPosition(worldPosition).worldPosition;
        }

        internal GridTile GetTileFromWorldPosition(Vector2 worldPosition)
        {
            float percentX = (worldPosition.x + worldSize.x / 2) / worldSize.x;
            float percentY = (worldPosition.y + worldSize.y / 2) / worldSize.y;
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((width - 1) * percentX);
            int y = Mathf.RoundToInt((height - 1) * percentY);
            return tiles[x, y];
        }

        private float RoundToNearestGrid(float pos)
        {
            float diff = pos % tileFullSize;
            pos -= diff;
            if (diff > (tileHalfSize))
            {
                pos += tileFullSize;
            }
            return pos;
        }
    }
}