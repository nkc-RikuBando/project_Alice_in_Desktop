using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;
using Connector.Inputer;
using Player;

namespace PlayerState
{
    public class PlayerCore : MonoBehaviour
    {
        private IInputReceivable _inputReceivable;
        private PlayerStatus _playerStatus;
        private PlayerStateManager stateManager;

        private int _dir = 1;

        private void Start()
        {
            _inputReceivable = GetComponent<IInputReceivable>();
            _playerStatus = GetComponent<PlayerStatus>();
            stateManager = GetComponent<PlayerStateManager>();
        }

        private void Update()
        {
            if (_playerStatus._IsWindowTouching) return;
            if (!_playerStatus._InputFlgX) return;

            PlayerDirection();
        }

        // Playerの向き変更メソッド
        private void PlayerDirection()
        {
            bool flg1 = stateManager.crrentPlayerState == PlayerStateEnum.WALLJUMP;
            bool flg2 = stateManager.crrentPlayerState == PlayerStateEnum.WALLJUMPUP;
            bool flg3 = stateManager.crrentPlayerState == PlayerStateEnum.WALLJUMPFALL;
            bool flg4 = stateManager.crrentPlayerState == PlayerStateEnum.LANDING;
            bool flg5 = stateManager.crrentPlayerState == PlayerStateEnum.STAY;
            if (flg1 || flg2 || flg3 || flg4 || flg5) return;

            if (_inputReceivable.MoveKey_D() || _inputReceivable.MoveH() ==  1) _dir =  1;
            if (_inputReceivable.MoveKey_A() || _inputReceivable.MoveH() == -1) _dir = -1;

            transform.localScale = new Vector3(_dir * _playerStatus._SizeMag, 1f * _playerStatus._SizeMag, 1f);
        }
    }
}
