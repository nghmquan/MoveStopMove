using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Character
{
    [Header("Player properties")]
    [SerializeField] private FixedJoystick fixedJoystick;

    private Vector3 moveDirection;
    private float moveHorizontal, moveVertical;

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        Move(HandleInput());
        Attack();
    }

    //Player movement
    private Vector3 HandleInput()
    {
        moveHorizontal = fixedJoystick.Horizontal;
        moveVertical = fixedJoystick.Vertical;
        return moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
    }

    protected override void Move(Vector3 _position)
    {
        base.Move(_position);
    }
}
