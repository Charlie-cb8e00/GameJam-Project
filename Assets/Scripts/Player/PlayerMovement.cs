using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.Rendering.FilterWindow;

public class PlayerMovement : MonoBehaviour
{
    public InputActionReference inputAction;
    public float moveSpeed = 5f;
    public Rigidbody player;
    public Transform cam;

    private Vector2 movementInput;

    void Start()
    {
        inputAction.action.Enable();
        player = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movementInput = inputAction.action.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        float horInput = movementInput.x;
        float verInput = movementInput.y;

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * verInput + camRight * horInput;

        player.MovePosition(player.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mascara"))
        {
            SceneManager.LoadScene("Game_Win");
        }
    }
}

