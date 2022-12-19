using BaridaGames.PanteonCaseProject.Data;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public abstract class SoldierBase : UnitBase
    {
        internal float Damage => (data as SoldierSO).damage;
        internal float AttackSpeed => (data as SoldierSO).attackSpeed;
        internal float AttackRange => (data as SoldierSO).attackRange;
        internal float MoveSpeed => (data as SoldierSO).moveSpeed;

        public abstract bool CanMove(Vector2 targetPosition);
        public abstract void Move(Vector2 targetPosition);
        public abstract bool CanAttack(UnitBase target);
        public abstract void Attack(UnitBase target);

        public override void OnSelected()
        {
            SoldierController.Instance.SetCurrentSoldier(this);
            base.OnSelected();
        }

        public override void OnDeselected()
        {
            SoldierController.Instance.SetCurrentSoldier(null);
            base.OnDeselected();
        }
    }
}