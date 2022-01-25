using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;
using GameSystem;

namespace Gimmicks
// �撣���ĂĂ��炢�I�I�I�I�I�I
{
    public class CheshireCat : MonoBehaviour/*, IRenderingFlgSettable*/
    {
        private GameObject player; // �v���C���[��ۑ�
        private Animator playerAnim; // �v���C���[�̃A�j���[�V�����ۑ�
        private IPlayerAction _ActionKey;
        private Animator myAnimator;
        //private bool InOutFlg;                         // ��ʊO�ɂ��邩
        private LayerChange layerChange;

        [Header("���[�v��̃I�u�W�F�N�g")]
        [SerializeField] private GameObject warpPoint; // ���[�v��I�u�W�F�N�g���擾
        private CheshireCat warpScr;
        private Animator warpPointAnim;                // ���[�v��̃A�j���[�V�����ۑ�
        private bool warpFlg;
        private const float plaerWarpWaitTime = 0.375f;

        private bool stayFlg = false;                  // �؍݂��Ă��邩�t���O
        [Header("�͂���UI���A�^�b�`")]
        [SerializeField] private GameObject hairuUI;

        void Start()
        {
            player = GetGameObject.playerObject; // �v���C���[���擾
            playerAnim = player.GetComponent<Animator>();
            _ActionKey = player.GetComponent<IPlayerAction>();
            myAnimator = GetComponent<Animator>();
            //InOutFlg = true;
            layerChange = GetComponent<LayerChange>();
            warpPointAnim = warpPoint.GetComponent<Animator>();
            hairuUI.SetActive(false);
            warpScr = warpPoint.GetComponent<CheshireCat>();
        }

        void Update()
        {
            Warping();
            WarpValidSwitch();
            Debug.Log(warpFlg);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            // �v���C���[�������ė�����
            if (collision.gameObject == player && layerChange.OutFlg == false)
            {
                stayFlg = true; // �؍݃t���O��true
                hairuUI.SetActive(true);
            }
            else
            {
                warpFlg = false;
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
            this.StartCoroutine(PlayerWarpStart());
        }

        void WarpValidSwitch()
        {
            bool isDisplayHide = layerChange.OutFlg == true || warpScr.layerChange.OutFlg == true;
            if (isDisplayHide)
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
            yield return new WaitForSeconds(plaerWarpWaitTime);
            player.transform.position = warpPoint.transform.position;
        }
    }
}
