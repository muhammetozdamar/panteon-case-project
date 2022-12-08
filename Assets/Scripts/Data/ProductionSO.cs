using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Data
{
    [CreateAssetMenu(fileName = "Production", menuName = "Panteon Games Case/Production", order = 0)]
    public class ProductionSO : ScriptableObject
    {
        [SerializeField] internal BaseObjectSO product;
        [SerializeField] internal float productionTime;
        [SerializeField] internal ProductionCost[] cost;
    }
}