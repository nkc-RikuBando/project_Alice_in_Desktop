using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayerChange : MonoBehaviour,IRenderingFlgSettable
{
    // Player��Layer��ύX���鏈������

    [SerializeField] int _insideLayerNum;
    [SerializeField] int _outsideLayerNum;


    void IRenderingFlgSettable.SetRenderingFlg(bool val)
    {
        //Debug.Log(val);
        if (val) gameObject.layer = _insideLayerNum;
        else     gameObject.layer = _outsideLayerNum;
    }
}
