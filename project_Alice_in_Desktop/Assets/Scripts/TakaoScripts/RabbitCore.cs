using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitCore : MonoBehaviour, IRenderingFlgSettable
{
    public bool isMove { get; set; } = false; //�ړ�����Ƃ�
    public bool isTeleportation { get; set; } = false; //�u�Ԉړ�����Ƃ�
    public bool isHit { get; set; } = false; //�v���C���[�ɓ��������Ƃ�
    public bool isStop { get; set; } = false; //�E�B���h�E��G���Ē�~����Ƃ�
    public bool isPlay { get; set; } = false; //�E�B���h�E�𗣂��Ē�~�I������Ƃ�
    public bool isInside { get; set; } = true;

    public void SetRenderingFlg(bool val)
    {
        Debug.Log("val = " + val);
        isInside = val;
    }
}
