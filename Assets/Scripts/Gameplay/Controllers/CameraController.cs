using BaridaGames.PanteonCaseProject.Utilities;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 20f;

        private Rect bounds;
        private Vector2 direction = default;
        private Vector3 lastMousePosition;
        private const string HORIZONTAL = "Horizontal";
        private const string VERTICAL = "Vertical";

        private void Update()
        {
            direction.x = Input.GetAxisRaw(HORIZONTAL);
            direction.y = Input.GetAxisRaw(VERTICAL);
            Move(direction.normalized);

            if (MouseHelper.LeftClickHold)
            {
                Move((lastMousePosition - MouseHelper.Position).normalized);
            }
            lastMousePosition = MouseHelper.Position;
        }

        internal void Move(Vector2 direction)
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            if (!bounds.Contains(transform.position))
            {
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, bounds.xMin, bounds.xMax),
                    Mathf.Clamp(transform.position.y, bounds.yMin, bounds.yMax),
                    transform.position.z);
            }
        }

        internal void SetBounds(Vector2 center, Vector2 size)
        {
            bounds.x = center.x;
            bounds.y = center.y;
            bounds.size = size;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
    }
}