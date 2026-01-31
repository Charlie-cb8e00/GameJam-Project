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
            Shooting();
        }
    }

    void Shooting()
    {
        RaycastHit hit;

        Ray mouseRay = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Cursor.visible = true;

        bool impacta = Physics.Raycast(mouseRay, out hit, range);

        Vector3 drawDirection = (impacta ? hit.point : mouseRay.origin + mouseRay.direction * range) - FirePoint.position;
        Debug.DrawRay(FirePoint.position, drawDirection, Color.red, 0.1f);

        if (impacta)
        {
            Debug.Log("Impacta con: " + hit.collider.name);

            EnemyHealth enemy = hit.collider.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        else
        {
            Debug.Log("No impacta con nada.");
        }
    }

}
