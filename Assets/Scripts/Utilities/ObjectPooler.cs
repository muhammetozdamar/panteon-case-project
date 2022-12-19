using System.Collections.Generic;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Utilities
{
    public class ObjectPooler<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] internal Transform parent = default;
        [SerializeField] internal T prefab = default;
        [SerializeField] internal int startCount = default;
        internal List<T> pool = new List<T>();
        internal virtual void Awake()
        {
            for (int i = 0; i < startCount; i++)
            {
                T item = Instantiate(prefab, parent);
                item.gameObject.SetActive(false);
                pool.Add(item);
            }
        }

        internal virtual T GetObject()
        {
            T item = pool.Find((obj) => !obj.gameObject.activeInHierarchy);
            if (item == null)
            {
                item = Instantiate(prefab, parent);
                item.gameObject.SetActive(false);
                pool.Add(item);
            }
            return item;
        }
    }
}