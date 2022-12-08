using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Data
{
    public class BaseObjectSO : ScriptableObject
    {
        [SerializeField] internal new string name = "Base Object";
        [SerializeField] internal Sprite icon = default;
        internal string Name => name;
        internal Sprite Icon => icon;
    }
}