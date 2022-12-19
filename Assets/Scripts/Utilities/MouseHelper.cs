using UnityEngine;
using UnityEngine.EventSystems;

namespace BaridaGames.PanteonCaseProject.Utilities
{
    public static class MouseHelper
    {
        public static bool OnUI => (EventSystem.current.IsPointerOverGameObject());
        public static bool LeftClickDown => Input.GetMouseButtonDown(0) && !OnUI;
        public static bool LeftClickUp => Input.GetMouseButtonUp(0) && !OnUI;
        public static bool LeftClickHold => Input.GetMouseButton(0) && !OnUI;
        public static bool RightClickDown => Input.GetMouseButtonDown(1) && !OnUI;
        public static bool RightClickUp => Input.GetMouseButtonUp(1) && !OnUI;
        public static bool RightClickHold => Input.GetMouseButton(1) && !OnUI;
        public static Vector3 Position => Input.mousePosition;
    }
}