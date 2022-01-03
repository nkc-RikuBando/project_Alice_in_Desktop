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
                Debug.Log("静止");
                playerStatusManager.PlayerIsInput(false);
            }
            if (Input.GetKeyDown(KeyCode.K)) 
            {
                Debug.Log("再生");
                playerStatusManager.PlayerIsInput(true);
            }
        }



        // Playerの移動メソッド
        private void HorizontalMove()
        {
            // 移動の物理処理
            _rb.velocity = new Vector2(_inputReceivable.MoveH() * _playerStatus._Speed, _rb.velocity.y);
        }

        // Playerの向きメソッド
        private void PlayerDirection()
        {
            bool _playerDic = _inputReceivable.MoveH() == 0;

            // 入力されていない状態
            if (_playerDic)
            {
                transform.localScale = new Vector3(1f * _statusManager.ScaleMagnification,
                                                   1f * _statusManager.ScaleMagnification,
                                                   1f);

                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Dash"), false);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"), false);
            }
            // 移動状態
            else if (!_playerDic)
            {
                transform.localScale = new Vector3(_inputReceivable.MoveH() * _statusManager.ScaleMagnification,
                                                    1f * _statusManager.ScaleMagnification,
                                                    1f);

                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Dash"), true);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"), false);
            }
            // 静止状態
            else if (_playerDic && _playerStatus._GroundChecker)
            {
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Dash"), false);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"), false);
            }
        }
    }

}
