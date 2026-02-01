using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject container;

    [Header("Input")]
    [SerializeField] private InputActionReference pauseAction;

    [Header("Player Input (Optional)")]
    [SerializeField] private PlayerInput playerInput;  // Drag PlayerInput for action map switching

    public static bool GameIsPaused = false;

    private void Awake()
    {
        // ← AQUÍ: Ocultamos TODO al inicio (desaparece por defecto)
        pauseMenuPanel?.SetActive(false);
        container?.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    private void OnEnable()
    {
        if (pauseAction?.action != null)
        {
            pauseAction.action.Enable();
            pauseAction.action.performed += OnPausePerformed;
        }
    }

    private void OnDisable()
    {
        if (pauseAction?.action != null)
        {
            pauseAction.action.performed -= OnPausePerformed;
            pauseAction.action.Disable();
        }
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        pauseMenuPanel.SetActive(true);
        container.SetActive(true);  // ← MUESTRA el contenedor al pausar (Escape)

        Time.timeScale = 0f;
        GameIsPaused = true;

        // Switch to UI action map if PlayerInput assigned
        playerInput?.SwitchCurrentActionMap("UI");

        // Unlock & show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        container.SetActive(false);
        pauseMenuPanel.SetActive(true);

        Time.timeScale = 1f;
        GameIsPaused = false;

        // Switch back to Player map
        playerInput?.SwitchCurrentActionMap("Player");

        // Lock & hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Call from UI Buttons (OnClick)
    public void Continuar() => Resume();
    public void irAlMenu() => SceneManager.LoadScene("Menu");
    public void Salir() => Application.Quit();
}