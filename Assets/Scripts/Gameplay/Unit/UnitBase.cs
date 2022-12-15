using System;
using BaridaGames.PanteonCaseProject.Data;
using BaridaGames.PanteonCaseProject.Gameplay.UI;
using UnityEngine;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public abstract class UnitBase : MonoBehaviour, IDamageable, IInteractable
    {
        [SerializeField] internal UnitSO data;
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
            OnDamagedEvent(new DamageableEventArgs { currentHealth = health, maxHealth = data.MaxHealth, damageAmount = value });
            if (health <= 0)
            {
                OnDeath();
                return true;
            }
            return false;
        }


        internal virtual void OnDamagedEvent(DamageableEventArgs e)
        {
            OnDamaged?.Invoke(this, e);
        }

        public virtual void OnDeath()
        {
            Debug.Log($"{gameObject.name}({data.name}) died!");
            Destroy(gameObject);
        }

        public virtual void OnMouseDown()
        {
            InformationPanelController.Instance.SetCurrentUnit(this);
        }

        public virtual void OnMouseHold()
        {
            return;
        }

        public virtual void OnMouseUp()
        {
            return;
        }
    }
}