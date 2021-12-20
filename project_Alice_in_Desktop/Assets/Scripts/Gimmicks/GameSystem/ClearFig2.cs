using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Connector.Inputer;

namespace GameSystem
{
    public class ClearFig2 : MonoBehaviour, IHitSwitch
    {
        [SerializeField] private GameObject player;
        private ITestKey _ITestKey;                 // ���̓C���^�[�t�F�[�X��ۑ�
        private Animator animator;                  // �A�j���[�^�[��ۑ�

        private bool switchFlg;
        private bool stayFlg;
        [SerializeField] private string sceneName;   // �V�[���ړ���̖��O
        [SerializeField] private float fadeTime;     // �t�F�[�h���鎞��

        public enum AnimeType
        {
            DOOR_ROCK_NOW, DOOR_ROCK_KAIJO, DOOR_ROOK_ACTION
        }
        AnimeType animeType;

        void Start()
        {
            animator = GetComponent<Animator>();  // �A�j���[�^�[���擾
            _ITestKey = GetComponent<ITestKey>(); // ���̓C���^�[�t�F�[�X���擾
            switchFlg = false;
            stayFlg = false;
        }

        void Update()
        {
            AnimeSwitch();
            AnimePlay();
            
            //animator.SetBool("Locked", true);          // �����������Ă���A�j��
            //if(IsSceceMove())
            //{
            //    if (switchFlg == true) �@�@�@�@�@�@�@�@// �X�C�b�`�I��
            //        animator.SetBool("Locked", false); // �������A�j��
            //    else                   �@�@�@�@�@�@�@�@// �X�C�b�`�I�t
            //        animator.SetTrigger("Action");�@�@ // �����������Ă���A�N�V����
            //}
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player) stayFlg = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == player) stayFlg = false;
        }

        public void Switch(bool switchOn)
        {
            switchFlg = switchOn;
        }

        /// <summary>
        /// �A�j���[�^�[�̎��
        /// </summary>
        void AnimeSwitch()
        {
            switch (animeType)
            {
                case AnimeType.DOOR_ROCK_KAIJO:
                    animator.SetBool("Locked", false);
                    break;
                case AnimeType.DOOR_ROCK_NOW:
                    animator.SetBool("Locked", true);
                    break;
                case AnimeType.DOOR_ROOK_ACTION:
                    animator.SetTrigger("Action");
                    break;
            }
        }

        /// <summary>
        /// �A�j���[�^�[�̎��s
        /// </summary>
        void AnimePlay()
        {
            if (switchFlg == true)
            {
                animeType = AnimeType.DOOR_ROCK_KAIJO;
            }
            else animeType = AnimeType.DOOR_ROCK_NOW;

            if (IsSceceMove())
            {
                animeType = AnimeType.DOOR_ROOK_ACTION;
                if(switchFlg == true) FadeManager.Instance.LoadScene(sceneName, fadeTime);
            }
        }

        /// <summary>
        /// �L�[���͂��違�v���C���[���؍�
        /// </summary>
        /// <returns></returns>
        bool IsSceceMove()
        {
            return _ITestKey.EventKey() && stayFlg == true;
        }
    }
}
