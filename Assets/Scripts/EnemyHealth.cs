using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxLife = 1;
    int currentLife;

    void Start()
    {
        currentLife = maxLife;
    }

    public void TakeDamage(int damage = 1)
    {
        currentLife -= damage;

        if (currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}
