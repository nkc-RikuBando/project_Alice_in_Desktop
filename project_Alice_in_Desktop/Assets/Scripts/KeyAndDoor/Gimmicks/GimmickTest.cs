using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gimmicks
{
    public class GimmickTest : MonoBehaviour, IHitSwitch
    {
        private bool switchFlg = false;

        public void Switch(bool switchOn)
        {
            switchFlg = switchOn;
            if (switchFlg == true)
            {
                gameObject.SetActive(false);
                Debug.Log("�X�C�b�`ON");
            }
            else
            {
                gameObject.SetActive(true);
                Debug.Log("�X�C�b�`OFF");
            }
        }
    }
}
