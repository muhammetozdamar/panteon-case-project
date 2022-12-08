using System;
using BaridaGames.PanteonCaseProject.Data;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, IDamageable
{
    [SerializeField] internal UnitSO data;
    internal float health;

    internal float Health => health;
    private void Start()
    {
        health = data.maxHealth;
    }
    public bool OnDamage(float value)
    {
        health -= value;
        if (health <= 0)
        {
            OnDeath();
            return true;
        }
        return false;
    }

    public void OnDeath()
    {
        Debug.Log($"{gameObject.name}({data.name}) died!");
        Destroy(gameObject);
    }
}