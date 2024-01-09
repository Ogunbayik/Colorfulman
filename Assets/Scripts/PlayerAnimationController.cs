using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private int isWalkHash;
    private int isRunHash;
    private int isPunchHash;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        isWalkHash = Animator.StringToHash("isWalk");
        isRunHash = Animator.StringToHash("isRun");
        isPunchHash = Animator.StringToHash("isPunch");
    }

    public void WalkAnimation(bool isWalk)
    {
        animator.SetBool(isWalkHash, isWalk);
    }

    public void RunAnimation(bool isRun)
    {
        animator.SetBool(isRunHash, isRun);
    }

    public void PunchAnimation(bool isPunch)
    {
        animator.SetBool(isPunchHash, isPunch);
    }

}
