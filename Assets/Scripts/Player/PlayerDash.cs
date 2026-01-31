using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerDash : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public InputActionReference dashAction;

    [SerializeField] private float dashSpeed = 110f;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldown = 0.9f;
    [SerializeField] private float invincibilityDuration = 0.18f;  // mismo tiempo que dashDuration o un poco más
    public bool IsInvincible { get; private set; } = false;

    private bool isDashing = false;
    private bool onCooldown = false;

    private Rigidbody rb;           // usamos el mismo Rigidbody que ya tienes

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Falta Rigidbody en el Player para el Dash");
        }
    }

    private void OnEnable()
    {
        if (dashAction?.action != null)
        {
            dashAction.action.Enable();
            dashAction.action.performed += OnDashPerformed;
        }
    }

    private void OnDisable()
    {
        if (dashAction?.action != null)
        {
            dashAction.action.performed -= OnDashPerformed;
            dashAction.action.Disable();
        }
    }

    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        if (isDashing || onCooldown || rb == null) return;

        // Obtenemos la dirección actual del input (WASD / joystick)
        Vector2 inputDirection = playerMovement.inputAction.action.ReadValue<Vector2>();

        Vector3 dashDirection;

        // Si hay input de movimiento → dash en esa dirección
        if (inputDirection.sqrMagnitude > 0.1f)
        {
            dashDirection = new Vector3(inputDirection.x, 0f, inputDirection.y).normalized;
        }
        // Si no hay input → dash en la dirección hacia la que está mirando el personaje
        else
        {
            // Usamos la dirección actual del transform (hacia donde apunta el GameObject)
            dashDirection = transform.forward.normalized;

            //Otra opción:  dashDirection = new Vector3(Mathf.Sign(transform.localScale.x), 0f, 0f);
        }

        StartCoroutine(PerformDash(dashDirection));
    }

    private IEnumerator PerformDash(Vector3 direction)
    {
        isDashing = true;
        onCooldown = true;
        IsInvincible = true;   // ← activamos invencibilidad

        float originalDrag = rb.drag;
        rb.drag = 0f;

        float timer = 0f;

        while (timer < dashDuration)
        {
            rb.velocity = direction * dashSpeed;
            timer += Time.deltaTime;
            yield return null;
        }

        rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        rb.drag = originalDrag;

        isDashing = false;

        // Esperamos el resto del tiempo de invencibilidad si es más largo que el dash
        float remainingInvincibility = invincibilityDuration - dashDuration;
        if (remainingInvincibility > 0)
        {
            yield return new WaitForSeconds(remainingInvincibility);
        }

        IsInvincible = false;   // ← terminamos invencibilidad

        yield return new WaitForSeconds(dashCooldown);
        onCooldown = false;
    }

    // Métodos públicos útiles para otros scripts
    public bool IsDashing() => isDashing;
}
