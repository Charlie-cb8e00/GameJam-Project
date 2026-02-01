using UnityEngine;

public class FloatingRotate : MonoBehaviour
{
    [Header("Rotación")]
    public Vector3 rotationAxis = Vector3.up;
    public float rotationSpeed = 50f;

    [Header("Movimiento Vertical")]
    public float floatSpeed = 2f;
    public float floatHeight = 0.25f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Rotación configurable
        transform.Rotate(rotationAxis.normalized * rotationSpeed * Time.deltaTime);

        // Movimiento flotante
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
