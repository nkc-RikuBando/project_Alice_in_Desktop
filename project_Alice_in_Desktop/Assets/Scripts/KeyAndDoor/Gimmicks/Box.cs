using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Connector.Inputer;

namespace Gimmicks
{
    public class Box : MonoBehaviour
    {
        private GameObject player; // �v���C���[��ۑ�
        private ITestKey _ITestKey;
        [SerializeField] private GameObject hideKey; // �����擾
        private bool stayFlg;
        private float inputTime = 0;
        //private bool hitFlg = false;

        public bool PlHitFlg
        {
            get { return stayFlg; }
            set { stayFlg = value; }
        }

        void Start()
        {
            player = GameObject.Find("PlayerTest"); // �v���C���[�̎擾
            _ITestKey = GetComponent<ITestKey>();
            hideKey.SetActive(false);
            stayFlg = false;
        }

        void Update()
        {
            if(StayInput())
            {
                inputTime += 1f * Time.deltaTime;
                if(inputTime >= 3)
                {
                    KeyApp();
                    hideKey.transform.parent = null;
                    Destroy(gameObject);
                }
            }
            else
            {
                inputTime = default; // �L�[���͎��Ԃ��O�ɂ���
            }
        }
    
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // �v���C���[�������ė�����
            if (collision.gameObject == player)
            {
                stayFlg = true; // �؍ݒ�
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            // �v���C���[���o�čs������
            if (collision.gameObject == player)
            {
                stayFlg = false; // �؍݂��ĂȂ�
            }
        }

        /// <summary>
        /// �v���C���[���G��Ă���A���AQ�L�[�𒷉���
        /// </summary>
        /// <returns></returns>
        bool StayInput()
        {
            return stayFlg == true && _ITestKey.EventNagaoshiKey();
        }

        /// <summary>
        /// �����o������
        /// </summary>
        public void KeyApp() // ��(����������)
        {
            hideKey.SetActive(true); //��ꂽ�献���o��
        }
    }
}
