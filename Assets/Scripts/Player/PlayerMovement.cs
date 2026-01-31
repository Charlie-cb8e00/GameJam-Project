
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionReference inputAction;
    public int moveSpeed = 150;
    public Rigidbody player;


    void Start()
    {
        inputAction.action.Enable();

    }


    void Update()
    {

        Vector2 movement = inputAction.action.ReadValue<Vector2>() * moveSpeed;
        player.AddForce(movement.x, 0, movement.y);

    }

}

