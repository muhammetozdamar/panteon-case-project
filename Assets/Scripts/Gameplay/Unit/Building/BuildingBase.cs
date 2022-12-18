using BaridaGames.PanteonCaseProject.Data;
using BaridaGames.PanteonCaseProject.Gameplay.Astar;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public abstract class BuildingBase : UnitBase
    {
        internal ProductionSO[] Productions => (data as BuildingSO).productions;
        internal Vector2 position;
        public abstract bool CanProduce(ProductionSO production);
        public abstract void AddProductionToQueue(ProductionSO production);
        protected abstract void Produce(ProductionSO production);

        public override void OnDeath()
        {
            GridController.Instance.RemoveObject(position, Bounds);
            base.OnDeath();
        }
    }
}