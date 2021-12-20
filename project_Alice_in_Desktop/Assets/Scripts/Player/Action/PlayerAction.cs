using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Connector.Player;

namespace Player
{
    public class PlayerAction : MonoBehaviour,IPlayerAction
    {
        // �A�N�V�������͏���

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



        // Action�L�[�������Ă����
        bool IPlayerAction.ActionKey()
        {
            bool _inputFlg;

            if (_playerStatus._InputFlgAction) _inputFlg = _inputReceivable.ActionKey(); 
            else                               _inputFlg = false;

            return _inputFlg;
        }

        // Action�L�[�𗣂�����
        bool IPlayerAction.ActionKeyUp()
        {
            bool _inputFlg;

            if (_playerStatus._InputFlgAction) _inputFlg = _inputReceivable.ActionKeyUp();
            else                               _inputFlg = false;

            return _inputFlg;
        }

        // Action�L�[����������
        bool IPlayerAction.ActionKey_Down()
        {
            bool _inputFlg;

            if (_playerStatus._InputFlgAction) _inputFlg = _inputReceivable.ActionKey_Down();
            else                               _inputFlg = false;

            return _inputFlg;
        }


        // �󒆏�ԃ��\�b�h
        private void IsAction() 
        {
            // �󒆏�Ԃł̓A�N�V�������͂��ł��Ȃ�
            if (_rb.velocity.y != 0) _playerStatus._InputFlgAction = false;
            else                     _playerStatus._InputFlgAction = true;
        }
    }
}
