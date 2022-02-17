using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;
using Animation;
using Player;
using StageSelect;
using Gimmicks;

namespace GameSystem
{
    public class ClearFlg : MonoBehaviour, IGetKey
    {// ����m���Ă���B

        // ���̃��X�g
        [Header("���̃A�^�b�`�s�v")]
        [SerializeField] private List<GameObject> keyList = new List<GameObject>();

        private GameObject player;
        [SerializeField] private GameObject inputUI;
        private IPlayerAction _IActionKey;        // ���̓C���^�[�t�F�[�X��ۑ�
        private PlayerStatusManager playerStatusManager;
        private Animator animator;                  // �A�j���[�^�[�̕ۑ�

        private ClearEffect clearEffect;            // �N���A�G�t�F�N�g�̕ۑ�
        private bool clearFlg;
        private bool stayFlg;
        [SerializeField] private int stageNum;
        private ISendClearStageNum iSendClearStageNum;

        private LayerChange layerChange;

        //[Range(1, 3)]
        //[SerializeField] private int seNum = 1;
        private bool seFlg;

        void Start()
        {
            player = GetGameObject.playerObject;
            _IActionKey = player.GetComponent<IPlayerAction>(); // ���̓C���^�[�t�F�[�X���擾
            playerStatusManager = player.GetComponent<PlayerStatusManager>();
            playerStatusManager.PlayerIsInput(true);
            animator = GetComponent<Animator>();  // �A�j���[�^�[���擾
            clearEffect = GetComponent<ClearEffect>(); // �N���A�G�t�F�N�g���擾
            clearFlg = false;
            stayFlg = false;
            inputUI.SetActive(false);
            if (keyList.Count <= 0) Clear();
            layerChange = GetComponent<LayerChange>();

            seFlg = true;

            //iSendClearStageNum = GameObject.Find("StageManagerSingleton").GetComponent<ISendClearStageNum>();
        }

        void Update()
        {
            ClearAnime();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player && layerChange.OutFlg == false)
            {
                stayFlg = true;
                inputUI.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == player /*&& layerChange.OutFlg == false*/)
            {
                stayFlg = false;
                inputUI.SetActive(false);
            }
        }

        public void AddKey(GameObject set)
        {
            keyList.Add(set);
        }

        // �����������烊�X�g�������
        public void GetKey(GameObject get)
        {
            keyList.Remove(get);             // ���X�g���献������
            if (keyList.Count <= 0) Clear(); // �N���A���\�b�h���Ă�
            //Destroy(get); // ���X�g����������献���g������
        }

        public void Clear()
        {
            clearFlg = true; // �N���A�t���O��true�ɂ���
        }

        /// <summary>
        /// �N���A���̉��o
        /// </summary>
        void ClearAnime()
        {
            animator.SetBool("Locked", true);
            if (IsSceceMove())
            {
                if (clearFlg == true)
                {
                    //if(iSendClearStageNum != null) iSendClearStageNum.SendClearStage(stageNum);
                    playerStatusManager.PlayerIsInput(false); // ���̓��͂��󂯕t���Ȃ�����
                    animator.SetTrigger("Action");
                    clearEffect.StartClearEffect();
                }
                else
                {
                    animator.SetTrigger("Action");
                    AudioManager.Instance.SeAction("DoorKnock");
                }
            }

            if (clearFlg == true)
            {
                animator.SetBool("Locked", false);
                if(seFlg)
                {
                    AudioManager.Instance.SeAction("DoorOpen");
                    seFlg = false;
                }
            }
        }

        /// <summary>
        /// �V�[���ړ��������
        /// </summary>
        /// <returns></returns>
        bool IsSceceMove()
        {
            return _IActionKey.ActionKey_Down() && stayFlg == true;
        }
    }
}
