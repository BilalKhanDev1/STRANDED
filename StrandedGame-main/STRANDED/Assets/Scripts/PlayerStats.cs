using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
        
    }
    private void Update()
    {
        
        Debug.Log("Player Health is: " + health);
    }

    public override void InitVariables()
    {
        maxHealth = 150;
        health = maxHealth;
        isDead = false;
        
    }

    public override void Die()
    {
        Debug.Log("You're Dead");
       
    }


}
