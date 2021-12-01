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

        [SerializeField] private float _fallGrabity = 1;

        private WallChecker _wallChecker;
        private IInputReceivable _inputReceivable;
        private PlayerStatus _playerStatus;
        private PlayerAnimation _playerAnimation;
        private CapsuleCollider2D _capCol;
        private Rigidbody2D _rb;

        private Vector2 vec;
        private bool _isWall;
        private bool _isWallJump;
        private bool _isInputJump;
        private float _timeCount;


        private const float VERTICAL_ANGLE = 90;


        private void Start()
        {
            _wallChecker = GetComponent<WallChecker>();
            _inputReceivable = GetComponent<IInputReceivable>();
            _playerStatus = GetComponent<PlayerStatus>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _rb = GetComponent<Rigidbody2D>();
            _capCol = GetComponent<CapsuleCollider2D>();
        }

        private void Update()
        {
            JumpInput();
        }
        private void FixedUpdate()
        {
            WallSticking();
        }


        // ジャンプ入力処理
        private void JumpInput()
        {
            // 壁は張り付き入力
            if (_rb.velocity.y != 0)
            {
                if (_inputReceivable.MoveH() == transform.localScale.x)
                {
                    _isWall = _wallChecker.CheckIsGround(_capCol);
                }
            }

            if (_isWall)
            {
                if (_inputReceivable.JumpKey_W() || _inputReceivable.JumpKey_Space())
                {
                    _isInputJump = true;
                }

                if (_rb.velocity.y < 0)
                {
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), true);
                }

            }
            else
            {
                if (_rb.velocity.y < 0)
                {
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), true);
                }

            }

        }

        // 壁張り付き処理
        private void WallSticking()
        {
            // 壁張り付き時の挙動
            if (_isWall)
            {
                // Playerを静止状態にする
                _rb.velocity = Vector2.zero;
                _rb.gravityScale = 0;
                _isWallJump = true;
                _playerStatus._InputFlgY = false;

                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), true);

                _timeCount += Time.deltaTime;
                if (_timeCount > 0.2f)
                {
                    _playerStatus._InputFlgY = true;
                }


                // 壁ジャンプ
                WallJump();



                // 壁張り付き時の入力処理
                if (transform.localScale.x == 1)
                {
                    if (_inputReceivable.MoveH() == 0)
                    {
                        _isWall = false;
                    }
                    else if (_inputReceivable.MoveH() == -1)
                    {
                        _isWall = false;
                    }
                }
                else if (transform.localScale.x == -1)
                {
                    if (_inputReceivable.MoveH() == 0)
                    {
                        _isWall = false;
                    }
                    else if (_inputReceivable.MoveH() == 1)
                    {
                        _isWall = false;
                    }
                }
            }
            else
            {
                _rb.gravityScale = _fallGrabity;
            }

        }

        // 壁ジャンプ
        private void WallJump()
        {
            if (_isInputJump && _isWallJump && _playerStatus._InputFlgY || _isInputJump && _isWallJump && _playerStatus._InputFlgY)
            {
                float angle = (VERTICAL_ANGLE + (_playerStatus._WallJumpAngle * transform.localScale.x)) * Mathf.Deg2Rad;
                vec = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                transform.localScale = new Vector2(-transform.localScale.x, 1f);

                _rb.velocity = Vector2.zero;
                _rb.AddForce(vec.normalized * _playerStatus._WallJumpPower);

                _playerStatus._InputFlgX = false;
                _isWallJump = false;
                _isWall = false;
                _isInputJump = false;
                _timeCount = 0f;

                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
                _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Jump"));


            }

        }
    }
}
