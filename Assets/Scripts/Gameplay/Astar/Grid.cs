using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay.Astar
{
    public class Grid
    {
        private GridTile[,] tiles;
        private float tileHalfSize = 0.5f;
        private float tileFullSize = 0.5f;
        private int width = 0;
        private int height = 0;
        private Rect bounds;
        private Vector2 bottomLeft;

        internal GridTile[,] Tiles => tiles;

        internal Grid(Rect bounds, float tileHalfSize)
        {
            this.bounds = bounds;
            this.tileHalfSize = tileHalfSize;
            tileFullSize = tileHalfSize * 2;
            bottomLeft = new Vector2(bounds.xMin, bounds.yMin);
            width = Mathf.RoundToInt(bounds.width / tileFullSize);
            height = Mathf.RoundToInt(bounds.height / tileFullSize);
            tiles = new GridTile[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tiles[x, y] = new GridTile(x, y, GetTileWorldPosition(x, y));
                }
            }
        }

        internal Vector2 GetTileWorldPosition(int x, int y)
        {
            return (bottomLeft + Vector2.right * (x * tileFullSize + tileHalfSize) + Vector2.up * (y * tileFullSize + tileHalfSize));
        }

        internal GridTile GetNodeWorldPosition(Vector2 worldPosition)
        {
            float percentX = (worldPosition.x + bounds.center.x) / bounds.xMax;
            float percentY = (worldPosition.y + bounds.center.y) / bounds.yMax;
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);
            int x = Mathf.RoundToInt((bounds.xMax - 1) * percentX);
            int y = Mathf.RoundToInt((bounds.yMax - 1) * percentY);
            return tiles[x, y];
        }
    }
}