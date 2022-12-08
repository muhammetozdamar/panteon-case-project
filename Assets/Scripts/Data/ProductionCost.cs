using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Data
{
    [System.Serializable]
    public class ProductionCost
    {
        [SerializeField] internal ProductReferance product;
        [SerializeField] internal int amount;
    }
}