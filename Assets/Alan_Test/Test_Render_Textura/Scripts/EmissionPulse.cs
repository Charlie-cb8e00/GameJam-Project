using UnityEngine;

public class EmissionPulse : MonoBehaviour
{
    public Color emissionColor = Color.cyan;
    public float maxIntensity = 2f;
    public float pulseSpeed = 2f;

    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        float intensity = Mathf.Abs(Mathf.Sin(Time.time * pulseSpeed)) * maxIntensity;
        material.SetColor("_EmissionColor", emissionColor * intensity);
    }
}
