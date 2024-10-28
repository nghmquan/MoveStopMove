using UnityEngine;

public class Player : Character
{
    [Header("Player properties")]
    [SerializeField] private FixedJoystick fixedJoystick;

    private float moveHorizontal, moveVertical;

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        Move(HandleInput());
    }

    //Player movement
    private Vector3 HandleInput()
    {
        moveHorizontal = fixedJoystick.Horizontal;
        moveVertical = fixedJoystick.Vertical;
        Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
        return moveDirection;
    }
}
