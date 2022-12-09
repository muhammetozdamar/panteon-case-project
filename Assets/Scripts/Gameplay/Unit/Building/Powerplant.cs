using BaridaGames.PanteonCaseProject.Data;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class Powerplant : BuildingBase
    {
        private ProductionSO currentProduction = null;
        private float currentProductionTime = 0;
        private float currentProductionTimeLeft = 0;
        internal override void Start()
        {
            base.Start();
            if (Productions != null && Productions.Length > 0)
            {
                currentProduction = Productions[0];
                currentProductionTime = currentProduction.productionTime;
                currentProductionTimeLeft = 0f;
            }
        }

        private void Update()
        {
            if (currentProduction != null)
            {
                currentProductionTimeLeft = Mathf.Clamp(currentProductionTimeLeft + Time.deltaTime, 0f, currentProductionTime);
                if (currentProductionTimeLeft >= currentProductionTime)
                {
                    Debug.Log($"Producing {currentProduction.product.Name}, progress {currentProductionTimeLeft / currentProductionTime}!");
                    Produce(currentProduction);
                    currentProductionTimeLeft = 0f;
                }
            }
        }
        public override bool CanProduce(ProductionSO production)
        {
            throw new System.NotImplementedException();
        }

        public override void AddProductionToQueue(ProductionSO production)
        {
            throw new System.NotImplementedException();
        }

        protected override void Produce(ProductionSO production)
        {
            Debug.Log($"{production.product.Name} is produced!");
        }
    }
}