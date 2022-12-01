using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivation : MonoBehaviour
{
    public float damage = 10;
    public float time = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyStats enemy = other.transform.GetComponent<EnemyStats>();
            EnemyContoller movement = other.transform.GetComponent<EnemyContoller>();
            if(enemy != null)
            {
               
                movement.isMoving = false;

            }

        }
    }
}
