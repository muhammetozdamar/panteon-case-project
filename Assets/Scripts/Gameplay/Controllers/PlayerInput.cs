using BaridaGames.PanteonCaseProject.Utilities;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private LayerMask selectableLayerMask;
        private ISelectable currentSelectable;
        private void Update()
        {
            if (MouseHelper.LeftClickDown)
            {
                if (currentSelectable != null)
                {
                    currentSelectable.OnDeselected();
                    currentSelectable = null;
                }
                if (RayHelper.TryGetComponentFromMouse<ISelectable>(selectableLayerMask, out currentSelectable))
                {
                    currentSelectable.OnSelected();
                }
            }
        }
    }
}