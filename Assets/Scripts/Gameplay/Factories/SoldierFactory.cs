using System.Collections.Generic;
using BaridaGames.PanteonCaseProject.Data;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class SoldierFactory : MonoBehaviour
    {
        [SerializeField] private List<ProductionSO> productions;
        [SerializeField] private List<SoldierBase> soldiers;
        private Dictionary<ProductionSO, SoldierBase> soldierDb;
        internal SoldierBase GetSoldier(ProductionSO production, Vector3 position)
        {
            if (soldierDb == null)
            {
                soldierDb = new Dictionary<ProductionSO, SoldierBase>();
                foreach (ProductionSO prod in productions)
                {
                    soldierDb.Add(prod, soldiers.Find((soldier) => soldier.data == (prod.product as SoldierSO)));
                }
            }
            return Instantiate(soldierDb[production], position, Quaternion.identity);
        }
    }
}