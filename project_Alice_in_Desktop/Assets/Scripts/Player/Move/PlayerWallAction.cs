using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;
using Connector.Inputer;

namespace Player
{
    public class PlayerWallAction : MonoBehaviour
    {
        // Playerの壁アクション

        [SerializeField, Tooltip("壁から落ちる時の重力")] private float _fallGrabity = 1;

        private WallChecker 　　  _wallChecker;
        private IInputReceivable  _inputReceivable;
        private PlayerStatus      _playerStatus;
        private PlayerAnimation   _playerAnimation;
        private CapsuleCollider2D _capCol;
        private BoxCollider2D     _boxCol;
        private Rigidbody2D       _rb;

        private Vector2 _vec;
        private bool    _isWall;
        private bool    _isWallJump;
        private bool    _isInputJump;
        private float   _jumpCount;

        // ジャンプ可能カウント変数
        //private const float JUMP_FEASIBLE_COUNT = 0.2f;

        // 壁ジャンプ用の垂直な角度
        private const float VERTICAL_ANGLE = 90;


        private void Start()
        {
            _wallChecker     = GetComponent<WallChecker>();
            _inputReceivable = GetComponent<IInputReceivable>();
            _playerStatus    = GetComponent<PlayerStatus>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _rb              = GetComponent<Rigidbody2D>();
            _capCol          = GetComponent<CapsuleCollider2D>();
            _boxCol          = GetComponent<BoxCollider2D>();
        }
        private void Update()
        {
            JumpInput();
        }
        private void FixedUpdate()
        {
            WallSticking();
        }



        // --------- ジャンプ入力処理 ---------
        private void JumpInput()
        {
            // 壁は張り付き入力
            if (_rb.velocity.y != 0 && _playerStatus._WallJudge)
            {
                if (_inputReceivable.MoveH() == transform.localScale.x)
                {
                    _isWall = _wallChecker.CheckIsGround(_capCol);
                }
            }


            // 壁に張り付いている場合
            if (_isWall)
            {
                _playerStatus._GroundJudge = false;

                // 壁ジャンプ状態に変更
                if (_inputReceivable.JumpKey_W() || _inputReceivable.JumpKey_Space())
                {
                    _isInputJump    = true;
                }

                // 降下状態
                if (_rb.velocity.y < 0)
                {
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), true);
                }
            }
            // 張り付いていない場合
            else
            {
                if (_rb.velocity.y < 0)
                {
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), true);
                    _boxCol.enabled = false;
                }

                _playerStatus._GroundJudge = true;
            }
        }


        // --------- 壁張り付き処理 ---------
        private void WallSticking()
        {
            // 壁張り付き時の挙動
            if (_isWall)
            {
                // Playerを静止状態にする
                _rb.velocity = Vector2.zero;

                _boxCol.enabled = true;

                // 動けなくなる
                _rb.gravityScale = 0;
                _isWallJump 　　 = true;
                _playerStatus._InputFlgY = false;

                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), true);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), false);


                // 少しの間入力できない
                _jumpCount += Time.deltaTime;
                if (_jumpCount > _playerStatus.JumpFeasibleCount)
                {
                    _playerStatus._InputFlgY = true;
                }


                // 壁ジャンプ
                WallJump();


                // 壁張り付き時の入力処理
                if (transform.localScale.x == 1)
                {
                    if (_inputReceivable.MoveH() == 0)　　　 _isWall = false;
                    else if (_inputReceivable.MoveH() == -1) _isWall = false;
                }
                else if (transform.localScale.x == -1)
                {
                    if (_inputReceivable.MoveH() == 0)      _isWall = false;
                    else if (_inputReceivable.MoveH() == 1) _isWall = false;
                }
            }
            else
            {
                // 壁に張り付いていない場合
                _rb.gravityScale = _fallGrabity;
            }

        }


        // 壁ジャンプ
        private void WallJump()
        {
            // 入力フラグ
            bool _isJumpInputKey_W     = _isInputJump && _isWallJump && _playerStatus._InputFlgY;
            bool _isJumpInputKey_Space = _isInputJump && _isWallJump && _playerStatus._InputFlgY;


            if (_isJumpInputKey_W || _isJumpInputKey_Space)
            {
                // 壁ジャンプの物理処理
                float angle = (VERTICAL_ANGLE + (_playerStatus._WallJumpAngle * transform.localScale.x)) * Mathf.Deg2Rad;
                _vec = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                transform.localScale = new Vector2(-transform.localScale.x, 1f);

                _rb.velocity = Vector2.zero;
                _rb.AddForce(_vec.normalized * _playerStatus._WallJumpPower);


                // フラグをfalseにする
                _playerStatus._InputFlgX = false;
                _isWallJump  = false;
                _isWall      = false;
                _isInputJump = false;
                _jumpCount   = 0f;

                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
                _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Jump"));


            }

        }
    }
}
