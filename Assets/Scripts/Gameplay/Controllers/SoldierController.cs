using BaridaGames.PanteonCaseProject.Utilities;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class SoldierController : MonoBehaviourSingleton<SoldierController>
    {
        [SerializeField] private LayerMask groundLayerMask = default;
        [SerializeField] private LayerMask unitLayerMask = default;
        private SoldierBase currentSoldier;
        private void Update()
        {
            if (currentSoldier == null) return;
            if (MouseHelper.RightClickDown)
            {
                if (RayHelper.TryGetComponentFromMouse<UnitBase>(unitLayerMask, out UnitBase target))
                {
                    if (currentSoldier.CanAttack(target))
                    {
                        currentSoldier.Attack(target);
                    }
                }
                else
                {
                    Vector2 hitPoint = RayHelper.GetHitPointFromMouse(groundLayerMask);
                    if (currentSoldier.CanMove(hitPoint))
                        currentSoldier.Move(hitPoint);
                }
            }
        }

        internal void SetCurrentSoldier(SoldierBase soldier)
        {
            currentSoldier = soldier;
        }
    }
}