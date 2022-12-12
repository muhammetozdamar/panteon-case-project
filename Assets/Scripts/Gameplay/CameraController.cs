using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 20f;
        [SerializeField] private Rect bounds;


        private Vector2 direction = default;
        private const string HORIZONTAL = "Horizontal";
        private const string VERTICAL = "Vertical";

        private void Update()
        {
            direction.x = Input.GetAxisRaw(HORIZONTAL);
            direction.y = Input.GetAxisRaw(VERTICAL);
            Move(direction);
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
    }
}