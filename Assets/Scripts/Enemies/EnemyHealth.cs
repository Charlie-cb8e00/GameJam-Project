using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 1;
    public int currentHealth;
    public bool isHurt = false;
    public Animator animator;
    public Slider barraVida;
    public AudioSource buttonSound;

    //public GameObject trailGO;

    private void Start()
    {
        if (barraVida != null)
        {
            barraVida.maxValue = maxHealth;
            barraVida.value = currentHealth;
        }
    }

    void Awake()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(int damage = 1)
    {
        currentHealth -= damage;
        if (barraVida != null)
            barraVida.value = currentHealth;

        isHurt = true;
        buttonSound.Play();
        animator.SetBool("isHurt", isHurt);
        StartCoroutine(hurtCooldown());
        Debug.Log(gameObject.name + " ha recibido " + damage + " de da�o. Vida restante: " + currentHealth);
        //partículas
        //GameObject go = Instantiate(trailGO, this.gameObject.transform.position, Quaternion.identity);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " ha muerto.");
        Destroy(gameObject);
    }
    IEnumerator hurtCooldown()
    {
        yield return new WaitForSeconds(.5f);
        isHurt = false;
        animator.SetBool("isHurt", isHurt);
        Debug.Log(isHurt);
    }

}
