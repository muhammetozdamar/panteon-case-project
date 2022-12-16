using BaridaGames.PanteonCaseProject.Data;
using BaridaGames.PanteonCaseProject.Gameplay.UI;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public abstract class SoldierBase : UnitBase
    {
        internal float Damage => (data as SoldierSO).damage;
        internal float MoveSpeed => (data as SoldierSO).moveSpeed;
        public abstract bool CanMove(Vector2 targetPosition);
        public abstract bool CanAttack(UnitBase targetUnit);
        public abstract void Move(Vector2 targetPosition);
        public abstract void Attack(UnitBase targetUnit);

        public override void OnMouseDown()
        {
            InformationPanelController.Instance.SetCurrentUnit(this);
            SoldierController.Instance.SetCurrentSoldier(this);
        }
    }
}