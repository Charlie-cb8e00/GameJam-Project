using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_1 : MonoBehaviour
{
    public Transform jugador;
    public float attackRange = 2f;
    public int damage = 1;
    public float attackCooldown = 1f;
    public float attackDuration = 0.3f;

    private NavMeshAgent agent;
    private float lastAttackTime;
    private bool isAttacking = false;

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
            StartCoroutine(MeleeAttack());
            lastAttackTime = Time.time;
        }
    }

    IEnumerator MeleeAttack()
    {
        isAttacking = true;

        agent.isStopped = true;

        jugador.GetComponent<PlayerHealth>().TakeDamage(damage);
        Debug.Log("Jugador recibe " + damage + " de daño por enemigo melee.");

        yield return new WaitForSeconds(attackDuration);

        isAttacking = false;
        agent.isStopped = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
