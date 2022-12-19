using System;
using BaridaGames.PanteonCaseProject.Data;
using BaridaGames.PanteonCaseProject.Gameplay.UI;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public abstract class UnitBase : MonoBehaviour, IDamageable, ISelectable
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] internal UnitSO data;
        internal RectInt Bounds => data.bounds;
        internal float health;
        public virtual event EventHandler OnDamaged;
        public virtual event EventHandler OnDied;
        internal float Health => health;
        internal virtual void Start()
        {
            health = data.maxHealth;
        }
        public virtual bool OnDamage(float value)
        {
            health -= value;
            DamagePopupController.Instance.OnDamage(value, transform.position);
            OnDamagedEvent(new DamageableEventArgs { currentHealth = health, maxHealth = data.MaxHealth, damageAmount = value });
            if (health <= 0)
            {
                OnDeath();
                return true;
            }
            return false;
        }

        public virtual void OnDeath()
        {
            OnDiedEvent(null);
            Destroy(gameObject);
        }

        internal virtual void OnDamagedEvent(DamageableEventArgs e)
        {
            OnDamaged?.Invoke(this, e);
        }

        internal virtual void OnDiedEvent(DamageableEventArgs e)
        {
            OnDied?.Invoke(this, e);
        }

        public virtual void OnSelected()
        {
            InformationPanelController.Instance.SetCurrentUnit(this);
            spriteRenderer.color = Color.yellow;
        }

        public virtual void OnDeselected()
        {
            InformationPanelController.Instance.ResetPanel();
            if (spriteRenderer)
                spriteRenderer.color = Color.white;
        }
    }
}