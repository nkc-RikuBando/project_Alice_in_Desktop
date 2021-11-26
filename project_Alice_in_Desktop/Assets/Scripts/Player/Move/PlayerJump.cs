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
        private PlayerState _playerState;
        private GroundChecker _groundChecker;
        private Rigidbody2D _rb;
        private CapsuleCollider2D _capCol;

        private bool _jumpFlg;
        private float _frame;


        private void Start()
        {
            _inputReceivable = GetComponent<IInputReceivable>();
            _playerStatus    = GetComponent<PlayerStatus>();
            _playerState     = GetComponent<PlayerState>();
            _groundChecker   = GetComponent<GroundChecker>();
            _rb              = GetComponent<Rigidbody2D>();
            _capCol = GetComponent<CapsuleCollider2D>();
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

            if (_inputReceivable.JumpKey_W() && _groundChecker.CheckIsGround(_capCol) || _inputReceivable.JumpKey_Space() && _groundChecker.CheckIsGround(_capCol))
            {
                _playerState._StateEnum = PlayerState.PlayerStateEnum.JUMP_PREVIOUS;
                _jumpFlg = true;

            }
        }

        // ジャンプ処理
        private void JumpAction()
        {
            bool _isLanding;

            if (_jumpFlg)
            {
                _frame++;

                // 小ジャンプ
                if (_frame > _playerStatus._BigJumpFrame)
                {
                    _rb.velocity = Vector2.zero;
                    _rb.AddForce(Vector2.up * _playerStatus._BigJumpPower);
                    _frame = 0f;
                    _jumpFlg = false;
                }
                // 大ジャンプ
                else if (_frame < _playerStatus._BigJumpFrame && _inputReceivable.JumpKey_W() || _frame < _playerStatus._BigJumpFrame && _inputReceivable.JumpKey_Space())
                {
                    _rb.velocity = Vector2.zero;
                    _rb.AddForce(Vector2.up * _playerStatus._SmallJumpPower);
                    _frame = 0f;
                    _jumpFlg = false;
                }
            }


            // Playerステート変更
            if (_rb.velocity.y > 1)
            {
                //_playerState._StateEnum = PlayerState.PlayerStateEnum.JUMP_UP;
            }
            else if (_rb.velocity.y < -1)
            {
                _playerState._StateEnum = PlayerState.PlayerStateEnum.JUMP_DOWN;
                _playerStatus._InputFlgX = true;
            }

            if (_groundChecker.CheckIsGround(_capCol) == true)
            {
                _isLanding = true;

                if (_isLanding)
                {
                    //_playerState._StateEnum = PlayerState.PlayerStateEnum.LANDING;
                    Debug.Log("着地");
                    _isLanding = false;
                }
            }


        }
    }

}
