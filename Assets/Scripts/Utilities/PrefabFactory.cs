using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Utilities
{
    public class PrefabFactory<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T prefab;
        internal virtual T GetPrefab()
        {
            return Instantiate(prefab);
        }
    }
}