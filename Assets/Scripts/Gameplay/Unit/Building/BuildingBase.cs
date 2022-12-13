using BaridaGames.PanteonCaseProject.Data;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public abstract class BuildingBase : UnitBase
    {
        internal RectInt Bounds => data.bounds;
        internal ProductionSO[] Productions => (data as BuildingSO).productions;
        internal Sprite Gfx => (data as BuildingSO).gfx;
        public abstract bool CanProduce(ProductionSO production);
        public abstract void AddProductionToQueue(ProductionSO production);
        protected abstract void Produce(ProductionSO production);
    }
}