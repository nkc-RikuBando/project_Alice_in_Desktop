using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayerChange : MonoBehaviour,IRenderingFlgSettable
{
    // PlayerのLayerを変更する処理処理

    [SerializeField] LayerMask _insideLayer;
    [SerializeField] LayerMask _outsideLayer;


    void IRenderingFlgSettable.SetRenderingFlg(bool val)
    {
        if (val) gameObject.layer = _insideLayer;
        else     gameObject.layer = _outsideLayer;
    }
}
