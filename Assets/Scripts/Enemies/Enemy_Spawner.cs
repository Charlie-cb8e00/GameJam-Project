using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;        // Array con los tipos de enemigos
    public int maxEnemies = 10;              // Máximo enemigos en escena
    public float spawnInterval = 5f;         // Tiempo entre spawns
    public float minDistanceFromPlayer = 5f; // Distancia mínima desde el jugador

    public Vector3 spawnCenter = Vector3.zero; // Centro del cuadrado
    public float spawnSize = 10f;             // Tamaño del cuadrado (10x10)

    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private Transform jugador;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Limpiamos enemigos destruidos
            spawnedEnemies.RemoveAll(e => e == null);

            if (spawnedEnemies.Count < maxEnemies && enemyPrefabs.Length > 0)
            {
                Vector3 spawnPos = Vector3.zero;
                bool validSpawn = false;

                // Intentamos hasta 10 veces encontrar un spawn válido
                for (int i = 0; i < 10; i++)
                {
                    float x = Random.Range(-spawnSize / 2f, spawnSize / 2f) + spawnCenter.x;
                    float z = Random.Range(-spawnSize / 2f, spawnSize / 2f) + spawnCenter.z;
                    spawnPos = new Vector3(x, 0f, z);

                    if (Vector3.Distance(spawnPos, jugador.position) >= minDistanceFromPlayer)
                    {
                        validSpawn = true;
                        break;
                    }
                }

                if (validSpawn)
                {
                    // Elegimos un tipo de enemigo aleatorio
                    GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                    GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                    spawnedEnemies.Add(enemy);
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Opcional: dibujar el área de spawn en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(spawnCenter + new Vector3(0, 0.5f, 0), new Vector3(spawnSize, 1f, spawnSize));
    }
}
