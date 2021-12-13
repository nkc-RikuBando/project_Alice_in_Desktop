using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayerChange : MonoBehaviour,IRenderingFlgSettable
{
    // Player‚ÌLayer‚ğ•ÏX‚·‚éˆ—ˆ—

    [SerializeField] LayerMask _insideLayer;
    [SerializeField] LayerMask _outsideLayer;


    void IRenderingFlgSettable.SetRenderingFlg(bool val)
    {
        if (val) gameObject.layer = _insideLayer;
        else     gameObject.layer = _outsideLayer;
    }
}
