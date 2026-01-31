using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
    public float velocidad = 3f;
    public Transform jugador;
    public float attackRange = 10f;
    public int damage = 1;
    public float attackCooldown = 5f;
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
            StartCoroutine(RangedAttack());
            lastAttackTime = Time.time;
        }
    }

    IEnumerator RangedAttack()
    {
        isAttacking = true;

        RaycastHit hit;
        Vector3 direction = (jugador.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, direction, out hit, attackRange))
        {
            Debug.DrawRay(transform.position, direction * hit.distance, Color.blue, 0.1f);
            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<PlayerHealth>()?.TakeDamage(damage);
                Debug.Log("Jugador recibe " + damage + " de daño por disparo enemigo.");
            }
        }

        yield return new WaitForSeconds(attackDuration);

        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
