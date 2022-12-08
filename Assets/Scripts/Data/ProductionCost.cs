using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Data
{
    [System.Serializable]
    public class ProductionCost
    {
        [SerializeField] internal BaseObjectSO product;
        [SerializeField] internal int amount;
    }
}