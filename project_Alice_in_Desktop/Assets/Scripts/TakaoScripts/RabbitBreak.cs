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
        if (rabbitCore.isStop) return;   //�E�B���h�E�G���Ă���Ԃ̓��^�[��
        if (rabbitCore.isInside) return; //�C���T�C�h�̊Ԃ̓��^�[��
        if (rabbitCore.isHit) return;    //�v���C���[�ƐڐG�����Ȃ烊�^�[��
        Debug.Log("break");
        animator.SetTrigger("Capture");

        fadeEffect.StartOutsideEffect();
    }
}
