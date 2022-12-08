using BaridaGames.PanteonCaseProject.Data;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class BuildingBase : UnitBase
    {
        internal Vector2Int Dimension => data.dimension;
        internal ProductionSO[] Productions => (data as BuildingSO).productions;
    }
}