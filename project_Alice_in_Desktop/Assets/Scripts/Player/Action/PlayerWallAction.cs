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
        // localScale�����v����

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


        // �ǃW�����v�p�̐����Ȋp�x
        private const float VERTICAL_ANGLE = 90;


        private void Start()
        {
            // �q�I�u�W�F�N�g�擾
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



        // �ǃW�����v���̓��\�b�h
        private void WallJumpInput()
        {
            // �ǂɒ���t���Ă���ꍇ(����t���Ă�Ȃ牺�̃��\�b�h�ɓ����ׂ��ł́H)
            if (_isWall)
            {
                // �ǃW�����v��ԂɕύX
                if (_inputReceivable.JumpKey_W() || _inputReceivable.JumpKey_Space())
                {
                    _isWallJump = true;
                }
            }
        }

        // ����t�����̓��\�b�h
        private void StickInput()
        {
            bool _isStick = _rb.velocity.y != 0 && _playerStatus._WallJudge && !_playerStatus._GroundChecker;
            bool _stickInput = _inputReceivable.MoveH() == transform.localScale.x;

            // �ǂ͒���t������
            if (_isStick)
            {
                if (_stickInput)
                {
                    _isWall = _wallChecker.CheckIsGround(_capCol);
                }
            }
        }

        // �ǒ���t����ԃ��\�b�h
        private void WallSticking()
        {
            // �ǒ���t�����̋���
            if (_isWall)
            {
                // Player��Î~��Ԃɂ���
                _rb.velocity = Vector2.zero;
                _rb.gravityScale = 0;

                _playerStatus._InputFlgY = false;
                _playerStatus._GroundJudge = false;
                _boxCol.enabled = true;

                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), true);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), false);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("JumpUp"), false);

                // �����̊ԓ��͂ł��Ȃ�
                _jumpCount += Time.deltaTime;
                if (_jumpCount > _playerStatus.JumpFeasibleCount)
                {
                    _playerStatus._InputFlgY = true;
                }

                // �ǃW�����v
                WallJump();
            }
        }

        // �ǃW�����v���\�b�h
        private void WallJump()
        {
            // ���̓t���O
            bool _isJumpInputKey_W = _isWallJump && _playerStatus._InputFlgY;
            bool _isJumpInputKey_Space = _isWallJump && _playerStatus._InputFlgY;

            if (_isJumpInputKey_W || _isJumpInputKey_Space)
            {
                // ��������
                _rb.velocity = Vector2.zero;
                _rb.AddForce(JumpAngle().normalized * _playerStatus._WallJumpPower);

                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

                // �t���O��false�ɂ���
                _jumpCount  = 0f;
                _isWall     = false;
                _isWallJump = false;
                _playerStatus._InputFlgX = false;
                

                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("JumpUp"), true);
                _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Jump"));
            }
        }

        // �ǔ���֌W��Player��ԃ��\�b�h
        private void WallState()
        {
            // ����t���Ă���ꍇ
            if (_isWall)
            {
                // �~�����
                if (_rb.velocity.y < -1f)
                {
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), true);
                }

                // �ǒ���t����Ԃŋt�����ɓ��͂����ꍇ
                if (transform.localScale.x == 1)
                {
                    if (_inputReceivable.MoveH() != 1) _isWall = false;
                }
                else if (transform.localScale.x == -1)
                {
                    if (_inputReceivable.MoveH() != -1) _isWall = false;
                }
            }
            // ����t���Ă��Ȃ��ꍇ
            else
            {
                // �~�����
                if (_rb.velocity.y < 0f)
                {
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
                    _boxCol.enabled = false;
                }

                // �n�ʔ���ł���悤�ɂ���
                if(_playerStatus._insideFlg) _playerStatus._GroundJudge = true;

                // �ǂɒ���t���Ă��Ȃ��ꍇ
                _rb.gravityScale = _playerStatus._Gravity;
            }
        }

        // �ǃW�����v����p�x����x�N�g���ɕϊ����郁�\�b�h
        private Vector2 JumpAngle()
        {
            float tempAngle = (VERTICAL_ANGLE + (_playerStatus._WallJumpAngle * transform.localScale.x)) * Mathf.Deg2Rad;

            return new Vector2(Mathf.Cos(tempAngle), Mathf.Sin(tempAngle));
        }

    }
}
