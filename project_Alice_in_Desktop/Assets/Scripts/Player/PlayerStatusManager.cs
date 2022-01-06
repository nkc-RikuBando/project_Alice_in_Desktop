using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;

namespace Player
{
    public class PlayerStatusManager : MonoBehaviour, IPlayerStatusSentable
    {
        // �X�e�[�^�X��ύX���鏈��

        private PlayerStatus _playerStatus;

        private void Awake()
        {
            _playerStatus = GetComponent<PlayerStatus>();
        }


        // ���͎�t�Ǘ����\�b�h
        public void PlayerIsInput(bool flg)
        {
            _playerStatus._InputFlgX = flg;
            _playerStatus._InputFlgY = flg;
            _playerStatus._InputFlgAction = flg;
        }

        // Player�T�C�Y�ύX���\�b�h
        void IPlayerStatusSentable.PlayerSizeChange(float mag)
        {
            Vector3 _playerScale = transform.localScale;

            // �傫���������� or ���������傫���@=�@�ʏ�
            if (Mathf.Abs(_playerScale.x) < 1 && mag > 1)�@�@   mag = 1;
            else if (Mathf.Abs(_playerScale.x) > 1 && mag < 1)�@mag = 1;

            // �T�C�Y�ɂ���ăX�e�[�^�X��ύX
            switch (mag) 
            {
                case 1:   // �ʏ�
                    _playerStatus._Speed     = _playerStatus._Speed;
                    _playerStatus._JumpPower = _playerStatus._JumpPower;
                    break;
                case 0.5f:// ��������
                    _playerStatus._Speed     = _playerStatus._SmallStateSpeed;
                    _playerStatus._JumpPower = _playerStatus._SmallStateJumpPower;
                    break;
                case 1.5f:  // �傫����
                    _playerStatus._Speed     = _playerStatus._BigStateSpeed;
                    _playerStatus._JumpPower = _playerStatus._BigStateJumpPower;
                    break;
                default:
                    break;
            }

            // �T�C�Y��ύX
            _playerStatus._SizeMag = mag;
            transform.localScale = new Vector3(mag, mag, 1f);
        }
    }

}
