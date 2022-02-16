using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitBreak : MonoBehaviour
{
    RabbitCore rabbitCore;
    Animator animator;
    [SerializeField] private GameObject fadeObj;
    private FadeEffect fadeEffect;
    // Start is called before the first frame update
    void Start()
    {
        rabbitCore = GetComponent<RabbitCore>();
        animator = GetComponent<Animator>();
        fadeEffect = fadeObj.GetComponent<FadeEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rabbitCore.isStop) return;   //ウィンドウ触っている間はリターン
        if (rabbitCore.isInside) return; //インサイドの間はリターン
        if (rabbitCore.isHit) return;    //プレイヤーと接触したならリターン
        Debug.Log("break");
        animator.SetTrigger("Capture");

        fadeEffect.StartOutsideEffect();
    }
}
