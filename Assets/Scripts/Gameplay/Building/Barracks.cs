using System.Collections.Generic;
using BaridaGames.PanteonCaseProject.Data;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class Barracks : BuildingBase
    {
        [SerializeField] private Transform spawnPoint;
        private Queue<ProductionSO> productions;
        private ProductionSO currentProduction;
        private float currentProductionTime;
        private float currentProductionTimeLeft;

        private void Update()
        {
            if (currentProduction == null && productions != null && productions.Count > 0)
            {
                currentProduction = productions.Dequeue();
                currentProductionTime = currentProduction.productionTime;
            }

            if (currentProduction != null)
            {
                currentProductionTimeLeft = Mathf.Clamp(currentProductionTimeLeft + Time.deltaTime, 0f, currentProductionTime);
                if (currentProductionTimeLeft >= currentProductionTime)
                {
                    Debug.Log($"Producing {currentProduction.product.name}, progress {currentProductionTimeLeft / currentProductionTime}!");
                    Produce(currentProduction);
                    currentProductionTimeLeft = 0f;
                    currentProduction = null;
                }
            }
        }
        public bool CanProduce(ProductionSO production)
        {
            if (production.cost.Length == 0) return true;
            // Can add check for costs later.
            return false;
        }
        private void Produce(ProductionSO production)
        {
            Debug.Log($"{production.product.name} is produced!");
        }
        public void AddSoldierProduction(ProductionSO production)
        {
            if (productions == null) productions = new Queue<ProductionSO>();
            productions.Enqueue(production);
            Debug.Log($"{production.product.name} added to the queue!");
        }
    }
}