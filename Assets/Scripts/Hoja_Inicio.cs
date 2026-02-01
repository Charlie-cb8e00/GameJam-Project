using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hoja_Inicio : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelInfo; // Imagen o panel del Canvas

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        panelInfo.SetActive(false);
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    panelInfo.SetActive(true);
                }
            }
        }
    }
}
