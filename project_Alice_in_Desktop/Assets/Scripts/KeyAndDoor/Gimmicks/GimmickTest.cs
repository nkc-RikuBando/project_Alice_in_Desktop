using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickTest : MonoBehaviour, IHitSwitch
{
    private bool switchFlg = false;

    void Update()
    {
        //Debug.Log("ギミック " + switchFlg);
    }

    public void Switch(bool switchOn)
    {
        switchFlg = switchOn;
        if (switchFlg == true)
        {
            transform.position = new Vector3(4, -3, 0);
            Debug.Log("スイッチON");
        }
        else
        {
            transform.position = new Vector3(10, 0, 0);
            Debug.Log("スイッチOFF");
        }
    }
}
