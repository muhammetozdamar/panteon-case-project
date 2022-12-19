using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Utilities
{
    public static class RayHelper
    {
        private static Camera _camera;
        private static Camera camera
        {
            get
            {
                if (_camera == null) _camera = Camera.main;
                return _camera;
            }
        }

        public static RaycastHit2D GetRaycastHitFromMouse(LayerMask layerMask)
        {
            Ray ray = camera.ScreenPointToRay(MouseHelper.Position);
            return Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
        }

        public static Vector2 GetHitPointFromMouse(LayerMask layerMask)
        {
            return GetRaycastHitFromMouse(layerMask).point;
        }
        public static bool TryGetComponentFromMouse<T>(LayerMask layerMask, out T component) where T : class
        {
            RaycastHit2D hit = GetRaycastHitFromMouse(layerMask);
            if (hit.transform == null)
            {
                component = null;
                return false;
            }
            if (hit.transform.TryGetComponent<T>(out component))
            {
                return true;
            }
            component = null;
            return false;
        }
    }
}