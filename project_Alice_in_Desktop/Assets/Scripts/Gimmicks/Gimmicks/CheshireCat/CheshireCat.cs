using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;
using GameSystem;

namespace Gimmicks
{
    public class CheshireCat : MonoBehaviour
    {
        private GameObject player;   // �v���C���[��ۑ�
        private Animator playerAnim; // �v���C���[�̃A�j���[�V�����ۑ�
        private IPlayerAction _ActionKey;
        private Animator myAnimator;
        private LayerChange layerChange;
        private CatFrontObj catFrontObj;

        [Header("���[�v��̃I�u�W�F�N�g")]
        [SerializeField] private GameObject warpPoint; // ���[�v��I�u�W�F�N�g���擾
        private CheshireCat warpScr;
        private Animator warpPointAnim;                // ���[�v��̃A�j���[�V�����ۑ�
        private bool warpFlg;
        private const float playerWarpWaitTime = 0.375f;

        private bool stayFlg = false;                  // �؍݂��Ă��邩�t���O
        [Header("�͂���UI���A�^�b�`")]
        [SerializeField] private GameObject hairuUI;

        //[Range(1, 5)]
        //[SerializeField] private int seNum = 1;

        void Start()
        {
            player = GetGameObject.playerObject; // �v���C���[���擾
            playerAnim = player.GetComponent<Animator>();
            _ActionKey = player.GetComponent<IPlayerAction>();
            myAnimator = GetComponent<Animator>();
            layerChange = GetComponent<LayerChange>();
            catFrontObj = GetComponent<CatFrontObj>();
            hairuUI.SetActive(false);
            warpPointAnim = warpPoint.GetComponent<Animator>();
            warpScr = warpPoint.GetComponent<CheshireCat>(); // ���[�v��̔L�̏������擾
        }

        void Update()
        {
            Warping();
            WarpValidSwitch();
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            bool enterObj = collision.gameObject.tag == "Gimmick" || collision.gameObject.layer == 6;
            // �v���C���[�������ė�����
            if (enterObj)
            {
                catFrontObj.IsFrontObj = true;
                warpScr.catFrontObj.IsFrontObj = true;
                catFrontObj.EnterObjNum += 1;
                warpScr.catFrontObj.EnterObjNum += 1;
            }
            else if (collision.gameObject == player && warpFlg == true)
            {
                stayFlg = true; // �؍݃t���O��true
                hairuUI.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            bool exitObj = collision.gameObject.tag == "Gimmick" || collision.gameObject.layer == 6;
            // �v���C���[���o�čs������
            if (exitObj)
            {
                catFrontObj.IsFrontObj = false;
                warpScr.catFrontObj.IsFrontObj = false;
                catFrontObj.EnterObjNum -= 1;
                warpScr.catFrontObj.EnterObjNum -= 1;
            }
            else if (collision.gameObject == player/* && warpFlg == false*/)
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
            if (StayInput()) PlayerWarpToHandlePos();
        }

        /// <summary>
        /// �v���C���[���G��Ă���A���A�L�[������
        /// </summary>
        /// <returns></returns>
        bool StayInput()
        {
            return stayFlg == true && _ActionKey.ActionKey_Down() && warpFlg == true;
        }

        /// <summary>
        /// ���[�v��Ƀv���C���[���ړ�������
        /// </summary>
        void PlayerWarpToHandlePos()
        {
            playerAnim.SetTrigger("Teleport");
            myAnimator.SetTrigger("Teleport");
            warpPointAnim.SetTrigger("Teleport");
            AudioManager.Instance.SeAction("�`�F�V���L_1");
            this.StartCoroutine(PlayerWarpStart());
        }

        void WarpValidSwitch()
        {
            bool isDisplayHide = layerChange.OutFlg == true || warpScr.layerChange.OutFlg == true || catFrontObj.IsFrontObj == true || warpScr.catFrontObj.IsFrontObj == true;
            bool enterObjCount = catFrontObj.EnterObjNum != 0 || warpScr.catFrontObj.EnterObjNum != 0;
            if (isDisplayHide ||  enterObjCount) // �܂��Ă���
            {
                //hairuUI.SetActive(false);
                warpFlg = false;
                myAnimator.SetBool("Close", true);
                warpPointAnim.SetBool("Close", true);
            }
            else
            {
                warpFlg = true;
                myAnimator.SetBool("Close", false);
                warpPointAnim.SetBool("Close", false);
            }
        }

        IEnumerator PlayerWarpStart()
        {
            yield return new WaitForSeconds(playerWarpWaitTime);
            player.transform.position = warpPoint.transform.position;
        }
    }
}
