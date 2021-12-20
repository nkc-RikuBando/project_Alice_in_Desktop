using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Connector.Player;

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
            bool _inputFlg;

            if (_playerStatus._InputFlgAction) _inputFlg = _inputReceivable.ActionKey(); 
            else                               _inputFlg = false;

            return _inputFlg;
        }

        // Actionキーを離した時
        bool IPlayerAction.ActionKeyUp()
        {
            bool _inputFlg;

            if (_playerStatus._InputFlgAction) _inputFlg = _inputReceivable.ActionKeyUp();
            else                               _inputFlg = false;

            return _inputFlg;
        }

        // Actionキーを押した時
        bool IPlayerAction.ActionKey_Down()
        {
            bool _inputFlg;

            if (_playerStatus._InputFlgAction) _inputFlg = _inputReceivable.ActionKey_Down();
            else                               _inputFlg = false;

            return _inputFlg;
        }


        // 空中状態メソッド
        private void IsAction() 
        {
            // 空中状態ではアクション入力ができない
            if (_rb.velocity.y != 0) _playerStatus._InputFlgAction = false;
            else                     _playerStatus._InputFlgAction = true;
        }
    }
}
