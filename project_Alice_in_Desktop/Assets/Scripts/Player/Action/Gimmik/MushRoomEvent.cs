using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class MushRoomEvent : MonoBehaviour
    {
        // �L�m�R�ɐG����Player�̑傫�����ς�鏈��

        private PlayerStatus        _playerStatus;
        private PlayerStatusManager _statusManager;
        
        void Start()
        {
            _playerStatus = GetComponent<PlayerStatus>(); 
            _statusManager = GetComponent<PlayerStatusManager>();
        }


        // ��Animation�̃C�x���g�ŌĂԊ֐�

        public void PlayerSizeChange() 
        {
            _playerStatus._SizeMag = _statusManager.GetSize();
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

