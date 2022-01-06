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
        private BoxCollider2D _boxCol;

        private float _jumpCount;
        private bool _jumpFlg;
        private bool _isLanding;

        // ジャンプ可能カウント変数
        //private const float JUMP_FEASIBLE_COUNT = 0.2f;


        private void Start()
        {
            _inputReceivable = GetComponent<IInputReceivable>();
            _playerStatus = GetComponent<PlayerStatus>();
            _groundChecker = GetComponent<GroundChecker>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _rb = GetComponent<Rigidbody2D>();
            _boxCol = GetComponent<BoxCollider2D>();
        }
        private void Update()
        {
            GroundChecker();
            JumpStateManager();
            JumpActionInput();
            Landing();
        }
        private void FixedUpdate()
        {
            JumpAction();
        }



        // 地面判定メソッド
        private bool GroundChecker()
        {
            if (_playerStatus._GroundJudge) _playerStatus._GroundChecker = _groundChecker.CheckIsGround(_boxCol);
            else _playerStatus._GroundChecker = false;

            return _playerStatus._GroundChecker;
        }

        // 入力フラグメソッド
        private bool IsJumpInput()
        {
            bool _isJumpInputKey_W;
            bool _isJumpInputKey_Space;

            _isJumpInputKey_W = _inputReceivable.JumpKey_W() && GroundChecker();
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
                    _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Jump"));
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("JumpUp"), true);
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

                _jumpCount = 0f;
                _jumpFlg = false;
                _playerStatus._InputFlgY = false;
            }
        }


        // 着地状態メソッド
        private void Landing()
        {
            // このif文の中身のせいでアニメーションバグが起きている
            // 着地した瞬間
            //if (_playerStatus._GroundChecker && _rb.velocity.y < -1f)
            //{
            //    _isLanding = true;
            //    Debug.Log("着地した！");
            //}
            //else if (!_playerStatus._GroundChecker)
            //{
            //    _isLanding = false;
            //}

            // 着地している状態
            if (_playerStatus._GroundChecker)
            {
                Debug.Log("着地状態");
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), false);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);

                JumpCount();
            }
        }

        // ジャンプができるようになるまでの計測メソッド
        private void JumpCount()
        {
            bool _jumpCountFlg = _jumpCount > _playerStatus.JumpFeasibleCount && _jumpCount < _playerStatus.JumpFeasibleCount + 0.1f;

            // 少しの間入力できない
            _jumpCount += Time.deltaTime;
            if (_jumpCountFlg)
            {
                _playerStatus._InputFlgY = true;
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("JumpUp"), false);
            }
        }

        // ジャンプ状態管理メソッド
        private void JumpStateManager()
        {
            // Playerステート変更
            // 下降状態
            if (_rb.velocity.y < -0.1f)
            {
                Debug.Log("下降状態");
                _playerStatus._InputFlgX = true;
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), true);
            }
        }
    }

}
