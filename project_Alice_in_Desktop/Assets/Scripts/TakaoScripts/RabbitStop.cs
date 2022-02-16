using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Window;

public class RabbitStop : MonoBehaviour, IWindowTouch
{
    RabbitCore rabbitCore;
    private Animator animator;
    private Animator childAnimator;
    private Rigidbody2D rigd2D;

    [SerializeField] GameObject childSpring;

    // Start is called before the first frame update
    void Start()
    {
        rabbitCore = GetComponent<RabbitCore>();
        animator = GetComponent<Animator>();
        childAnimator = childSpring.GetComponent<Animator>();
        rigd2D = GetComponent<Rigidbody2D>();
    }

    public void WindowTouchAction()
    {
        //Ç±Ç±Ç…StopÇÃèàóù
        rabbitCore.isStop = true;
        StopMove();
    }

    private void StopMove()
    {
        if (rabbitCore.isStop)
        {
            animator.enabled = false;
            childAnimator.enabled = false;
            rigd2D.velocity = Vector2.zero;
            rigd2D.isKinematic = true;
        }
    }
}
