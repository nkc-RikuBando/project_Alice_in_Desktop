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

        private float _defaultSpeed;
        private float _defaultJumpPower;
        private float _defaultWallJumpPower;
        private float _defaultWallJumpAngle;

        private void Awake()
        {
            _playerStatus = GetComponent<PlayerStatus>();
            _defaultSpeed = _playerStatus._Speed;
            _defaultJumpPower = _playerStatus._JumpPower;
            _defaultWallJumpPower = _playerStatus._WallJumpPower;
            _defaultWallJumpAngle = _playerStatus._WallJumpAngle;
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
                    _playerStatus._Speed     = _defaultSpeed;
                    _playerStatus._JumpPower = _defaultJumpPower;
                    _playerStatus._WallJumpPower = _defaultWallJumpPower;
                    _playerStatus._WallJumpAngle = _defaultWallJumpAngle;

                    break;
                case 0.5f:// ��������
                    _playerStatus._Speed     = _playerStatus._SmallStateSpeed;
                    _playerStatus._JumpPower = _playerStatus._SmallStateJumpPower;
                    _playerStatus._WallJumpPower = _playerStatus._SmallStateWallJumpPower;
                    _playerStatus._WallJumpAngle = _playerStatus._BigStateJumpAngle;
                    break;
                case 1.5f:  // �傫����
                    _playerStatus._Speed     = _playerStatus._BigStateSpeed;
                    _playerStatus._JumpPower = _playerStatus._BigStateJumpPower;
                    _playerStatus._WallJumpPower = _playerStatus._BigStateWallJumpPower;
                    _playerStatus._WallJumpAngle = _playerStatus._SmallStateJumpAngle;
                    break;
                default:
                    break;
            }

            // �T�C�Y��ύX
            _playerStatus._SizeMag = mag;
            //transform.localScale = new Vector3(mag, mag, 1f);
        }
    }

}
