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
        // localScale周りを要調整

        private WallChecker       _wallChecker;
        private IInputReceivable  _inputReceivable;
        private PlayerStatus      _playerStatus;
        private PlayerStatusManager _statusManager;
        private PlayerAnimation   _playerAnimation;
        private CapsuleCollider2D _capCol;
        private BoxCollider2D     _boxCol;
        private Rigidbody2D       _rb;
        private GameObject        _wallCheckObj;

        private bool  _isWall;
        private bool  _isWallJump;
        private float _jumpCount;


        // 壁ジャンプ用の垂直な角度
        private const float VERTICAL_ANGLE = 90;


        private void Start()
        {
            // 子オブジェクト取得
            _wallCheckObj = transform.Find("WallCheckCollider").gameObject;

            _wallChecker     = GetComponent<WallChecker>();
            _inputReceivable = GetComponent<IInputReceivable>();
            _playerStatus    = GetComponent<PlayerStatus>();
            _statusManager   = GetComponent<PlayerStatusManager>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _rb              = GetComponent<Rigidbody2D>();
            _capCol          = GetComponent<CapsuleCollider2D>();
            _boxCol          = _wallCheckObj.GetComponent<BoxCollider2D>();

            _boxCol.enabled = false;
        }
        private void Update()
        {
            StickInput();
            WallJumpInput();
            WallState();
        }
        private void FixedUpdate()
        {
            WallSticking();
        }



        // 壁ジャンプ入力メソッド
        private void WallJumpInput()
        {
            // 壁に張り付いている場合(張り付いてるなら下のメソッドに入れるべきでは？)
            if (_isWall)
            {
                // 壁ジャンプ状態に変更
                if (_inputReceivable.JumpKey_W() || _inputReceivable.JumpKey_Space())
                {
                    _isWallJump = true;
                }
            }
        }

        // 張り付き入力メソッド
        private void StickInput()
        {
            bool _isStick = _rb.velocity.y != 0 && _playerStatus._WallJudge && !_playerStatus._GroundChecker;
            bool _stickInput = _inputReceivable.MoveH() == transform.localScale.x;

            // 壁は張り付き入力
            if (_isStick)
            {
                if (_stickInput)
                {
                    _isWall = _wallChecker.CheckIsGround(_capCol);
                }
            }
        }

        // 壁張り付き状態メソッド
        private void WallSticking()
        {
            // 壁張り付き時の挙動
            if (_isWall)
            {
                // Playerを静止状態にする
                _rb.velocity = Vector2.zero;
                _rb.gravityScale = 0;

                _playerStatus._InputFlgY = false;
                _playerStatus._GroundJudge = false;
                _boxCol.enabled = true;

                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), true);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), false);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("JumpUp"), false);

                // 少しの間入力できない
                _jumpCount += Time.deltaTime;
                if (_jumpCount > _playerStatus.JumpFeasibleCount)
                {
                    _playerStatus._InputFlgY = true;
                }

                // 壁ジャンプ
                WallJump();
            }
        }

        // 壁ジャンプメソッド
        private void WallJump()
        {
            // 入力フラグ
            bool _isJumpInputKey_W = _isWallJump && _playerStatus._InputFlgY;
            bool _isJumpInputKey_Space = _isWallJump && _playerStatus._InputFlgY;

            if (_isJumpInputKey_W || _isJumpInputKey_Space)
            {
                // 物理挙動
                _rb.velocity = Vector2.zero;
                _rb.AddForce(JumpAngle().normalized * _playerStatus._WallJumpPower);

                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

                // フラグをfalseにする
                _jumpCount  = 0f;
                _isWall     = false;
                _isWallJump = false;
                _playerStatus._InputFlgX = false;
                

                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("JumpUp"), true);
                _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Jump"));
            }
        }

        // 壁判定関係のPlayer状態メソッド
        private void WallState()
        {
            // 張り付いている場合
            if (_isWall)
            {
                // 降下状態
                if (_rb.velocity.y < -1f)
                {
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), true);
                }

                // 壁張り付き状態で逆方向に入力した場合
                if (transform.localScale.x == 1)
                {
                    if (_inputReceivable.MoveH() != 1) _isWall = false;
                }
                else if (transform.localScale.x == -1)
                {
                    if (_inputReceivable.MoveH() != -1) _isWall = false;
                }
            }
            // 張り付いていない場合
            else
            {
                // 降下状態
                if (_rb.velocity.y < 0f)
                {
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
                    _boxCol.enabled = false;
                }

                // 地面判定できるようにする
                if(_playerStatus._insideFlg) _playerStatus._GroundJudge = true;

                // 壁に張り付いていない場合
                _rb.gravityScale = _playerStatus._Gravity;
            }
        }

        // 壁ジャンプする角度からベクトルに変換するメソッド
        private Vector2 JumpAngle()
        {
            float tempAngle = (VERTICAL_ANGLE + (_playerStatus._WallJumpAngle * transform.localScale.x)) * Mathf.Deg2Rad;

            return new Vector2(Mathf.Cos(tempAngle), Mathf.Sin(tempAngle));
        }

    }
}
