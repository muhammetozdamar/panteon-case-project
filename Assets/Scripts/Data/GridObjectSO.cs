using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Data
{
    public class GridObjectSO : BaseObjectSO
    {
        [SerializeField] internal RectInt bounds = default;
        [SerializeField] internal Sprite gfx;
        internal Sprite Gfx => gfx;
    }
}