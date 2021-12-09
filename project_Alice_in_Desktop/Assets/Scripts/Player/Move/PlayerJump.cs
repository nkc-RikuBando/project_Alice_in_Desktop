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
            Debug.Log(_playerStatus._StateEnum);
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
            if (_playerStatus._GroundJudge)
            {
                _playerStatus._GroundChecker = _groundChecker.CheckIsGround(_capCol);
            } 
            else _playerStatus._GroundChecker = false;


            // 入力の分岐処理
            _isJumpInputKey_W = _inputReceivable.JumpKey_W() && _playerStatus._GroundChecker;
            _isJumpInputKey_Space = _inputReceivable.JumpKey_Space() && _playerStatus._GroundChecker;


            // ジャンプ状態にする
            if (_playerStatus._InputFlgY)
            {
                if (_isJumpInputKey_W || _isJumpInputKey_Space)
                {
                    _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Jump"));
                    _jumpFlg = true;
                    _jumpCount = 0f;
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
                _rb.AddForce(Vector2.up * _playerStatus._JumpPower);

                _playerStatus._StateEnum = PlayerStateEnum.JUMP_UP; 

                _jumpFlg = false;
                _playerStatus._InputFlgY = false;
            }

            // 着地状態
            if (_playerStatus._GroundChecker && _rb.velocity.y < 0f)
            {
                _isLanding = true;
            }

            // Playerステート変更
            if (!_isLanding)
            {
                // 上昇状態
                if (_rb.velocity.y > 0)
                {
                    // 後で追加する
                }
                // 下降状態
                else if (_rb.velocity.y < 0f)
                {
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), true);
                    _playerStatus._StateEnum = PlayerStateEnum.JUMP_DOWN;
                    _playerStatus._InputFlgX = true;
                }
            }

            if (_isLanding)
            {
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), false);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);

                _playerStatus._StateEnum = PlayerStateEnum.LANDING;

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
