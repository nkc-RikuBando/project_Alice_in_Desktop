using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;
using Animation;

namespace GameSystem
{
    public class ClearFlg : MonoBehaviour, IGetKey
    {// ����m���Ă���B

        [SerializeField] private List<GameObject> keyList = new List<GameObject>();

        private GameObject player;
        [SerializeField] private GameObject inputUI;
        private IPlayerAction _IActionKey;                 // ���̓C���^�[�t�F�[�X��ۑ�
        //private IKeyCount iKeyCount;
        private Animator animator;                  // �A�j���[�^�[�̕ۑ�

        private ClearEffect clearEffect;            // �N���A�G�t�F�N�g�̕ۑ�
        private bool clearFlg;
        private bool stayFlg;

        [SerializeField] private float width; //�I�u�W�F�N�g�Ԃ̕�
        [SerializeField] private GameObject geneKeyUI; // ��������UI
        private Vector3 uiPos; // UI�̐����ʒu��ۑ�
        private GameObject frame;
        //private int geneKeyNum;

        private void Awake()
        {
            //iKeyCount = gameObject.GetComponent<IKeyCount>();
            //iKeyCount.keyCount(keyList.Count);
        }

        void Start()
        {
            player = GetGameObject.playerObject;
            _IActionKey = player.GetComponent<IPlayerAction>(); // ���̓C���^�[�t�F�[�X���擾
            
            animator = GetComponent<Animator>();  // �A�j���[�^�[���擾
            clearEffect = GetComponent<ClearEffect>(); // �N���A�G�t�F�N�g���擾
            clearFlg = false;
            inputUI.SetActive(false);
            if (keyList.Count <= 0) Clear();

            frame = GetGameObject.FrameObject;
            //KeyCountUI();
        }

        void Update()
        {
            animator.SetBool("Locked", true);
            if (IsSceceMove())
            {
                if (clearFlg == true)
                {
                    animator.SetTrigger("Action");
                    clearEffect.StartClearEffect();
                }
                else
                    animator.SetTrigger("Action");
            }
            if (clearFlg == true)
                animator.SetBool("Locked", false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player)
            {
                stayFlg = true;
                inputUI.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == player)
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
            keyList.Remove(get); // ���X�g���献������
            if (keyList.Count <= 0) Clear(); // �N���A���\�b�h���Ă�

            //Destroy(get); // ���X�g����������献���g������
        }

        public void Clear()
        {
            clearFlg = true; // �N���A�t���O��true�ɂ���
        }

        void KeyCountUI()
        {
            // ���̃X�N���v�g����ꂽ�I�u�W�F�N�g�̈ʒu
            uiPos = transform.position;

            //X����horizontal�̐��������ׂ�
            for (int i = 0; i < keyList.Count; i++)
            {
                Vector3 genePos = new Vector3(-33+(uiPos.x + keyList.Count * width / 2 - i * width - width / 2), 7);
                //Prefab��Cube�𐶐�����
                GameObject copy = Instantiate(geneKeyUI, genePos, Quaternion.identity);
                copy.transform.SetParent(frame.transform);
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
