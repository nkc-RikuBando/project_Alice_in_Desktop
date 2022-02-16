using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitCore : MonoBehaviour, IRenderingFlgSettable
{
    public bool isMove { get; set; } = false; //移動するとき
    public bool isTeleportation { get; set; } = false; //瞬間移動するとき
    public bool isHit { get; set; } = false; //プレイヤーに当たったとき
    public bool isStop { get; set; } = false; //ウィンドウを触って停止するとき
    public bool isPlay { get; set; } = false; //ウィンドウを離して停止終了するとき
    public bool isInside { get; set; } = true;

    public void SetRenderingFlg(bool val)
    {
        Debug.Log("val = " + val);
        isInside = val;
    }
}
