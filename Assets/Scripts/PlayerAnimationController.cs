using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private PlayerController playerController;

    private Animator animator;

    private int isWalkHash;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        isWalkHash = Animator.StringToHash("isWalk");
    }

    public void WalkAnimation(bool isWalk)
    {
        animator.SetBool(isWalkHash, isWalk);
    }

    public void RunAnimation(bool isRun)
    {
        animator.SetBool("isRun", isRun);
    }

}
