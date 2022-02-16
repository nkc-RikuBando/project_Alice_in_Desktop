using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Window;

public class RabbitReStart : MonoBehaviour, IWindowLeave
{
    RabbitCore rabbitCore;
    RabbitMove rabbitMove;
    private Animator animator;
    private Animator childAnimator;
    private Rigidbody2D rigd2D;

    [SerializeField] GameObject childSpring;

    // Start is called before the first frame update
    void Start()
    {
        rabbitCore = GetComponent<RabbitCore>();
        rabbitMove = GetComponent<RabbitMove>();
        animator = GetComponent<Animator>();
        childAnimator = childSpring.GetComponent<Animator>();
        rigd2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WindowLeaveAction()
    {
        rabbitCore.isPlay = true;
        rabbitCore.isStop = false;
        PlayMove();
    }

    public void PlayMove()
    {
        rigd2D.isKinematic = false;
        animator.enabled = true;
        childAnimator.enabled = true;
        rabbitCore.isPlay = false;
        if (!rabbitCore.isInside) return;
        rabbitCore.isTeleportation = true;
        rabbitMove.Act_RabbitTeleportation();

    }
}
