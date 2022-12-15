using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class RegularSoldier : SoldierBase
    {
        public override void Attack(UnitBase targetUnit)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanAttack(UnitBase targetUnit)
        {
            // can add spesific checks later if needed.
            return true;
        }

        public override bool CanMove(Vector2 targetPosition)
        {
            throw new System.NotImplementedException();
        }

        public override void Move(Vector2 targetPosition)
        {
            throw new System.NotImplementedException();
        }
    }
}