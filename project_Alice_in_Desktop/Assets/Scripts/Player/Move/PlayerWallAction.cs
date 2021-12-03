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

        [SerializeField, Tooltip("�ǂ��痎���鎞�̏d��")] private float _fallGrabity = 1;

        private WallChecker �@�@  _wallChecker;
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

        // �W�����v�\�J�E���g�ϐ�
        //private const float JUMP_FEASIBLE_COUNT = 0.2f;

        // �ǃW�����v�p�̐����Ȋp�x
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



        // --------- �W�����v���͏��� ---------
        private void JumpInput()
        {
            // �ǂ͒���t������
            if (_rb.velocity.y != 0 && _playerStatus._WallJudge)
            {
                if (_inputReceivable.MoveH() == transform.localScale.x)
                {
                    _isWall = _wallChecker.CheckIsGround(_capCol);
                }
            }


            // �ǂɒ���t���Ă���ꍇ
            if (_isWall)
            {
                _playerStatus._GroundJudge = false;

                // �ǃW�����v��ԂɕύX
                if (_inputReceivable.JumpKey_W() || _inputReceivable.JumpKey_Space())
                {
                    _isInputJump    = true;
                }

                // �~�����
                if (_rb.velocity.y < 0)
                {
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), true);
                }
            }
            // ����t���Ă��Ȃ��ꍇ
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


        // --------- �ǒ���t������ ---------
        private void WallSticking()
        {
            // �ǒ���t�����̋���
            if (_isWall)
            {
                // Player��Î~��Ԃɂ���
                _rb.velocity = Vector2.zero;

                _boxCol.enabled = true;

                // �����Ȃ��Ȃ�
                _rb.gravityScale = 0;
                _isWallJump �@�@ = true;
                _playerStatus._InputFlgY = false;

                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), true);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), false);


                // �����̊ԓ��͂ł��Ȃ�
                _jumpCount += Time.deltaTime;
                if (_jumpCount > _playerStatus.JumpFeasibleCount)
                {
                    _playerStatus._InputFlgY = true;
                }


                // �ǃW�����v
                WallJump();


                // �ǒ���t�����̓��͏���
                if (transform.localScale.x == 1)
                {
                    if (_inputReceivable.MoveH() == 0)�@�@�@ _isWall = false;
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
                // �ǂɒ���t���Ă��Ȃ��ꍇ
                _rb.gravityScale = _fallGrabity;
            }

        }


        // �ǃW�����v
        private void WallJump()
        {
            // ���̓t���O
            bool _isJumpInputKey_W     = _isInputJump && _isWallJump && _playerStatus._InputFlgY;
            bool _isJumpInputKey_Space = _isInputJump && _isWallJump && _playerStatus._InputFlgY;


            if (_isJumpInputKey_W || _isJumpInputKey_Space)
            {
                // �ǃW�����v�̕�������
                float angle = (VERTICAL_ANGLE + (_playerStatus._WallJumpAngle * transform.localScale.x)) * Mathf.Deg2Rad;
                _vec = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                transform.localScale = new Vector2(-transform.localScale.x, 1f);

                _rb.velocity = Vector2.zero;
                _rb.AddForce(_vec.normalized * _playerStatus._WallJumpPower);


                // �t���O��false�ɂ���
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
