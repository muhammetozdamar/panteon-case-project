using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Data
{
    [CreateAssetMenu(fileName = "Product Referance", menuName = "Panteon Games Case/Product Referance", order = 0)]
    public class ProductReferance : ScriptableObject
    {
        [SerializeField] internal ScriptableObject product;
        private void OnValidate()
        {
            if (product != null && !(product is IProduct))
            {
                Debug.LogError($"{product.name} is not a product!");
                product = null;
            }
        }
    }
}