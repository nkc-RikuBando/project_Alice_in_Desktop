using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;


namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        // Playerの移動処理

        private IInputReceivable _inputReceivable;
        private PlayerStatus     _playerStatus;
        private PlayerAnimation  _playerAnimation;
        private Rigidbody2D      _rb;


        private void Start()
        {
            _inputReceivable = GetComponent<IInputReceivable>();
            _playerStatus    = GetComponent<PlayerStatus>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _rb              = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_playerStatus._InputFlgX) HorizontalMove();
        }



        // --------- 移動処理 ---------
        private void HorizontalMove()
        {
            // 移動の物理処理
            _rb.velocity = new Vector2(_inputReceivable.MoveH() * _playerStatus._Speed, _rb.velocity.y);

            // Playerの向き
            if (_inputReceivable.MoveH() != 0)
            {
                transform.localScale = new Vector3(_inputReceivable.MoveH(), 1f, 1f);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Dash"), true);
                _playerStatus._StateEnum = PlayerStateEnum.DASH;
            }
            else if (_inputReceivable.MoveH() == 0 && _playerStatus._GroundChecker)
            {
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Dash"), false);
                _playerStatus._StateEnum = PlayerStateEnum.STAY;
            }
        }

    }

}
