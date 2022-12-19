using System.Collections.Generic;
using BaridaGames.PanteonCaseProject.Data;
using BaridaGames.PanteonCaseProject.Gameplay.Astar;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class Barracks : BuildingBase
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private SoldierFactory soldierFactory;
        private Queue<ProductionSO> productionQueue;
        private ProductionSO currentProduction;
        private float currentProductionTime;
        private float currentProductionTimeLeft;

        private void Update()
        {
            if (currentProduction == null && productionQueue != null && productionQueue.Count > 0)
            {
                currentProduction = productionQueue.Dequeue();
                currentProductionTime = currentProduction.productionTime;
            }

            if (currentProduction != null)
            {
                currentProductionTimeLeft = Mathf.Clamp(currentProductionTimeLeft + Time.deltaTime, 0f, currentProductionTime);
                if (currentProductionTimeLeft >= currentProductionTime)
                {
                    Produce(currentProduction);
                    currentProductionTimeLeft = 0f;
                    currentProduction = null;
                }
            }
        }
        public override bool CanProduce(ProductionSO production)
        {
            if (production.cost.Length == 0) return true;
            // Can add check for costs later.
            return false;
        }
        protected override void Produce(ProductionSO production)
        {
            Vector2 availablePos = GridController.Instance.GetClosestAvailablePoint(spawnPoint.position);
            soldierFactory.GetSoldier(production, availablePos);
        }
        public override void AddProductionToQueue(ProductionSO production)
        {
            if (productionQueue == null) productionQueue = new Queue<ProductionSO>();
            productionQueue.Enqueue(production);
        }
    }
}