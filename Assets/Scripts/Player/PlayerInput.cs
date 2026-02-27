using UnityEngine;

public class PlayerInput : MonoBehaviour, IMoveInputSource
{
    public Vector2 MoveDirection { get; private set; }
    public bool Jump { get; private set; }

    public BookUI bookUI; // Assign in inspector
    public PlayerCombat combat; // Assign in inspector

    void Update()
    {
        Vector2 moveDirection = Vector2.zero;
        if(Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector2.left;
        }
        if(Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector2.right;
        }
        if(Input.GetKey(KeyCode.Space))
        {
            Jump = true;
        }
        else
        {
            Jump = false;
        }
        MoveDirection = moveDirection.normalized;

        if (Input.GetKeyDown(KeyCode.E))
        {
            bookUI.ToggleVisibility();
        }

        if (Input.GetMouseButtonDown(1)) // Right click
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            combat.StartAiming(mousePosition);
        }
        else if (Input.GetMouseButtonUp(1)) // Release right click
        {
            combat.StopAiming();
        }

        combat.SetHandPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)); // Update hand position for aiming indicator
    }
}
