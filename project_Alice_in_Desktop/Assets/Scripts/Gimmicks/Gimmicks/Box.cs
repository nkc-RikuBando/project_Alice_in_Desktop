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
        private BoxCollider2D boxCol;
        [SerializeField] private GameObject hideKey; // �����擾
        private bool stayFlg = false;
        private Animator myAnimator;                 // ��(���g)�̃A�j���[�V������ۑ�
        private Animator keyAnimator;                // ���̃A�j���[�V������ۑ�

        [SerializeField] private GameObject uiGauge; // �Q�[�W��ۑ�
        [SerializeField] private float time;

        [SerializeField] private LayerMask layer;

        public bool PlHitFlg
        {
            get { return stayFlg; }
            set { stayFlg = value; }
        }

        void Start()
        {
            player = GetGameObject.playerObject; // �v���C���[���擾
            // ���̓C���^�[�t�F�[�X���擾
            _IActionKey = player.GetComponent<IPlayerAction>();

            // �����蔻��C���^�[�t�F�[�X���擾
            _IHitPlayer = uiGauge.GetComponentInChildren<IHitPlayer>();
            boxCol = GetComponent<BoxCollider2D>();
            playerStatusManager = player.GetComponent<PlayerStatusManager>();
            //hideKey = GetGameObject.KeyObj;
            hideKey.SetActive(false);              // �����\��
            myAnimator = GetComponent<Animator>(); // ��(���g)�̃A�j���[�V�������擾
            keyAnimator = hideKey.GetComponent<Animator>(); // ���̃A�j���[�V�������擾
            uiGauge.SetActive(false);              // �Q�[�W���\��
        }

        void Update()
        {
            BoxBreak();
        }

        void FixedUpdate()
        {
            UpCast();
            RightRayCast();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // �v���C���[�������ė�����
            if (collision.gameObject == player)
            {
                stayFlg = true; // �؍ݒ�
                _IHitPlayer.IsHitPlayer();
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            // �v���C���[���o�čs������
            if (collision.gameObject == player)
            {
                stayFlg = false; // �؍݂��ĂȂ�
                _IHitPlayer.NonHitPlayer();
            }
        }


        /// <summary>
        /// ��(���g)������
        /// </summary>
        void BoxBreak()
        {
            if (StayInput())
            {
                playerStatusManager.PlayerIsInput(false);
                // �Q�[�W�����܂�����
                if (WaitTimeUI.gaugeMaxFlg == true)
                {
                    WaitTimeUI.gaugeMaxFlg = false;
                    myAnimator.SetTrigger("Destroy");  // �A�j���[�V�����Đ�
                    hideKey.transform.parent = null;   // �����q�I�u�W�F�N�g����O��
                    uiGauge.SetActive(false);          // �Q�[�W����U�B��
                    this.StartCoroutine(KeyAppTime());
                }
            }
            else if (UpInput()) playerStatusManager.PlayerIsInput(true);
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

        void UpCast()
        {
            Vector3 chkPos = transform.position;
            float boxHarfWitdh = boxCol.size.x / 2;
            bool result = false;
            Vector3 lineLength = -transform.up * 3f;

            // �R�_�`�F�b�N�i�Ƃ肠�����j
            chkPos.x = transform.position.x - boxHarfWitdh;
            //chkPos.x += 0.8f;
            chkPos.y += 0.2f;
            for (int loopNo = 0; loopNo < 3; loopNo++)
            {
                // ���g�̈ʒu���火�Ɍ������Đ��������āA�n�ʂɐڐG���������`�F�b�N����
                result |= Physics2D.Linecast(chkPos + transform.up, chkPos - lineLength, 0);
                //���C��\�����Ă݂�
                Debug.DrawLine(chkPos + transform.up, chkPos - lineLength, Color.red);

                // �I�t�Z�b�g���Z
                chkPos.x += boxHarfWitdh;
            }
        }

        void RightRayCast()
        {
            Vector3 offset = new Vector3(1.1f, 1, 0);
            //Ray�̍쐬�@�@�@�@�@�@�@��Ray���΂����_�@�@�@��Ray���΂�����
            Ray2D ray = new Ray2D(transform.position + offset, Vector3.right);

            //Ray�����������I�u�W�F�N�g�̏������锠
            RaycastHit2D hit;

            //Ray�̔�΂��鋗��
            int distance = 1;

            //Ray�̉���   ��Ray�̌��_�@�@�@�@��Ray�̕����@�@�@��Ray�̐F
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
            //               ��Ray  ��Ray�����������I�u�W�F�N�g ������
            hit = Physics2D.Raycast(ray.origin, ray.direction, distance, layer);
            //����Ray�ɃI�u�W�F�N�g���Փ˂�����

            if (hit.transform != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                //Ray�����������I�u�W�F�N�g��tag��Player��������
                if (hit.collider.gameObject == player)
                {
                    Debug.Log("���������k����");
                }
            }
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
