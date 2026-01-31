using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    public Transform FirePoint;
    public float range = 100f;
    public Camera mainCamera;
    public int damage = 1;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit, range))
        {
            targetPoint = hit.point;

            EnemyHealth enemy = hit.collider.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
                enemy.TakeDamage(damage);
        }
        else
        {
            targetPoint = ray.origin + ray.direction * range;
        }

        Vector3 shootDir = (targetPoint - FirePoint.position).normalized;

        Debug.DrawRay(FirePoint.position, shootDir * range, Color.red, 0.1f);
    }

}
