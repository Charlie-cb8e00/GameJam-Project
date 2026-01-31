using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador_Disparo : MonoBehaviour
{
    public Transform FirePoint;

    private void Update()
    {
        Shooting();
    }

    public void Shooting()
    {
        RaycastHit hit;

        if (Physics.Raycast (FirePoint.position, transform.TransformDirection (Vector3.forward),out hit, 100))
        {
            Debug.DrawRay(FirePoint.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        }
    }
}
