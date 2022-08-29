using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Connector.Player;
using UnityEngine.UI;

namespace Player
{
    public class PlayerAction : MonoBehaviour,IPlayerAction
    {
        // アクション入力処理

        private IInputReceivable _inputReceivable;
        private PlayerStatus     _playerStatus;
        private Rigidbody2D      _rb;


        private void Start()
        {
            _inputReceivable = GetComponent<IInputReceivable>();
            _playerStatus    = GetComponent<PlayerStatus>();
            _rb              = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            IsAction();
        }



        // Actionキーを押している間
        bool IPlayerAction.ActionKey()
        {
            if (!_playerStatus._InputFlgAction) return false;
            return _inputReceivable.ActionKey();
        }

        // Actionキーを離した時
        bool IPlayerAction.ActionKeyUp()
        {
            if (!_playerStatus._InputFlgAction) return false;
            return _inputReceivable.ActionKeyUp();
        }

        // Actionキーを押した時
        bool IPlayerAction.ActionKey_Down()
        {
            if (!_playerStatus._InputFlgAction) return false;
            return _inputReceivable.ActionKey_Down();
        }


        // 空中状態メソッド
        private void IsAction() 
        {
            if (_playerStatus._IsWindowTouching) return;

            // 空中状態ではアクション入力ができない
            if (_rb.velocity.y > 0.1f || _rb.velocity.y < -0.1f) _playerStatus._InputFlgAction = false;
            else _playerStatus._InputFlgAction = true;
        }
    }
}
