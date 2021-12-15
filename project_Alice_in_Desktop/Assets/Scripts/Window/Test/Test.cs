using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour,IRenderingFlgSettable
{
    private bool renderingFlg = false;


    private void FixedUpdate()
    {
        Debug.Log(renderingFlg);

    }

    void IRenderingFlgSettable.SetRenderingFlg(bool val)
    {
        Debug.Log(val);
        renderingFlg = val;
    }

}
