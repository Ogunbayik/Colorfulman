using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string HORIZONTAL_INPUT = "Horizontal";
    private const string VERTICAL_INPUT = "Vertical";

    private PlayerAnimationController animationController;

    [SerializeField] private Transform body;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private float movementSpeed;
    private float horizontalInput;
    private float verticalInput;

    private Vector3 movementDirection;

    private bool isWalk;
    private void Awake()
    {
        animationController = GetComponentInChildren<PlayerAnimationController>();
    }
    void Start()
    {
        isWalk = false;
        movementSpeed = 0f;
    }

    void Update()
    {
        CheckMovement();

        HandleMovement();

    }

    private void CheckMovement()
    {
        var dir = movementDirection.sqrMagnitude;

        if (dir != 0)
            isWalk = true;
        else
            isWalk = false;
    }

    private void HandleMovement()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL_INPUT);
        verticalInput = Input.GetAxis(VERTICAL_INPUT);

        movementDirection = new Vector3(horizontalInput, 0f, verticalInput);
        var running = Input.GetKey(KeyCode.LeftShift);

        if (isWalk)
        {
            movementSpeed = walkSpeed;
            animationController.WalkAnimation(true);

            if (running)
            {
                animationController.RunAnimation(true);
                movementSpeed = runSpeed;
            }
            else
            {
                animationController.RunAnimation(false);
                movementSpeed = walkSpeed;
            }

            transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
            body.transform.position = transform.position; //Karakterin bugunu engellemek için
            body.transform.forward = Vector3.Lerp(body.transform.forward, movementDirection, 1f);
        }
        else
        {
            animationController.WalkAnimation(false);
        }
    }
}
