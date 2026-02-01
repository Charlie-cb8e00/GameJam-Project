using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;  // ← Arrastra tu PlayerMovement aquí (para acceder a cam e input)
    [SerializeField] private InputActionReference dashAction;

    [SerializeField] private float dashSpeed = 110f;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldown = 0.9f;
    [SerializeField] private float invincibilityDuration = 0.18f;
    public bool IsInvincible { get; private set; } = false;

    private bool isDashing = false;
    private bool onCooldown = false;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Falta Rigidbody en el Player para el Dash");
        }
        if (playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
            if (playerMovement == null)
            {
                Debug.LogError("Falta PlayerMovement en el mismo GameObject o asignado manualmente");
            }
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
        if (isDashing || onCooldown || rb == null || playerMovement == null) return;

        // Leemos el input actual de movimiento (mismo que PlayerMovement)
        Vector2 inputDirection = playerMovement.inputAction.action.ReadValue<Vector2>();

        Vector3 dashDirection;

        if (inputDirection.sqrMagnitude > 0.1f)
        {
            // ← CAMBIO PRINCIPAL: Dirección RELATIVA A LA CÁMARA (igual que en PlayerMovement)
            Vector3 camForward = playerMovement.cam.forward;
            Vector3 camRight = playerMovement.cam.right;

            camForward.y = 0f;
            camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

            dashDirection = (camForward * inputDirection.y + camRight * inputDirection.x).normalized;
        }
        else
        {
            // Sin input → dash hacia donde mira la cámara (horizontal)
            Vector3 camForward = playerMovement.cam.forward;
            camForward.y = 0f;
            dashDirection = camForward.normalized;
        }

        StartCoroutine(PerformDash(dashDirection));
    }

    private IEnumerator PerformDash(Vector3 direction)
    {
        isDashing = true;
        onCooldown = true;
        IsInvincible = true;

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

        float remainingInvincibility = invincibilityDuration - dashDuration;
        if (remainingInvincibility > 0)
        {
            yield return new WaitForSeconds(remainingInvincibility);
        }

        IsInvincible = false;

        yield return new WaitForSeconds(dashCooldown);
        onCooldown = false;
    }

    public bool IsDashing() => isDashing;
}
