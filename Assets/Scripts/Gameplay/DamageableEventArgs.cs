using System;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public class DamageableEventArgs : EventArgs
    {
        public float maxHealth;
        public float currentHealth;
        public float damageAmount;
    }
}