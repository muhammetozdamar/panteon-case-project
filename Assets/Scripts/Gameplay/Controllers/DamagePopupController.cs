using BaridaGames.PanteonCaseProject.Utilities;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class DamagePopupController : MonoBehaviourSingleton<DamagePopupController>
    {
        [SerializeField] private DamagePopupPool pool;

        public void OnDamage(float damage, Vector2 position)
        {
            DamagePopup popup = pool.GetObject();
            popup.Initialize(damage, position);
        }
    }
}