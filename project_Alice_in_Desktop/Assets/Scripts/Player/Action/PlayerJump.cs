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

        private IInputReceivable  _inputReceivable;
        private PlayerStatus      _playerStatus;
        private GroundChecker     _groundChecker;
        private PlayerAnimation   _playerAnimation;
        private Rigidbody2D       _rb;
        private CapsuleCollider2D _capCol;

        private float _jumpCount;
        private bool  _jumpFlg;
        private bool  _isLanding;

        // ジャンプ可能カウント変数
        //private const float JUMP_FEASIBLE_COUNT = 0.2f;


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
            GroundChecker();
            JumpActionInput();
        }
        private void FixedUpdate()
        {
            JumpAction();
            JumpStateManager();
            Landing();
        }



        // 地面判定メソッド
        private bool GroundChecker()
        {
            if (_playerStatus._GroundJudge) _playerStatus._GroundChecker = _groundChecker.CheckIsGround(_capCol);
            else _playerStatus._GroundChecker = false;

            return _playerStatus._GroundChecker;
        }

        // 入力フラグメソッド
        private bool IsJumpInput() 
        {
            bool _isJumpInputKey_W;
            bool _isJumpInputKey_Space;

            _isJumpInputKey_W 　　= _inputReceivable.JumpKey_W() && GroundChecker();
            _isJumpInputKey_Space = _inputReceivable.JumpKey_Space() && GroundChecker();

            return _isJumpInputKey_W || _isJumpInputKey_Space;
        }


        // ジャンプ入力メソッド
        private void JumpActionInput()
        {
            // ジャンプ状態にする
            if (_playerStatus._InputFlgY)
            {
                if (IsJumpInput())
                {
                    _jumpFlg = true;
                    _jumpCount = 0f;
                    _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Jump"));
                }
            }
        }

        // ジャンプアクションメソッド
        private void JumpAction()
        {
            if (_jumpFlg)
            {
                // 物理挙動
                _rb.velocity = Vector2.zero;
                _rb.AddForce(Vector2.up * _playerStatus._JumpPower);

                _jumpFlg = false;
                _playerStatus._InputFlgY = false;
            }
        }


        // 着地状態メソッド
        private void Landing()
        {
            // 着地した瞬間
            if (_playerStatus._GroundChecker && _rb.velocity.y < 0f)
            {
                _isLanding = true;
            }

            // 着地している状態
            if (_isLanding)
            {
                _playerStatus._InputFlgX = true;

                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("JumpUp"), false);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"),   false);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"),  false);

                JumpCount();
            }
        }

        // ジャンプができるようになるまでの計測メソッド
        private void JumpCount()
        {
            // 少しの間入力できない
            _jumpCount += Time.deltaTime;

            if (_jumpCount > _playerStatus.JumpFeasibleCount)
            {
                _playerStatus._InputFlgY = true;
                _isLanding = false;
            }

        }

        // ジャンプ状態管理メソッド
        private void JumpStateManager() 
        {
            // Playerステート変更
            if (!_isLanding)
            {
                // 上昇状態
                if (_rb.velocity.y > 0)
                {
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("JumpUp"), true);
                }
                // 下降状態
                else if (_rb.velocity.y < 0f)
                {
                    _playerStatus._InputFlgX = true;
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), true);
                }
            }

        }
    }

}
