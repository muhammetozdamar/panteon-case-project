using BaridaGames.PanteonCaseProject.Data;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public abstract class SoldierBase : UnitBase
    {
        internal float Damage => (data as SoldierSO).damage;
        public abstract bool CanMove(Vector2 targetPosition);
        public abstract bool CanAttack(UnitBase targetUnit);
        public abstract void Move(Vector2 targetPosition);
        public abstract void Attack(UnitBase targetUnit);
    }
}