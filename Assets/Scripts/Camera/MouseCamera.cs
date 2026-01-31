
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCamera : MonoBehaviour
{
    public Vector2 turn;
    public InputActionReference input;
    public float sensitivity = .5f;


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

        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }
}
