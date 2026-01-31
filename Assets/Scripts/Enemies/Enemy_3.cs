using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 : MonoBehaviour
{
    public float velocidad = 2f;
    public float pushForce = 12f;
    public int damage = 3;
    public float attackCooldown = 1.5f;

    private Rigidbody rb;
    private Transform jugador;
    private float lastAttackTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = -attackCooldown;
    }

    void FixedUpdate()
    {
        if (jugador == null) return;

        Vector3 direccion = (jugador.position - transform.position).normalized;
        rb.MovePosition(rb.position + direccion * velocidad * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        if (Time.time < lastAttackTime + attackCooldown) return;

        Rigidbody playerRb = collision.collider.GetComponent<Rigidbody>();
        PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();

        if (playerRb != null)
        {
            Vector3 pushDir = (collision.transform.position - transform.position).normalized;
            playerRb.AddForce(pushDir * pushForce, ForceMode.Impulse);
        }

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        lastAttackTime = Time.time;
        Debug.Log("Enemigo empuja y hace " + damage + " de daño");
    }
}
