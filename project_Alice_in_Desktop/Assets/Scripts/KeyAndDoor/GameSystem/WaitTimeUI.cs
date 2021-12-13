using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Connector.Inputer;

namespace GameSystem
{
    public class WaitTimeUI : MonoBehaviour
    {
        private ITestKey _ITestKey; // ���̓C���^�[�t�F�[�X�̕ۑ�
        private Image waitTime; // UI�̕ۑ�
        public static bool gaugeMaxFlg;
        private GameObject _parent; // �e�I�u�W�F�N�g�̕ۑ�
        private const float UP_DOWN_NUM = 0.01f; // �Q�[�W�̑�����

        //public bool GaugeMaxFlg
        //{
        //    get { return gaugeMaxFlg; }
        //    set { gaugeMaxFlg = value; }
        //}

        void Start()
        {
            _ITestKey = GetComponent<ITestKey>();  // ���̓C���^�[�t�F�[�X�̎擾
            waitTime = GetComponent<Image>();      // UI�̎擾
            gaugeMaxFlg = false;
            waitTime.fillAmount = default;         // �Q�[�W�̏����l�O
            _parent = transform.parent.gameObject; //���̐e�I�u�W�F�N�g���擾
        }

        void Update()
        {
            if (_ITestKey.EventNagaoshiKey()) // �L�[�𒷉���
                waitTime.fillAmount += UP_DOWN_NUM; // �Q�[�W��������
            else // �L�[�𗣂�
                waitTime.fillAmount -= UP_DOWN_NUM; // �Q�[�W������

            if (waitTime.fillAmount >= 1) // �Q�[�W�����܂�����
            {
                gaugeMaxFlg = true;
                Destroy(_parent);        // ���̐e�I�u�W�F�N�g�̍폜
            }
        }
    }
}
