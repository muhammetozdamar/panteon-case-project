using BaridaGames.PanteonCaseProject.Utilities;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class ProductUIFactory : PrefabFactory<ProductUI>
    {
        public static ProductUIFactory Instance;
        [SerializeField] private RectTransform parent;
        private void Awake()
        {
            Instance = this;
        }
        internal override ProductUI GetPrefab()
        {
            ProductUI ui = base.GetPrefab();
            ui.transform.SetParent(parent);
            return ui;
        }
    }
}

