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
        private float _jumpCount;
        private bool _flg;

        // ジャンプ可能カウント変数
        //private const float JUMP_FEASIBLE_COUNT = 0.2f;


        private void Start()
        {
            _inputReceivable = GetComponent<IInputReceivable>();
            _playerStatus    = GetComponent<PlayerStatus>();
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



        // --------- ジャンプ入力処理 ---------
        private void JumpActionInput()
        {
            // 入力フラグ
            bool _isJumpInputKey_W;
            bool _isJumpInputKey_Space;


            // 地面判定処理呼び出し
            if (_playerStatus._GroundJudge) _flg = _groundChecker.CheckIsGround(_capCol);
            else _flg = false;


            // 入力の分岐処理
            _isJumpInputKey_W     = _inputReceivable.JumpKey_W() && _flg;
            _isJumpInputKey_Space = _inputReceivable.JumpKey_Space() && _flg;


            // ジャンプ状態にする
            if (_playerStatus._InputFlgY)
            {
                if (_isJumpInputKey_W || _isJumpInputKey_Space)
                {
                    _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Jump"));
                    _jumpCount = 0f;
                    _jumpFlg = true;
                }
            }
        }



        // --------- ジャンプ処理 ---------
        private void JumpAction()
        {
            // ジャンプの物理処理
            if (_jumpFlg)
            {
                _rb.velocity = Vector2.zero;
                _rb.AddForce(Vector2.up * _playerStatus._BigJumpPower);

                _jumpFlg = false;
                _playerStatus._InputFlgY = false;
            }


            // Playerステート変更
            // 上昇状態
            if (_rb.velocity.y > 0)
            {
                // 後で追加する
            }
            // 下降状態
            else if (_rb.velocity.y < -0.2f)
            {
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), true);
                _playerStatus._InputFlgX = true;
            }


            // 着地状態
            if (_flg && _rb.velocity.y < 0)
            {
                _isLanding = true;
            }

            if (_isLanding)
            {
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), false);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);

                // 少しの間入力できない
                _jumpCount += Time.deltaTime;

                if (_jumpCount > _playerStatus.JumpFeasibleCount)
                {
                    _playerStatus._InputFlgY = true;
                    _isLanding = false;
                }
            }
        }
    }

}
