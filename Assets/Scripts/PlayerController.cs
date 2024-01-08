using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum States
    {
        Move,
        Attack
    }

    public States currentState;

    private const string HORIZONTAL_INPUT = "Horizontal";
    private const string VERTICAL_INPUT = "Vertical";

    private PlayerAnimationController animationController;

    [SerializeField] private Transform directionHelper;
    [SerializeField] private Transform body;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private float movementSpeed;
    private float horizontalInput;
    private float verticalInput;
    private float timerPunch;

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
        timerPunch = 0f;
    }

    void Update()
    {
        switch (currentState)
        {
            case States.Move:
                CheckMovement();
                HandleMovement();

                if (Input.GetKey(KeyCode.Q) && !isWalk)
                    SwitchState(States.Attack);
                break;
            case States.Attack:
                Debug.Log("Punch");
                animationController.PunchAnimation(true);
                timerPunch += Time.deltaTime;
                var punchAnimationTime = 2f;

                if (timerPunch >= punchAnimationTime)
                {
                    timerPunch = 0f;
                    SwitchState(States.Move);
                    animationController.PunchAnimation(false);
                }
                break;
        }


    }

    private void CheckMovement()
    {
        var dir = movementDirection.sqrMagnitude;

        if (dir != 0)
        {
            isWalk = true;
        }
        else
        {
            isWalk = false;
        }
    }

    private void HandleMovement()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL_INPUT);
        verticalInput = Input.GetAxis(VERTICAL_INPUT);

        movementDirection = new Vector3(horizontalInput, 0f, verticalInput);
        var running = Input.GetKey(KeyCode.LeftShift);
        var lastDirection = (directionHelper.position - transform.position).normalized;
        Debug.Log(lastDirection);
        

        if (isWalk)
        {
            //HandleRotate();
            body.transform.forward = Vector3.Lerp(body.transform.forward, movementDirection, 1f);
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

            
        }
        else
        {
            animationController.WalkAnimation(false);
        }
    }

    private void HandleRotate()
    {
        var lookDirection = directionHelper.position - transform.position;
        body.transform.forward = Vector3.Lerp(body.transform.forward, movementDirection, 1f);
    }

    public void SwitchState(States state)
    {
        currentState = state;
    }

    


}
