
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public HealthBar healthbar;

    public InputActionReference inputAction;
    private PlayerDash playerDash;


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
        if (playerDash.IsInvincible) return;
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Jugador muerto");
            Destroy(gameObject);
        }
    }
}
