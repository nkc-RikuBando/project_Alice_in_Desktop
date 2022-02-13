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

        // Playerの向き変更メソッド
        private void PlayerDirection() 
        {
            if(_inputReceivable.MoveH() != 0) 
            {
                transform.localScale = new Vector3(_inputReceivable.MoveH() *_playerStatus._SizeMag, 1f * _playerStatus._SizeMag, 1f);
            }
        }
    }
}
