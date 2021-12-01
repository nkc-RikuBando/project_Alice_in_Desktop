using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using MyUtility;

namespace Player
{
    public class PlayerJump : MonoBehaviour
    {
        // Playerのジャンプ処理

        private IInputReceivable _inputReceivable;
        private PlayerStatus _playerStatus;
        private GroundChecker _groundChecker;
        private PlayerAnimation _playerAnimation;
        private Rigidbody2D _rb;
        private CapsuleCollider2D _capCol;

        private bool _jumpFlg;
        private bool _isLanding;
        private float _frameCount;

        private void Start()
        {
            _inputReceivable = GetComponent<IInputReceivable>();
            _playerStatus 　 = GetComponent<PlayerStatus>();
            _groundChecker   = GetComponent<GroundChecker>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _rb              = GetComponent<Rigidbody2D>();
            _capCol          = GetComponent<CapsuleCollider2D>();
        }


        private void Update()
        {
            JumpActionInput();
        }
        private void FixedUpdate()
        {
            JumpAction();
        }


        // ジャンプ入力処理
        private void JumpActionInput()
        {
            _groundChecker.CheckIsGround(_capCol);

            if (_playerStatus._InputFlgY)
            {
                if (_inputReceivable.JumpKey_W() && _groundChecker.CheckIsGround(_capCol) || _inputReceivable.JumpKey_Space() && _groundChecker.CheckIsGround(_capCol))
                {
                    _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Jump"));
                    _frameCount = 0f;
                    _jumpFlg = true;
                }
            }
        }

        // ジャンプ処理
        private void JumpAction()
        {
            if (_jumpFlg)
            {
                _rb.velocity = Vector2.zero;
                _rb.AddForce(Vector2.up * _playerStatus._BigJumpPower);
                _jumpFlg = false;
                _playerStatus._InputFlgY = false;
            }


            // Playerステート変更
            if (_rb.velocity.y > 0)
            {
            }
            else if (_rb.velocity.y < 0)
            {
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), true);
                _playerStatus._InputFlgX = true;
            }


            // 着地状態
            if (_groundChecker.CheckIsGround(_capCol) && _rb.velocity.y < 0)
            {
                _isLanding = true;
            }

            if (_isLanding)
            {
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), false);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);

                _frameCount += Time.deltaTime;

                if (_frameCount > 0.2f)
                {
                    _playerStatus._InputFlgY = true;
                    _isLanding = false;
                }
            }



        }
    }

}
