using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 5;
    int currentLives;

    void Start()
    {
        currentLives = maxLives;
    }

    public void TakeDamage(int damage)
    {
        currentLives -= damage;
        Debug.Log("Vidas: " + currentLives);

        if (currentLives <= 0)
        {
            Debug.Log("Jugador muerto");
        }
    }
}
