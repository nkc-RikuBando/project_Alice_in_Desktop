using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Connector.Player;
using GameSystem;
using Player;

namespace Gimmicks
{
    public class Box : MonoBehaviour
    {
        private GameObject player;                   // �v���C���[��ۑ�
        private IPlayerAction _IActionKey;           // ���̓C���^�[�t�F�[�X��ۑ�
        private PlayerStatusManager playerStatusManager;
        private IHitPlayer _IHitPlayer;              // �����蔻��C���^�[�t�F�[�X��ۑ�
        [SerializeField] private GameObject hideKey; // �����擾
        private bool stayFlg = false;
        private Animator myAnimator;                 // ��(���g)�̃A�j���[�V������ۑ�
        private Animator keyAnimator;                // ���̃A�j���[�V������ۑ�

        [SerializeField] private GameObject uiGauge; // �Q�[�W��ۑ�
        [SerializeField] private float time;

        [SerializeField] private LayerMask layer;

        private bool isBreak;

        public enum AnimeType
        {
            BOX_BREAK, KEY_APP
        }
        AnimeType animeType;

        //public bool PlHitFlg
        //{
        //    get { return stayFlg; }
        //    set { stayFlg = value; }
        //}

        void Start()
        {
            player = GetGameObject.playerObject; // �v���C���[���擾
            // ���̓C���^�[�t�F�[�X���擾
            _IActionKey = player.GetComponent<IPlayerAction>();

            // �����蔻��C���^�[�t�F�[�X���擾
            _IHitPlayer = uiGauge.GetComponentInChildren<IHitPlayer>();
            playerStatusManager = player.GetComponent<PlayerStatusManager>();
            //hideKey = GetGameObject.KeyObj;

            hideKey.SetActive(false);              // �����\��
            myAnimator = GetComponent<Animator>(); // ��(���g)�̃A�j���[�V�������擾
            keyAnimator = hideKey.GetComponent<Animator>(); // ���̃A�j���[�V�������擾
            
            uiGauge.SetActive(false);              // �Q�[�W���\��
            isBreak = false;
        }

        void Update()
        {
            if (isBreak == false)
            {
                RayCastHit();
            }
            BoxBreak();
        }

        void AnimePlay()
        {
            switch (animeType)
            {
                case AnimeType.BOX_BREAK:
                    myAnimator.SetTrigger("Destroy");
                    break;
                case AnimeType.KEY_APP:
                    keyAnimator.SetTrigger("Spawn");
                    break;
            }
        }

        /// <summary>
        /// �v���C���[�������ė���
        /// </summary>
        void PlayerEnter()
        {
            stayFlg = true; // �؍ݒ�
            _IHitPlayer.IsHitPlayer();
        }

        /// <summary>
        /// �v���C���[���o�čs����
        /// </summary>
        void PlayerExit()
        {
            stayFlg = false; // �؍݂��ĂȂ�
            _IHitPlayer.NonHitPlayer();
        }

        /// <summary>
        /// ��(���g)������
        /// </summary>
        void BoxBreak()
        {
            //if (IsStay())
            {
                //playerStatusManager.PlayerIsInput(false);
                // �Q�[�W�����܂�����
                if (WaitTimeUI.gaugeMaxFlg == true)
                {
                    isBreak = true;
                    WaitTimeUI.gaugeMaxFlg = false;
                    myAnimator.SetTrigger("Destroy");  // �A�j���[�V�����Đ�
                    hideKey.transform.parent = null;   // �����q�I�u�W�F�N�g����O��
                    uiGauge.SetActive(false);          // �Q�[�W����U�B��
                    //AudioManager.Instance.SeAction("���j��_1");
                    this.StartCoroutine(KeyAppTime());
                }
            }
            /*else */if (UpInput()) playerStatusManager.PlayerIsInput(true);
        }

        /// <summary>
        /// �v���C���[���G��Ă���A���A�L�[�𒷉���
        /// </summary>
        /// <returns></returns>
        //bool IsStay()
        //{
        //    return stayFlg == true/* && _IActionKey.ActionKey()*/;
        //}

        /// <summary>
        /// �v���C���[���G��Ă���A���A�L�[�𗣂�
        /// </summary>
        /// <returns></returns>
        bool UpInput()
        {
            return stayFlg == true && _IActionKey.ActionKeyUp();
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

        void RayCastHit()
        {
            // Ray�̈ʒu�̒����l
            Vector3 offset = new Vector3(-2f, 1, 0);
            Vector3 offset2 = new Vector3(0, 1.5f, 0);

            //  Ray�̍쐬�@�@�@�@�@�@�@Ray���΂����_�@�@�@Ray���΂�����
            Ray2D horiRay = new Ray2D(transform.position + offset, Vector3.right);
            Ray2D upRay = new Ray2D(transform.position + offset2, Vector3.up);

            // Ray�����������I�u�W�F�N�g�̏������锠
            //RaycastHit2D hit;

            // Ray�̔�΂��鋗��
            int horiRayDis = 4;
            int upRayDis = 2;

            // Ray�̉���    Ray�̌��_�@�@�@�@      Ray�̕����@�@�@       Ray�̐F
            Debug.DrawRay(horiRay.origin, horiRay.direction * horiRayDis, Color.red);
            Debug.DrawRay(upRay.origin,   upRay.direction * upRayDis,     Color.red);

            //                                   ��Ray  ��Ray�����������I�u�W�F�N�g ������
            bool horiRayHit = Physics2D.Raycast(horiRay.origin, horiRay.direction, horiRayDis, layer);
            bool upRayHit   = Physics2D.Raycast(upRay.origin,   upRay.direction,   upRayDis,   layer);

            //����Ray�ɃI�u�W�F�N�g���G�ꂽ��
            bool isPlayerHit = horiRayHit || upRayHit;
            if (isPlayerHit) PlayerEnter();
            else PlayerExit();
        }

        /// <summary>
        /// �����o������
        /// </summary>
        /// <returns></returns>
        IEnumerator KeyAppTime()
        {
            yield return new WaitForSeconds(time);
            hideKey.SetActive(true); //��ꂽ�献���o��
            keyAnimator.SetTrigger("Spawn");
        }
    }
}
