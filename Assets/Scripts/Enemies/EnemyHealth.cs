using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 1; 
    public int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth; 
    }

    
    public void TakeDamage(int damage = 1)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " ha recibido " + damage + " de daño. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " ha muerto.");
        Destroy(gameObject);
    }

}
