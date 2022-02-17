using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerCore : MonoBehaviour
    {
        private IInputReceivable _inputReceivable;
        private PlayerStatus _playerStatus;
        private PlayerStateManager stateManager;
        private GroundChecker groundChecker;

        private BoxCollider2D collid;

        private void Start()
        {
            _inputReceivable = GetComponent<IInputReceivable>();
            _playerStatus = GetComponent<PlayerStatus>();
            stateManager = GetComponent<PlayerStateManager>();

            groundChecker = GetComponent<GroundChecker>();
            collid = GetComponent<BoxCollider2D>();
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
            if (_inputReceivable.MoveKey_D() || _inputReceivable.MoveH() ==  1) _playerStatus.DirectionNum =  1;
            if (_inputReceivable.MoveKey_A() || _inputReceivable.MoveH() == -1) _playerStatus.DirectionNum = -1;

            transform.localScale = new Vector3(_playerStatus.DirectionNum * _playerStatus._SizeMag, 1f * _playerStatus._SizeMag, 1f);
        }
    }
}
