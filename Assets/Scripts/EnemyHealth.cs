using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int life = 1;

    public void Die()
    {
        Destroy(gameObject);
    }
}
