namespace BaridaGames.PanteonCaseProject.Gameplay.Astar
{
    public class GridTileInfo
    {
        internal int fCost => gCost + hCost;
        internal int gCost { get; set; }
        internal int hCost { get; set; }
        internal GridTile parent { get; set; }
    }
}