using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gimmicks;
using Connector.Inputer;

namespace GameSystem
{
    public class WaitTimeUIGene : MonoBehaviour
    {
        [SerializeField] private GameObject waitTimeUI; // ��������UI
        private GameObject obj;
        private GameObject canvas; // �L�����o�X�̕ۑ�
        private Box boxScr;        // Box�̃X�N���v�g�̕ۑ�
        private ITestKey _ITestKey; // ���̓C���^�[�t�F�[�X�̕ۑ�
        bool test;
        bool test2;

        void Start()
        {
            canvas = GameObject.Find("Canvas");   // �L�����o�X�̎擾
            boxScr = GetComponent<Box>();         // Box�X�N���v�g�̎擾
            _ITestKey = GetComponent<ITestKey>(); // ���̓C���^�[�t�F�[�X�̎擾
        }

        void Update()
        {
            test = boxScr.PlHitFlg && _ITestKey.EventNagaoshiKey() ? true : false;
            if (test == true && !test2)
            {
                obj = Instantiate(waitTimeUI, canvas.transform);
                Vector3 UIpos = transform.position + new Vector3(0, 3);
                obj.transform.position = UIpos;
                test2 = true;
            }
            else if (!test)
            {
                if (obj == null) return;
                Destroy(obj.gameObject);
                test2 = false;
            }
        }
    }
}
