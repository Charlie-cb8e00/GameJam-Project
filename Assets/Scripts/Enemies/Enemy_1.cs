using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    public float velocidad = 6f;
    public Transform jugador;
    public float attackRange = 2f;
    public int damage = 1;
    public float attackCooldown = 1f;
    public float attackDuration = 0.3f;

    private Rigidbody rb;
    private float lastAttackTime;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = -attackCooldown;
    }

    void FixedUpdate()
    {
        if (jugador == null) return;

        if (!isAttacking)
        {
            Vector3 direccion = (jugador.position - transform.position).normalized;
            rb.MovePosition(rb.position + direccion * velocidad * Time.fixedDeltaTime);
        }

        float distancia = Vector3.Distance(transform.position, jugador.position);
        if (distancia <= attackRange && Time.time >= lastAttackTime + attackCooldown && !isAttacking)
        {
            StartCoroutine(MeleeAttack());
            lastAttackTime = Time.time;
        }
    }

    IEnumerator MeleeAttack()
    {
        isAttacking = true;

        jugador.GetComponent<PlayerHealth>().TakeDamage(damage);
        Debug.Log("Jugador recibe " + damage + " de daño por enemigo melee.");

        yield return new WaitForSeconds(attackDuration);

        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
