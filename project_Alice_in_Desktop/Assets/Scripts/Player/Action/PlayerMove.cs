using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;

namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        // Player�̈ړ�����

        private IInputReceivable _inputReceivable;
        private PlayerStatus _playerStatus;
        private PlayerStatusManager playerStatusManager;
        private PlayerStatusManager _statusManager;
        private PlayerAnimation _playerAnimation;
        private Rigidbody2D _rb;


        private void Start()
        {
            playerStatusManager = GetComponent<PlayerStatusManager>();

            _inputReceivable = GetComponent<IInputReceivable>();
            _playerStatus = GetComponent<PlayerStatus>();
            _statusManager = GetComponent<PlayerStatusManager>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _rb = GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            if (_playerStatus._InputFlgX)
            {
                HorizontalMove();
                PlayerDirection();
            }

            if (Input.GetKeyDown(KeyCode.C)) 
            {
                Debug.Log("�Î~");
                playerStatusManager.PlayerIsInput(false);
            }
            if (Input.GetKeyDown(KeyCode.K)) 
            {
                Debug.Log("�Đ�");
                playerStatusManager.PlayerIsInput(true);
            }
        }



        // Player�̈ړ����\�b�h
        private void HorizontalMove()
        {
            // �ړ��̕�������
            _rb.velocity = new Vector2(_inputReceivable.MoveH() * _playerStatus._Speed, _rb.velocity.y);
        }

        // Player�̌������\�b�h
        private void PlayerDirection()
        {
            bool _playerDic = _inputReceivable.MoveH() == 0;

            // ���͂���Ă��Ȃ����
            if (_playerDic)
            {
                transform.localScale = new Vector3(1f * _statusManager.ScaleMagnification,
                                                   1f * _statusManager.ScaleMagnification,
                                                   1f);

                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Dash"), false);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"), false);
            }
            // �ړ����
            else if (!_playerDic)
            {
                transform.localScale = new Vector3(_inputReceivable.MoveH() * _statusManager.ScaleMagnification,
                                                    1f * _statusManager.ScaleMagnification,
                                                    1f);

                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Dash"), true);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"), false);
            }
            // �Î~���
            else if (_playerDic && _playerStatus._GroundChecker)
            {
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Dash"), false);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"), false);
            }
        }
    }

}
