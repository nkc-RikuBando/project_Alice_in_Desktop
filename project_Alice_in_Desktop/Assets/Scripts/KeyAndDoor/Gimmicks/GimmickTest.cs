using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickTest : MonoBehaviour, IHitSwitch
{
    private bool switchFlg = false;

    void Update()
    {
        //Debug.Log("�M�~�b�N " + switchFlg);
    }

    public void Switch(bool switchOn)
    {
        switchFlg = switchOn;
        if (switchFlg == true)
        {
            transform.position = new Vector3(4, -3, 0);
            Debug.Log("�X�C�b�`ON");
        }
        else
        {
            transform.position = new Vector3(10, 0, 0);
            Debug.Log("�X�C�b�`OFF");
        }
    }
}
