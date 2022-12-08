using BaridaGames.PanteonCaseProject.Data;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class BuildingBase : MonoBehaviour
    {
        [SerializeField] internal BuildingSO data;
        internal Vector2Int Dimension => data.dimension;
        internal ProductionSO[] Productions => data.productions;
    }
}