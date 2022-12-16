using System;
using BaridaGames.PanteonCaseProject.Gameplay.Astar;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class SoldierController : MonoBehaviour
    {
        public static SoldierController Instance;
        [SerializeField] private LayerMask groundLayerMask = default;
        private SoldierBase currentSoldier;
        private readonly Vector2 offset = Vector2.left * 0.5f + Vector2.down * 0.5f;

        private void Awake()
        {
            Instance = this;
        }
        private void Update()
        {
            if (currentSoldier == null) return;
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, groundLayerMask);
                if (hit.transform != null)
                {
                    currentSoldier.Move(hit.point);
                }
            }

        }

        internal void SetCurrentSoldier(SoldierBase soldier)
        {
            currentSoldier = soldier;
        }
    }
}