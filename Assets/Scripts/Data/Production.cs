using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Data
{
    [System.Serializable]
    public class Production
    {
        [SerializeField] internal ProductReferance product;
        [SerializeField] internal float productionTime;
        [SerializeField] internal ProductionCost[] cost;
    }

}