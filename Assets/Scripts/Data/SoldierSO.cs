using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Data
{
    [CreateAssetMenu(fileName = "Soldier", menuName = "Panteon Games Case/Soldier", order = 0)]
    public class SoldierSO : UnitSO
    {
        [SerializeField] internal float damage = 10;
    }
}