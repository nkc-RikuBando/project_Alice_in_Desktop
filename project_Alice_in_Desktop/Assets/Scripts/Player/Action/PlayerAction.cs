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
            if (!_playerStatus._InputFlgAction) return false;
            return _inputReceivable.ActionKey();
        }

        // Action�L�[�𗣂�����
        bool IPlayerAction.ActionKeyUp()
        {
            if (!_playerStatus._InputFlgAction) return false;
            return _inputReceivable.ActionKeyUp();
        }

        // Action�L�[����������
        bool IPlayerAction.ActionKey_Down()
        {
            if (!_playerStatus._InputFlgAction) return false;
            return _inputReceivable.ActionKey_Down();
        }


        // �󒆏�ԃ��\�b�h
        private void IsAction() 
        {
            if (_playerStatus._IsWindowTouching) return;

            // �󒆏�Ԃł̓A�N�V�������͂��ł��Ȃ�
            if (_rb.velocity.y > 0.1f || _rb.velocity.y < -0.1f) _playerStatus._InputFlgAction = false;
            else _playerStatus._InputFlgAction = true;
        }
    }
}
