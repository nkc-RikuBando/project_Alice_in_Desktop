using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class MushRoomEvent : MonoBehaviour
    {
        // �L�m�R�ɐG����Player�̑傫�����ς�鏈��

        PlayerStatusManager _statusManager;
        
        void Start()
        {
            _statusManager = GetComponent<PlayerStatusManager>();
        }


        // ��Animation�̃C�x���g�ŌĂԊ֐�

        // �������Ȃ郁�\�b�h
        public void PlayerSizeChange_Small() 
        {
            _statusManager.PlayerSizeChange(0.5f);
        }

        // �傫���Ȃ郁�\�b�h
        public void PlayerSizeChange_Big() 
        {
            _statusManager.PlayerSizeChange(1.5f);
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

