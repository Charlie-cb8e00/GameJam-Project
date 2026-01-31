using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [Header("Disparo")]
    public Transform FirePoint;
    public float range = 100f;
    public int damage = 1;
    public Animator animator;

    bool isShooting = false;

    [Header("C�mara")]
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
        // Fallback rat�n
        else if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        isShooting = true;
        animator.SetBool("isShooting", isShooting);
        StartCoroutine(shootingCooldown());
        // Raycast directo desde la c�mara hacia adelante
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit, range))
        {
            targetPoint = hit.point;

            // Aplicar da�o
            EnemyHealth enemy = hit.collider.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
                enemy.TakeDamage(damage);
        }
        else
        {
            targetPoint = ray.origin + ray.direction * range;
        }

        // Direcci�n del disparo desde el FirePoint
        Vector3 shootDir = (targetPoint - FirePoint.position).normalized;
    }
    IEnumerator shootingCooldown()
    {
        yield return new WaitForSeconds(1);
        isShooting = false;
        animator.SetBool("isShooting", isShooting);
        Debug.Log(isShooting);
    }

}
