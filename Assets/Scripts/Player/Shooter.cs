using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    public Transform FirePoint;
    public float range = 100f;
    public Camera mainCamera;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shooting();
        }
    }

    void Shooting()
    {
        RaycastHit hit;

        Ray mouseRay = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        bool impacta = Physics.Raycast(mouseRay, out hit, range);

        Vector3 drawDirection = (impacta ? hit.point : mouseRay.origin + mouseRay.direction * range) - FirePoint.position;
        Debug.DrawRay(FirePoint.position, drawDirection, Color.red, 0.1f);

        if (impacta)
        {
            Debug.Log("Impacta con: " + hit.collider.name);
        }
        else
        {
            Debug.Log("No impacta con nada.");
        }
    }

}
