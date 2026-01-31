using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [Header("Disparo")]
    public Transform FirePoint;      
    public float range = 100f;       
    public int damage = 1;           

    [Header("Cámara")]
    public Camera mainCamera;        

    [Header("Input")]
    public InputActionReference shootAction; 

    void Start()
    {
        if (shootAction != null)
            shootAction.action.Enable();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Disparo con Input System
        if (shootAction != null && shootAction.action.WasPressedThisFrame())
        {
            Shoot();
        }
        // Fallback ratón
        else if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Raycast directo desde la cámara hacia adelante
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit, range))
        {
            targetPoint = hit.point;

            // Aplicar daño
            EnemyHealth enemy = hit.collider.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
                enemy.TakeDamage(damage);
        }
        else
        {
            targetPoint = ray.origin + ray.direction * range;
        }

        // Dirección del disparo desde el FirePoint
        Vector3 shootDir = (targetPoint - FirePoint.position).normalized;
    }

}
