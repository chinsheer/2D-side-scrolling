using UnityEngine;

public class PlayerInput : MonoBehaviour, IMoveInputSource
{
    public Vector2 MoveDirection { get; private set; }
    public bool Jump { get; private set; }

    public BookUI bookUI; // Assign in inspector

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
    }
}
