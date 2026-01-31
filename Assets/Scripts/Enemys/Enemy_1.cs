using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    public float velocidad = 10f;
    public Transform jugador;
    public float attackRange = 2f;
    public int damage = 1;
    public float attackCooldown = 1f;

    private Rigidbody rb;
    private float lastAttackTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = -attackCooldown;
    }

    void FixedUpdate()
    {
        if (jugador != null)
        {
            Vector3 direccion = (jugador.position - transform.position).normalized;
            rb.MovePosition(rb.position + direccion * velocidad * Time.fixedDeltaTime);

            float distancia = Vector3.Distance(transform.position, jugador.position);
            if (distancia <= attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                MeleeAttack();
                lastAttackTime = Time.time;
            }
        }
    }

    void MeleeAttack()
    {
        Debug.Log("Jugador recibe " + damage + " de daño por enemigo melee.");
        jugador.GetComponent<PlayerHealth>().TakeDamage(damage);
    }
}
