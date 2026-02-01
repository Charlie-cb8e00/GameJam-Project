
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCamera : MonoBehaviour
{
    public Vector2 turn;
    public InputActionReference input;
    public float sensitivity = .5f;

    public float minPitch = -80f;              // no mires demasiado abajo
    public float maxPitch = 80f;               //no mires demasiado arriba
    private float pitch = 0f;


    void Start()
    {
        input.action.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }


    void Update()
    {
        Vector2 moveMouse = input.action.ReadValue<Vector2>();
        turn.x += moveMouse.x * sensitivity;
        turn.y += moveMouse.y * sensitivity;

        pitch -= moveMouse.y;                           // el signo (-) invierte para que sea natural
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.localRotation = Quaternion.Euler(pitch, turn.x, 0);
    }
}
