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

        private int _dir = 1;

        private void Start()
        {
            _inputReceivable = GetComponent<IInputReceivable>();
            _playerStatus = GetComponent<PlayerStatus>();
        }

        private void Update()
        {
            if (_playerStatus._IsWindowTouching) return;
            if (!_playerStatus._InputFlgX) return;

            PlayerDirection();
        }

        // Player�̌����ύX���\�b�h
        private void PlayerDirection()
        {
            if (_inputReceivable.MoveKey_D() || _inputReceivable.MoveH() ==  1) _dir =  1;
            if (_inputReceivable.MoveKey_A() || _inputReceivable.MoveH() == -1) _dir = -1;

            transform.localScale = new Vector3(_dir * _playerStatus._SizeMag, 1f * _playerStatus._SizeMag, 1f);
        }
    }
}
