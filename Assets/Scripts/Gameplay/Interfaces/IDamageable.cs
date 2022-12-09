using System;

namespace BaridaGames.PanteonCaseProject.Gameplay
{
    public interface IDamageable
    {
        public bool OnDamage(float value);
        public void OnDeath();
        public event EventHandler OnDamaged;
        public event EventHandler OnDied;
    }
}