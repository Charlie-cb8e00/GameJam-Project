using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_2 : MonoBehaviour
{
    public Transform jugador;
    public float attackRange = 10f;
    public int damage = 1;
    public float attackCooldown = 5f;
    public float attackDuration = 0.3f;

    private NavMeshAgent agent;
    private float lastAttackTime;
    private bool isAttacking = false;
    public Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = -attackCooldown;
    }

    void Update()
    {
        if (jugador == null) return;

        if (!isAttacking)
        {
            agent.SetDestination(jugador.position);
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
        animator.SetBool("isAttacking", isAttacking);
        agent.isStopped = true;

        Vector3 direction = (jugador.position - transform.position).normalized;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, attackRange))
        {
            Debug.DrawRay(transform.position, direction * hit.distance, Color.blue, 0.1f);
            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<PlayerHealth>()?.TakeDamage(damage);
                Debug.Log("Jugador recibe " + damage + " de da�o por disparo enemigo.");
            }
        }

        yield return new WaitForSeconds(attackDuration);

        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
        agent.isStopped = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
