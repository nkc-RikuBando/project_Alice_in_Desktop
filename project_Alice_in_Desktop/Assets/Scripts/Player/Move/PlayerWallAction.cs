using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;
using Connector.Inputer;

namespace Player
{
    public class PlayerWallAction : MonoBehaviour
    {
        // Player�̕ǃA�N�V����

        [SerializeField] private float _fallGrabity = 1;

        private WallChecker _wallChecker;
        private IInputReceivable _inputReceivable;
        private PlayerStatus _playerStatus;
        private PlayerState _playerState;
        private CapsuleCollider2D _capCol;
        private Rigidbody2D _rb;

        private Vector2 vec;
        private bool _isWall;
        private bool _isWallJump;
        private bool _isInputJump;

        private const float VERTICAL_ANGLE = 90;


        private void Start()
        {
            _wallChecker     = GetComponent<WallChecker>();
            _inputReceivable = GetComponent<IInputReceivable>();
            _playerStatus    = GetComponent<PlayerStatus>();
            _playerState     = GetComponent<PlayerState>();
            _rb              = GetComponent<Rigidbody2D>();
            _capCol          = GetComponent<CapsuleCollider2D>();
        }

        private void Update()
        {
            JumpInput();
        }

        private void FixedUpdate()
        {
            WallSticking();
        }


        // �W�����v���͏���
        private void JumpInput()
        {
            // �ǂ͒���t������
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
            }

        }

        // �ǒ���t������
        private void WallSticking()
        {
            // �ǒ���t�����̋���
            if (_isWall)
            {
                // Player��Î~��Ԃɂ���
                _rb.velocity = Vector2.zero;
                _rb.gravityScale = 0;
                _isWallJump = true;


                // �ǃW�����v
                WallJump();


                // �ǒ���t�����̓��͏���
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

        // �ǃW�����v
        private void WallJump()
        {
            if (_isInputJump && _isWallJump || _isInputJump && _isWallJump)
            {
                float angle = (VERTICAL_ANGLE + (_playerStatus._WallJumpAngle * transform.localScale.x)) * Mathf.Deg2Rad;
                vec = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                _rb.velocity = Vector2.zero;
                _rb.AddForce(vec.normalized * _playerStatus._WallJumpPower);

                _playerStatus._InputFlgX = false;
                _isWallJump = false;
                _isWall = false;
                _isInputJump = false;

                _playerState._StateEnum = PlayerState.PlayerStateEnum.WALLSTICK;
            }
        }
    }
}
