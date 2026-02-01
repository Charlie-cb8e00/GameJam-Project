
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 1;
    public int currentHealth;
    public bool isHurt = false;
    public Animator animator;

    //public GameObject trailGO;

    void Awake()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(int damage = 1)
    {
        currentHealth -= damage;
<<<<<<< Updated upstream
        isHurt = true;
        animator.SetBool("isHurt", isHurt);
        StartCoroutine(hurtCooldown());
        Debug.Log(gameObject.name + " ha recibido " + damage + " de da�o. Vida restante: " + currentHealth);
        //partículas
        //GameObject go = Instantiate(trailGO, this.gameObject.transform.position, Quaternion.identity);
=======
        Debug.Log(gameObject.name + " ha recibido " + damage + " de da�o. Vida restante: " + currentHealth);
>>>>>>> Stashed changes

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
