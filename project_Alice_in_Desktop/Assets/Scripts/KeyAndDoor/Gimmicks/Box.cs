using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Connector.Inputer;
using GameSystem;

namespace Gimmicks
{
    public class Box : MonoBehaviour
    {
        [SerializeField] private GameObject player; // �v���C���[��ۑ�
        private ITestKey _ITestKey; // ���̓C���^�[�t�F�[�X���擾
        [SerializeField] private GameObject hideKey; // �����擾
        private bool stayFlg = false;
        //private float inputTime = default;

        public bool PlHitFlg
        {
            get { return stayFlg; }
            set { stayFlg = value; }
        }

        void Start()
        {
            _ITestKey = GetComponent<ITestKey>(); // ���̓C���^�[�t�F�[�X���擾
            hideKey.SetActive(false); // �����\��
        }

        void Update()
        {
            if (StayInput())
            {
                //inputTime += 1f * Time.deltaTime;
                if (/*inputTime >= 0.8f*/WaitTimeUI.gaugeMaxFlg == true)
                {
                    WaitTimeUI.gaugeMaxFlg = false;
                    KeyApp();
                    hideKey.transform.parent = null; // �����q�I�u�W�F�N�g����O��
                    Destroy(gameObject);
                }
            }
            //else
            //    inputTime = default; // �L�[���͎��Ԃ��O�ɂ���
        }
    
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // �v���C���[�������ė�����
            if (collision.gameObject == player)
                stayFlg = true; // �؍ݒ�
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            // �v���C���[���o�čs������
            if (collision.gameObject == player)
                stayFlg = false; // �؍݂��ĂȂ�
        }

        /// <summary>
        /// �v���C���[���G��Ă���A���A�L�[�𒷉���
        /// </summary>
        /// <returns></returns>
        bool StayInput()
        {
            return stayFlg == true && _ITestKey.EventNagaoshiKey();
        }

        /// <summary>
        /// �����o������
        /// </summary>
        public void KeyApp()
        {
            hideKey.SetActive(true); //��ꂽ�献���o��
        }
    }
}
