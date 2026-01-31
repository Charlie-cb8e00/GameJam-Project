
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public HealthBar healthbar;
    public bool isHurt = false;
    public Animator animator;

    public InputActionReference inputAction;


    void Start()
    {
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);

        inputAction.action.Enable();
    }

    public void Update()
    {
        Vector2 movement = inputAction.action.ReadValue<Vector2>();
    }

    public void TakeDamage(int damage)
    {
        isHurt = true;
        animator.SetBool("isHurt", isHurt);
        StartCoroutine(hurtCooldown());
        animator.SetBool("isHurt", isHurt);
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Jugador muerto");
            Destroy(gameObject);
        }
    }
    IEnumerator hurtCooldown()
    {
        yield return new WaitForSeconds(.5f);
        isHurt = false;
        Debug.Log(isHurt);
    }
}
