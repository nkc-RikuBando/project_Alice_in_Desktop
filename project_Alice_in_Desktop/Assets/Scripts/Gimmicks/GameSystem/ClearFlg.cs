using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Connector.Inputer;
using Animation;

namespace GameSystem
{
    public class ClearFlg : MonoBehaviour, IGetKey
    {// ����m���Ă���B

        [SerializeField] private List<GameObject> keyList = new List<GameObject>();

        //[System.NonSerialized] public bool clearFlg; 
        [SerializeField] private GameObject player;
        //[SerializeField] private string playerName; // �v���C���[�̖��O���擾
        private ITestKey _ITestKey;                 // ���̓C���^�[�t�F�[�X��ۑ�
        private Animator animator;                  // �A�j���[�^�[�̕ۑ�
        private ClearEffect clearEffect;            // �N���A�G�t�F�N�g�̕ۑ�

        private bool clearFlg;
        private bool stayFlg;
        //[SerializeField] private string sceneName;   // �V�[���ړ���̖��O
        //[SerializeField] private float fadeTime;     // �t�F�[�h���鎞��

        //private GameObject canvas; // �L�����o�X�̕ۑ�
        //public float high;
        //public float width;
        //public int horizontal;
        //private Vector3 pos;
        //public GameObject cube;

        void Start()
        {
            //player = GameObject.Find(playerName); // �v���C���[�I�u�W�F�N�g���擾
            _ITestKey = GetComponent<ITestKey>(); // ���̓C���^�[�t�F�[�X���擾
            animator = GetComponent<Animator>();  // �A�j���[�^�[���擾
            clearEffect = GetComponent<ClearEffect>(); // �N���A�G�t�F�N�g���擾
            clearFlg = false;
            if (keyList.Count <= 0) Clear();

            //canvas = GameObject.Find("Canvas");   // �L�����o�X�̎擾
            //for (int i = 0; i < keyList.Count; i++)
            //{
            //    GameObject ui = Instantiate(cube, canvas.transform);
            //    ui.transform.position = new Vector3(pos.x + keyList.Count * width / 2 - i * width - width / 2, 0, 0);
            //}
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
                //FadeManager.Instance.LoadScene(sceneName, fadeTime);
                else
                    animator.SetTrigger("Action");
            }
            if(clearFlg == true)
                animator.SetBool("Locked", false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player) stayFlg = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == player) stayFlg = false;
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
    }
}
