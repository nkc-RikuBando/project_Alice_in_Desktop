using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;
using GameSystem;

namespace Gimmicks
// �撣���ĂĂ��炢�I�I�I�I�I�I
{
    public class CheshireCat : MonoBehaviour, IRenderingFlgSettable
    {
        private GameObject player; // �v���C���[��ۑ�
        private Animator playerAnim; // �v���C���[�̃A�j���[�V�����ۑ�
        private IPlayerAction _ActionKey;
        private Animator myAnimator;
        private bool InOutFlg = true;                         // ��ʊO�ɂ��邩

        [Header("���[�v��̃I�u�W�F�N�g")]
        [SerializeField] private GameObject warpPoint; // ���[�v��I�u�W�F�N�g���擾
        private CheshireCat warpScr;
        private Animator warpPointAnim;                // ���[�v��̃A�j���[�V�����ۑ�

        private bool stayFlg = false;                  // �؍݂��Ă��邩�t���O
        [Header("�͂���UI���A�^�b�`")]
        [SerializeField] private GameObject hairuUI;

        public bool InOut
        {
            get { return InOutFlg; }
            set { InOutFlg = value; }
        }

        void Start()
        {
            player = GetGameObject.playerObject; // �v���C���[���擾
            playerAnim = player.GetComponent<Animator>();
            _ActionKey = player.GetComponent<IPlayerAction>();
            myAnimator = GetComponent<Animator>();
            InOutFlg = true;
            warpScr = warpPoint.GetComponent<CheshireCat>();
            warpPointAnim = warpPoint.GetComponent<Animator>();
            hairuUI.SetActive(false);
        }

        void Update()
        {
            Warping();
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            // �v���C���[�������ė�����
            if (collision.gameObject == player && warpScr.InOut)
            {
                stayFlg = true; // �؍݃t���O��true
                hairuUI.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // �v���C���[���o�čs������
            if (collision.gameObject == player)
            {
                stayFlg = false; // �؍݃t���O��false
                hairuUI.SetActive(false);
            }
        }

        /// <summary>
        /// ���[�v����
        /// </summary>
        void Warping()
        {
            if (StayInput()) WarpPlace();
        }

        /// <summary>
        /// �v���C���[���G��Ă���A���AQ�L�[������
        /// </summary>
        /// <returns></returns>
        bool StayInput()
        {
            return stayFlg == true && _ActionKey.ActionKey_Down();
        }

        /// <summary>
        /// ���[�v��Ƀv���C���[���ړ�������
        /// </summary>
        void WarpPlace()
        {
            playerAnim.SetTrigger("Teleport");
            myAnimator.SetTrigger("Teleport");
            warpPointAnim.SetTrigger("Teleport");
            this.StartCoroutine(WarpTime());
        }

        public void SetRenderingFlg(bool val)
        {
            InOutFlg = val;
            if (InOutFlg == false)
            {
                warpScr.enabled = false;
                myAnimator.SetBool("Close", true);
                warpPointAnim.SetBool("Close", true);
            }
            else
            {
                warpScr.enabled = true;
                myAnimator.SetBool("Close", false);
                warpPointAnim.SetBool("Close", false);
            }
        }

        IEnumerator WarpTime()
        {
            yield return new WaitForSeconds(0.375f);
            player.transform.position = warpPoint.transform.position;
        }
    }
}
