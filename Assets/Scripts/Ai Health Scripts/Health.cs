using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Floats")]
    public float Maxhealth;
    float CurrentHealth;

  //[Header("Ragdoll")]
  //AiRagdoll ragdoll; 
 

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = Maxhealth; //Sets the Ai's Current health to Max health once the game starts.
    }

    public void TakeDamage(float amount) 
    {
        CurrentHealth -= amount;

        if(CurrentHealth < 0) 
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        
    }
}
