using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class GimmickEvent : MonoBehaviour
    {
        // �L�m�R�ɐG����Player�̑傫�����ς�鏈��

        [SerializeField] private AudioClip _pushSE;

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
            _playerStatus._SizeMag = _statusManager.GetSize();
            transform.localScale = transform.localScale = new Vector3(_playerStatus.DirectionNum * _playerStatus._SizeMag, 1f * _playerStatus._SizeMag, 1f);
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

        // ��������SE�Đ����\�b�h
        public void PushPlaySE() 
        {
            _audioSource.PlayOneShot(_pushSE);
        }
    }

}

