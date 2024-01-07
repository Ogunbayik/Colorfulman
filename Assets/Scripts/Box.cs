using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private enum States
    {
        Idle,
        Walk
    }

    private States currentState;

    private PlayerController player;
    private MeshRenderer meshRenderer;
    private Animator animator;

    [SerializeField] private float movementSpeed;
    [SerializeField] private Color[] colors;

    private int maxWalkTimer;
    private float walkTimer;
    private int colorIndex;
    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
    }
    void Start()
    {
        movementSpeed = Random.Range(1, 3);
        currentState = States.Idle;
        walkTimer = 0f;
        maxWalkTimer = 2;

        SetColor();
    }

    private void SetColor()
    {
        var randomIndex = Random.Range(0, colors.Length);
        colorIndex = randomIndex;

        meshRenderer.material.color = colors[colorIndex];
    }

    private void Update()
    {
        switch(currentState)
        {
            case States.Idle:
                IdleState();
                break;
            case States.Walk:
                WalkState();
                break;
        }
    }
    private void IdleState()
    {
        walkTimer += Time.deltaTime;

        if (walkTimer >= maxWalkTimer)
            currentState = States.Walk;
    }
    private void WalkState()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        animator.SetTrigger("isMove");
    }

    public int GetColorIndex()
    {
        return colorIndex;
    }
}
