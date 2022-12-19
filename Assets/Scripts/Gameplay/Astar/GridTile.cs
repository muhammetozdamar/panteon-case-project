using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay.Astar
{
    public class GridTile
    {
        internal int xPosition { get; private set; } = 0;
        internal int yPosition { get; private set; } = 0;
        internal Vector2 worldPosition { get; private set; } = default;
        internal bool isOccupied = false;
        internal GridTileInfo Info { get; private set; } = new GridTileInfo();

        public GridTile(int xPosition, int yPosition, Vector2 worldPosition, bool isOccupied = false)
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;
            this.worldPosition = worldPosition;
            this.isOccupied = isOccupied;
        }

        public override string ToString()
        {
            return $"[{xPosition},{yPosition} - {worldPosition} - {isOccupied}]";
        }
    }
}