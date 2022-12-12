using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay.Astar
{
    public class GridTile
    {
        internal int xPosition { private set; get; } = 0;
        internal int yPosition { private set; get; } = 0;
        internal Vector2 worldPosition { private set; get; } = default;
        internal bool isOccupied = false;
        internal int fCost => (gCost + hCost);
        internal int gCost;
        internal int hCost;
        internal GridTile parent;

        public GridTile(int xPosition, int yPosition, Vector2 worldPosition, bool isOccupied = false)
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;
            this.worldPosition = worldPosition;
            this.isOccupied = isOccupied;
        }
    }
}