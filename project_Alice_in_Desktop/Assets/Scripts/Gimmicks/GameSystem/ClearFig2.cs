using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Connector.Player;
using Animation;

namespace GameSystem
{
    public class ClearFig2 : MonoBehaviour, IHitSwitch
    {
        [SerializeField] private GameObject player;
        private IPlayerAction _IActionKey;                 // ���̓C���^�[�t�F�[�X��ۑ�
        private Animator animator;                  // �A�j���[�^�[��ۑ�
        private ClearEffect clearEffect;            // �N���A�G�t�F�N�g��ۑ�

        private bool switchFlg;
        private bool stayFlg;
        //[SerializeField] private string sceneName;   // �V�[���ړ���̖��O
        //[SerializeField] private float fadeTime;     // �t�F�[�h���鎞��

        public enum AnimeType
        {
            DOOR_ROCK_NOW, DOOR_ROCK_KAIJO, DOOR_ROOK_ACTION
        }
        AnimeType animeType;

        void Start()
        {
            _IActionKey = player.GetComponent<IPlayerAction>(); // ���̓C���^�[�t�F�[�X���擾
            animator = GetComponent<Animator>();  // �A�j���[�^�[���擾
            clearEffect = GetComponent<ClearEffect>(); // �N���A�G�t�F�N�g���擾
            switchFlg = false;
            stayFlg = false;
        }

        void Update()
        {
            AnimeSwitch();
            AnimePlay();
            Debug.Log("switch��" + switchFlg);

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
            animeType = AnimeType.DOOR_ROCK_NOW;
            if (switchFlg == true)
            {
                animeType = AnimeType.DOOR_ROCK_KAIJO;
            }

            if (IsSceceMove())
            {
                animeType = AnimeType.DOOR_ROOK_ACTION;
                if(switchFlg == true)
                {
                    clearEffect.StartClearEffect();
                    // FadeManager.Instance.LoadScene(sceneName, fadeTime);
                }
            }
        }

        /// <summary>
        /// �L�[���͂��違�v���C���[���؍�
        /// </summary>
        /// <returns></returns>
        bool IsSceceMove()
        {
            return _IActionKey.ActionKey_Down() && stayFlg == true;
        }
    }
}
