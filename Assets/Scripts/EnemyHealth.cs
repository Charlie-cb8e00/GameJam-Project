using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 1;  // vida inicial
    private int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    // Llamamos a este método para que el enemigo reciba daño
    public void TakeDamage(int damage)
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
        Destroy(gameObject); // destruye el objeto enemigo
    }
}
