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
        private ITestKey _ITestKey;                 // ���̓C���^�[�t�F�[�X��ۑ�
        private IHitPlayer _IHitPlayer;             // �����蔻��C���^�[�t�F�[�X��ۑ�
        [SerializeField] private GameObject hideKey; // �����擾
        private bool stayFlg = false;
        private Animator animator;
        [SerializeField] private GameObject uiGauge;

        public bool PlHitFlg
        {
            get { return stayFlg; }
            set { stayFlg = value; }
        }

        void Start()
        {
            _ITestKey = GetComponent<ITestKey>(); // ���̓C���^�[�t�F�[�X���擾
            _IHitPlayer = GetComponent<IHitPlayer>(); // �����蔻��C���^�[�t�F�[�X���擾
            hideKey.SetActive(false); // �����\��
            animator = hideKey.GetComponent<Animator>();
            uiGauge.SetActive(false);
        }

        void Update()
        {
            if (StayInput())
            {
                if (WaitTimeUI.gaugeMaxFlg == true)
                {
                    WaitTimeUI.gaugeMaxFlg = false;
                    KeyApp();
                    hideKey.transform.parent = null; // �����q�I�u�W�F�N�g����O��
                    Destroy(gameObject);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // �v���C���[�������ė�����
            if (collision.gameObject == player)
            {
                stayFlg = true; // �؍ݒ�
                uiGauge.SetActive(true);
                _IHitPlayer.IsHitPlayer(stayFlg);
            }   
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // �v���C���[���o�čs������
            if (collision.gameObject == player)
            {
                stayFlg = false; // �؍݂��ĂȂ�
                uiGauge.SetActive(false);
                _IHitPlayer.IsHitPlayer(stayFlg);
            }   
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
            animator.SetTrigger("Spawn");
        }
    }
}
