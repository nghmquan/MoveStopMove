using System.Collections;
using UnityEngine;

public class Bot : Character
{
    [Header("Bot properites")]
    [SerializeField] private Vector3 moveHorizontal;
    [SerializeField] private Vector3 moveVertical;
    [SerializeField] private Transform hitCollided;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float raycastDistance = 1f;
    [SerializeField] private float moveTimer = 1f;
    [SerializeField] private float stopTimer = 1f;

    private Vector3 moveDirection;
    private bool isMoving = true;
    void Start()
    {
        OnInit();
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }
        Move(Vector3.zero);
    }

    protected override void OnInit()
    {
        base.OnInit();
        SetRandomDirection();
    }

    //Bot movement
    protected override void Move(Vector3 _postion)
    {
        Debug.DrawRay(hitCollided.position, transform.forward, Color.red, raycastDistance);
        if(Physics.Raycast(hitCollided.position, transform.forward, out RaycastHit hit, raycastDistance))
        {
            isMoving = false;
            stopTimer = 1f;
            SetRandomDirection();
            return;
        }

        if (isMoving)
        {
            base.Move(moveDirection);
            moveTimer -= Time.deltaTime;

            if(moveTimer <= 0f)
            {
                isMoving = false;
                stopTimer = 1f;
                base.Move(Vector3.zero);
            }
        }
        else
        {
            stopTimer -= Time.deltaTime;

            if(stopTimer <= 0f)
            {
                isMoving = true;
                moveTimer = 1f;
                SetRandomDirection();
            }
        }
    }

    private void SetRandomDirection()
    {
        moveDirection = new Vector3(
            Random.Range(-moveHorizontal.x, moveHorizontal.x),
            0,
            Random.Range(-moveVertical.y, moveVertical.y));
    }
}
