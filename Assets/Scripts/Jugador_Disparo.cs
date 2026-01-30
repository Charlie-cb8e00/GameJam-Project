using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador_Disparo : MonoBehaviour
{
    public Camera playerCamera;
    public float range = 100f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red, 1f);

            if (hit.collider.CompareTag("WeakPoint"))
            {
                EnemyHealth enemy = hit.collider.GetComponentInParent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.Die();
                }
            }
        }
    }
}
