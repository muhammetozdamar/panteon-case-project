using BaridaGames.PanteonCaseProject.Data;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public abstract class BuildingBase : UnitBase
    {
        internal ProductionSO[] Productions => (data as BuildingSO).productions;
        public abstract bool CanProduce(ProductionSO production);
        public abstract void AddProductionToQueue(ProductionSO production);
        protected abstract void Produce(ProductionSO production);
    }
}