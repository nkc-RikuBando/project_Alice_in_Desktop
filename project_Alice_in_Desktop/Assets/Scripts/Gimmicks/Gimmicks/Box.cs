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
            //AnimePlay();
            
            if (isBreak == false)
            {
                //UpRayCast();
                HorizontalRay();
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

        void PlayerEnter()
        {
            stayFlg = true; // �؍ݒ�
            _IHitPlayer.IsHitPlayer();
        }

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
                    this.StartCoroutine(KeyAppTime());
                }
            }
            /*else */if (UpInput()) playerStatusManager.PlayerIsInput(true);
        }

        /// <summary>
        /// �v���C���[���G��Ă���A���A�L�[�𒷉���
        /// </summary>
        /// <returns></returns>
        bool IsStay()
        {
            return stayFlg == true/* && _IActionKey.ActionKey()*/;
        }

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

        void HorizontalRay()
        {
            // Ray�̈ʒu�̒����l
            Vector3 offset = new Vector3(-2f, 1, 0);
            Vector3 offset2 = new Vector3(0, 1.5f, 0); // ��

            //Ray�̍쐬�@�@�@�@�@�@�@��Ray���΂����_�@�@�@��Ray���΂�����
            Ray2D ray = new Ray2D(transform.position + offset, Vector3.right);
            Ray2D ray2 = new Ray2D(transform.position + offset2, Vector3.up); // ��

            //Ray�����������I�u�W�F�N�g�̏������锠
            //RaycastHit2D hit;

            //Ray�̔�΂��鋗��
            int distance = 4;
            int distance2 = 2; // ��

            //Ray�̉���   ��Ray�̌��_�@�@�@�@��Ray�̕����@�@�@��Ray�̐F
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
            Debug.DrawRay(ray2.origin, ray2.direction * distance2, Color.red); // ��

            //               ��Ray  ��Ray�����������I�u�W�F�N�g ������
            bool hit = Physics2D.Raycast(ray.origin, ray.direction, distance, layer);
            bool hit2 = Physics2D.Raycast(ray2.origin, ray.direction, distance2, layer);

            //����Ray�ɃI�u�W�F�N�g���Փ˂�����
            bool isPlayerHit = hit || hit2;
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
