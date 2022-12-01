using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float maxHealth;

    [SerializeField] protected bool isDead;

    private void Start()
    {
        InitVariables();
    }

    public virtual void CheckHealth()
    {
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        if (health >= maxHealth)
        {
            health -= maxHealth;
        }
    }

    public virtual void Die()
    {
        isDead = true;
    }

    public void SetHealthTo(float healthToSetTo)
    {
        health = healthToSetTo;
        CheckHealth();
    }

    public virtual void TakeDamage(float damage)
    {
        float healthAfterDamage = health - damage;
        SetHealthTo(healthAfterDamage);
    }


    public virtual void InitVariables()
    {
        maxHealth = 100;
        SetHealthTo(maxHealth);
        isDead = false;
    }
}



