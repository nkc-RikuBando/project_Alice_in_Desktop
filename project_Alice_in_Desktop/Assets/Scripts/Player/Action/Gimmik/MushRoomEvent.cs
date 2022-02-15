using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class MushRoomEvent : MonoBehaviour
    {
        // �L�m�R�ɐG����Player�̑傫�����ς�鏈��

        [SerializeField] private AudioClip _se_Big;
        [SerializeField] private AudioClip _se_Small;

        private PlayerStatus        _playerStatus;
        private PlayerStatusManager _statusManager;
        private AudioSource _audioSource;
        
        void Start()
        {
            _playerStatus = GetComponent<PlayerStatus>(); 
            _statusManager = GetComponent<PlayerStatusManager>();
            _audioSource = GetComponent<AudioSource>();
        }


        // ��Animation�̃C�x���g�ŌĂԊ֐�

        public void PlayerSizeChange() 
        {
            float size = transform.localScale.y;

            _playerStatus._SizeMag = _statusManager.GetSize();

            if (size == 1) return;
            if (size < 1) _audioSource.PlayOneShot(_se_Small);
            if (size > 1) _audioSource.PlayOneShot(_se_Big);
        }

        // ���͉\���\�b�h
        public void PlayerInput_True()
        {
            _statusManager.PlayerIsInput(true);
        }

        // ���͕s���\�b�h
        public void PlayerInput_False()
        {
            _statusManager.PlayerIsInput(false);
        }
    }

}

