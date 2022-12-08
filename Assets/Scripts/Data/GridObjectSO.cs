using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Data
{
    public class GridObjectSO : ScriptableObject
    {
        [SerializeField] internal new string name = "Grid Object";
        [SerializeField] internal Sprite icon = default;
        [SerializeField] internal Vector2Int dimension = default;
    }
}