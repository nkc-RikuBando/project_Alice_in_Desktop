using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Connector.Player;
using GameSystem;

namespace Gimmicks
{
    public class Box : MonoBehaviour
    {
        private GameObject player; // �v���C���[��ۑ�
        private IPlayerAction _IActionKey;                 // ���̓C���^�[�t�F�[�X��ۑ�
        private IHitPlayer _IHitPlayer;             // �����蔻��C���^�[�t�F�[�X��ۑ�
        private GameObject hideKey; // �����擾
        private bool stayFlg = false;
        private Animator myAnimator;
        private Animator keyAnimator;

        [SerializeField] private GameObject uiGauge;
        [SerializeField] private float time;

        public bool PlHitFlg
        {
            get { return stayFlg; }
            set { stayFlg = value; }
        }

        void Start()
        {
            player = GetGameObject.playerObject;
            _IActionKey = player.GetComponent<IPlayerAction>(); // ���̓C���^�[�t�F�[�X���擾
            _IHitPlayer = uiGauge.GetComponentInChildren<IHitPlayer>(); // �����蔻��C���^�[�t�F�[�X���擾
            hideKey = GetGameObject.KeyObj;
            hideKey.SetActive(false); // �����\��
            myAnimator = GetComponent<Animator>();
            keyAnimator = hideKey.GetComponent<Animator>();
            uiGauge.SetActive(false);
        }

        void Update()
        {
            if (StayInput())
            {
                if (WaitTimeUI.gaugeMaxFlg == true)
                {
                    WaitTimeUI.gaugeMaxFlg = false;
                    myAnimator.SetTrigger("Destroy");
                    hideKey.transform.parent = null; // �����q�I�u�W�F�N�g����O��
                    Destroy(uiGauge);
                    this.StartCoroutine(KeyAppTime());
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // �v���C���[�������ė�����
            if (collision.gameObject == player)
            {
                stayFlg = true; // �؍ݒ�
                //uiGauge.SetActive(true);
                _IHitPlayer.IsHitPlayer();
            }   
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // �v���C���[���o�čs������
            if (collision.gameObject == player)
            {
                stayFlg = false; // �؍݂��ĂȂ�
                //uiGauge.SetActive(false);
                _IHitPlayer.NonHitPlayer();
            }   
        }

        /// <summary>
        /// �v���C���[���G��Ă���A���A�L�[�𒷉���
        /// </summary>
        /// <returns></returns>
        bool StayInput()
        {
            return stayFlg == true && _IActionKey.ActionKey();
        }

        /// <summary>
        /// �����o������
        /// </summary>
        public void KeyApp()
        {
            hideKey.SetActive(true); //��ꂽ�献���o��
            hideKey.transform.parent = null; // �����q�I�u�W�F�N�g����O��
            keyAnimator.SetTrigger("Spawn");
        }

        IEnumerator KeyAppTime()
        {
            yield return new WaitForSeconds(time);
            hideKey.SetActive(true); //��ꂽ�献���o��
            keyAnimator.SetTrigger("Spawn");
        }
    }
}
