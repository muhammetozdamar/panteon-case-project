using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay.Astar
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private Rect bounds = new Rect(Vector2.zero, Vector2.one * 100f);
        [SerializeField] private float tileHalfSize = 0.5f;
        [SerializeField] private SpriteRenderer ground = default;


        private Grid grid;
        private void Awake()
        {
            grid = new Grid(bounds, tileHalfSize);
            ground.size = bounds.size;
            ground.transform.localPosition = bounds.center;
            ground.GetComponent<BoxCollider2D>().size = bounds.size;
        }

        private void OnDrawGizmosSelected()
        {
            if (grid != null)
            {
                grid.Tiles[5, 4].isOccupied = true;
                foreach (GridTile n in grid.Tiles)
                {
                    Gizmos.color = (!n.isOccupied) ? Color.white : Color.red;
                    Gizmos.DrawWireCube(n.worldPosition, Vector2.one * (tileHalfSize));
                }
            }
        }
    }
}