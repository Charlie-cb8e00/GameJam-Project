using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_3 : MonoBehaviour
{
    [Header("Stats")]
    public int damage = 3;
    public float pushForce = 1f;          // Ajusta la distancia del empuje
    public float pushDuration = 0.2f;     // Duraci�n del empuje
    public float attackCooldown = 1.5f;
    public float attackDuration = 0.3f;
    public float recoveryTime = 1.0f;

    private NavMeshAgent agent;
    private Transform jugador;
    private float lastAttackTime;
    private bool isAttacking = false;
    public Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        jugador = GameObject.FindGameObjectWithTag("Player")?.transform;
        lastAttackTime = -attackCooldown;
    }

    void Update()
    {
        if (jugador == null || isAttacking) return;
        agent.SetDestination(jugador.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Player")) return;
        if (Time.time < lastAttackTime + attackCooldown || isAttacking) return;

        Rigidbody playerRb = collision.collider.GetComponent<Rigidbody>();
        PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();

        if (playerHealth != null)
            playerHealth.TakeDamage(damage);

        if (playerRb != null)
            StartCoroutine(AttackPlayerSafe(playerRb));

        lastAttackTime = Time.time;
        Debug.Log("Enemigo ataca y hace " + damage + " de da�o");
    }

    IEnumerator AttackPlayerSafe(Rigidbody playerRb)
    {
        isAttacking = true;
        animator.SetBool("isAttacking", isAttacking);
        agent.isStopped = true;

        Vector3 pushDir = (playerRb.position - transform.position).normalized;
        float elapsed = 0f;
        float speed = pushForce / pushDuration;

        while (elapsed < pushDuration)
        {
            // MovePosition respeta colisiones
            Vector3 newPos = playerRb.position + pushDir * speed * Time.fixedDeltaTime;
            playerRb.MovePosition(newPos);

            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(attackDuration + recoveryTime);

        agent.isStopped = false;
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
    }
}
