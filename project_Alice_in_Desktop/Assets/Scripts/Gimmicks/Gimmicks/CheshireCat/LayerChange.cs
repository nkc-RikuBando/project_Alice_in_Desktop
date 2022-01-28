using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gimmicks
{
    public class LayerChange : MonoBehaviour, IRenderingFlgSettable
    {
        [Header("�J�n���ɉ�ʊO�ɂ���")]
        [SerializeField] private bool outFlg;
        private const int insideLayerNum = 11;
        private const int outsideLayerNum = 12;

        public bool OutFlg
        {
            get { return outFlg; }
            set { outFlg = value; }
        }

        void Start()
        {
            if (outFlg == true) gameObject.layer = outsideLayerNum;
            else                gameObject.layer = insideLayerNum;
        }

        public void SetRenderingFlg(bool val)
        {
            // true�͒�
            if (val)
            {
                gameObject.layer = insideLayerNum;
                outFlg = false; // ���ɂ���
            }
            else
            {
                gameObject.layer = outsideLayerNum;
                outFlg = true; // �O�ɂ���
            }
        }
    }
}
