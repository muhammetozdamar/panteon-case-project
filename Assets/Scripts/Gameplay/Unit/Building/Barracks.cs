using System.Collections.Generic;
using BaridaGames.PanteonCaseProject.Data;
using BaridaGames.PanteonCaseProject.Gameplay.Astar;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class Barracks : BuildingBase
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private List<SoldierBase> soldiers;
        private Queue<ProductionSO> productionQueue;
        private ProductionSO currentProduction;
        private float currentProductionTime;
        private float currentProductionTimeLeft;
        private Dictionary<ProductionSO, SoldierBase> soldierDb;
        private void Awake()
        {
            soldierDb = new Dictionary<ProductionSO, SoldierBase>();
            foreach (ProductionSO production in Productions)
            {
                soldierDb.Add(production, soldiers.Find((soldier) => soldier.data == (production.product as SoldierSO)));
            }
        }

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
                    Debug.Log($"Producing {currentProduction.product.name}, progress {currentProductionTimeLeft / currentProductionTime}!");
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
            SoldierBase soldier = Instantiate(soldierDb[production], availablePos, Quaternion.identity);
        }
        public override void AddProductionToQueue(ProductionSO production)
        {
            if (productionQueue == null) productionQueue = new Queue<ProductionSO>();
            productionQueue.Enqueue(production);
            Debug.Log($"{production.product.name} added to the queue!");
        }
    }
}