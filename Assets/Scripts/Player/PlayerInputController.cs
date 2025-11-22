using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour

{
    public Vector2 dir = Vector2.zero;
    private PlayerController player;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Debug.Log("Move started");
        }
        if (context.performed)
        {
            // Debug.Log("Move performed");
        }
        if (context.canceled)
        {
            // Debug.Log("Move canceled");
        }
        dir = context.ReadValue<Vector2>();
        // Debug.Log(dir);
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Debug.Log("Jump started");
        }
        if (context.performed)
        {
            if (player != null)
            {
                player.Jump();
            }
            // Debug.Log("Jump performed");
        }
        if (context.canceled)
        {
            // Debug.Log("Jump canceled");
        }
    }

    public void ShootInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            player.isShooting = true;
            // Debug.Log("Shoot started");
        }
        if (context.performed)
        {
            // Debug.Log("Shoot performed");
        }
        if (context.canceled)
        {
            player.isShooting = false;
            // Debug.Log("Shoot canceled");
        }
    }
}
