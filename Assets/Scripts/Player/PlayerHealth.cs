
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


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

        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);

        if (currentHealth <= 2)
        {
            Debug.Log("Jugador muerto");
            SceneManager.LoadScene("Game_Over");
        }
    }

    IEnumerator hurtCooldown()
    {
        yield return new WaitForSeconds(.5f);
        isHurt = false;
        animator.SetBool("isHurt", isHurt);
        Debug.Log(isHurt);
    }
}
