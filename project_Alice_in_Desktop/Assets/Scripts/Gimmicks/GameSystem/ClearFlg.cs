using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Connector.Inputer;

namespace GameSystem
{
    public class ClearFlg : MonoBehaviour, IGetKey
    {// ����m���Ă���B

        [SerializeField] private List<GameObject> keyList = new List<GameObject>();

        //[System.NonSerialized] public bool clearFlg; 
        private GameObject player;
        [SerializeField] private string playerName; // �v���C���[�̖��O���擾
        private ITestKey _ITestKey;                 // ���̓C���^�[�t�F�[�X��ۑ�
        private Animator animator;                  // �A�j���[�^�[�̕ۑ�

        private bool clearFlg;
        private bool stayFlg;
        [SerializeField] private string sceneName;   // �V�[���ړ���̖��O
        [SerializeField] private float fadeTime;     // �t�F�[�h���鎞��

        void Start()
        {
            player = GameObject.Find(playerName); // �v���C���[�I�u�W�F�N�g���擾
            _ITestKey = GetComponent<ITestKey>(); // ���̓C���^�[�t�F�[�X���擾
            animator = GetComponent<Animator>();  // �A�j���[�^�[���擾
            clearFlg = false;
            if (keyList.Count <= 0) Clear();
        }

        void Update()
        {
            animator.SetBool("Locked", true);
            if (IsSceceMove())
            {
                if (clearFlg == true)
                    FadeManager.Instance.LoadScene(sceneName, fadeTime);
                else 
                    animator.SetTrigger("Action");
            }
            if(clearFlg == true)
                animator.SetBool("Locked", false);
        }

        public void AddKey(GameObject set)
        {
            keyList.Add(set);
        }

        // �����������烊�X�g�������
        public void GetKey(GameObject get)
        {
            keyList.Remove(get); // ���X�g���献������
            if (keyList.Count <= 0)
                Clear(); // �N���A���\�b�h���Ă�

            Destroy(get); // ���X�g����������献���g������
        }

        public void Clear()
        {
            clearFlg = true; // �N���A�t���O��true�ɂ���
        }

        /// <summary>
        /// �V�[���ړ��������
        /// </summary>
        /// <returns></returns>
        bool IsSceceMove()
        {
            return _ITestKey.EventKey() && stayFlg == true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject == player) stayFlg = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == player) stayFlg = false;
        }
    }
}
