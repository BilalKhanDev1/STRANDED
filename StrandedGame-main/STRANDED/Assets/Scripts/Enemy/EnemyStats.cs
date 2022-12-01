using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    [SerializeField] private float damage;
    [SerializeField] public float attackSpeed;


    [SerializeField] private bool canAttack;
    public TimeProgressor isDay;
    public float Decay = 3;

    private void Start()
    {
        InitVariables();
        
    }

    private void Update()
    {
        if (isDay.dayTime)
        {
            TakeDamage(Decay);
        }
        
        Debug.Log("Enemy Health is: " + health);
    }

    public void DealDamage(CharacterStats statsToDamage)
    {
        statsToDamage.TakeDamage(damage);
    }

   
    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    public override void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0f)
        {
            Destroy(gameObject);

        }
    }
    public override void InitVariables()
    {

        maxHealth = 25;
        health = maxHealth;
        isDead = false;


        
        damage = 10;
        attackSpeed = 1.5f;
        canAttack = true;

    }
}
