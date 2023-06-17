using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    [SerializeField] GameObject enemy;
    Animator animator;
    [SerializeField] Animation stateAnimation;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator = enemy.GetComponent<Animator>();
        animator.Play(stateAnimation.name);
    }
}
