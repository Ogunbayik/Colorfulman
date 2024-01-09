using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    private PunchCollider punchCollider;

    [Header(" Settings ")]
    [SerializeField] private Transform body;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float rotateSpeed;

    private float movementSpeed;
    private float horizontalInput;
    private float verticalInput;
    private float timerPunch;

    private Vector3 movementDirection;

    private bool isWalk;
    private void Awake()
    {
        animationController = GetComponentInChildren<PlayerAnimationController>();
        punchCollider = GetComponentInChildren<PunchCollider>();
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
                HandleMove();
                break;
            case States.Attack:
                HandleAttack();
                break;
        }
    }

    private void HandleMove()
    {
        CheckMovement();
        Movement();

        if (Input.GetKey(KeyCode.Q) && !isWalk)
        {
            SwitchState(States.Attack);
        }
    }

    private void HandleAttack()
    {
        punchCollider.ActivateCollider(true);
        animationController.PunchAnimation(true);
        timerPunch += Time.deltaTime;
        var punchAnimationTime = 2f;

        if (timerPunch >= punchAnimationTime)
        {
            timerPunch = 0f;
            SwitchState(States.Move);
            animationController.PunchAnimation(false);
            punchCollider.ActivateCollider(false);
        }
    }

    private void CheckMovement()
    {
        if (movementDirection != Vector3.zero)
        {
            isWalk = true;
            HandleRotate();
        }
        else
            isWalk = false;
    }

    private void Movement()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL_INPUT);
        verticalInput = Input.GetAxis(VERTICAL_INPUT);

        movementDirection = new Vector3(horizontalInput, 0f, verticalInput);
        movementDirection.Normalize();
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
            
        }
        else
        {
            animationController.WalkAnimation(false);
        }

        
    }

    private void HandleRotate()
    {
        var toRotate = Quaternion.LookRotation(movementDirection, Vector3.up);

        body.transform.rotation = Quaternion.RotateTowards(body.transform.rotation, toRotate, rotateSpeed * Time.deltaTime);
    }

    public void SwitchState(States state)
    {
        currentState = state;
    }

    


}
