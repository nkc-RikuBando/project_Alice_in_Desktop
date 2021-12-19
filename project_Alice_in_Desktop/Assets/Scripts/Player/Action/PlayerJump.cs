using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using MyUtility;

namespace Player
{
    public class PlayerJump : MonoBehaviour
    {
        // Player�̃W�����v����

        private IInputReceivable  _inputReceivable;
        private PlayerStatus      _playerStatus;
        private GroundChecker     _groundChecker;
        private PlayerAnimation   _playerAnimation;
        private Rigidbody2D       _rb;
        private CapsuleCollider2D _capCol;

        private float _jumpCount;
        private bool  _jumpFlg;
        private bool  _isLanding;

        // �W�����v�\�J�E���g�ϐ�
        //private const float JUMP_FEASIBLE_COUNT = 0.2f;


        private void Start()
        {
            _inputReceivable = GetComponent<IInputReceivable>();
            _playerStatus �@ = GetComponent<PlayerStatus>();
            _groundChecker   = GetComponent<GroundChecker>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _rb              = GetComponent<Rigidbody2D>();
            _capCol          = GetComponent<CapsuleCollider2D>();
        }
        private void Update()
        {
            GroundChecker();
            JumpActionInput();
        }
        private void FixedUpdate()
        {
            JumpAction();
            JumpStateManager();
            Landing();
        }



        // �n�ʔ��胁�\�b�h
        private bool GroundChecker()
        {
            if (_playerStatus._GroundJudge) _playerStatus._GroundChecker = _groundChecker.CheckIsGround(_capCol);
            else _playerStatus._GroundChecker = false;

            return _playerStatus._GroundChecker;
        }

        // ���̓t���O���\�b�h
        private bool IsJumpInput() 
        {
            bool _isJumpInputKey_W;
            bool _isJumpInputKey_Space;

            _isJumpInputKey_W �@�@= _inputReceivable.JumpKey_W() && GroundChecker();
            _isJumpInputKey_Space = _inputReceivable.JumpKey_Space() && GroundChecker();

            return _isJumpInputKey_W || _isJumpInputKey_Space;
        }


        // �W�����v���̓��\�b�h
        private void JumpActionInput()
        {
            // �W�����v��Ԃɂ���
            if (_playerStatus._InputFlgY)
            {
                if (IsJumpInput())
                {
                    _jumpFlg = true;
                    _jumpCount = 0f;
                    _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Jump"));
                }
            }
        }

        // �W�����v�A�N�V�������\�b�h
        private void JumpAction()
        {
            if (_jumpFlg)
            {
                // ��������
                _rb.velocity = Vector2.zero;
                _rb.AddForce(Vector2.up * _playerStatus._JumpPower);

                _jumpFlg = false;
                _playerStatus._InputFlgY = false;
            }
        }


        // ���n��ԃ��\�b�h
        private void Landing()
        {
            // ���n�����u��
            if (_playerStatus._GroundChecker && _rb.velocity.y < 0f)
            {
                _isLanding = true;
            }

            // ���n���Ă�����
            if (_isLanding)
            {
                _playerStatus._InputFlgX = true;

                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("JumpUp"), false);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"),   false);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"),  false);

                JumpCount();
            }
        }

        // �W�����v���ł���悤�ɂȂ�܂ł̌v�����\�b�h
        private void JumpCount()
        {
            // �����̊ԓ��͂ł��Ȃ�
            _jumpCount += Time.deltaTime;

            if (_jumpCount > _playerStatus.JumpFeasibleCount)
            {
                _playerStatus._InputFlgY = true;
                _isLanding = false;
            }

        }

        // �W�����v��ԊǗ����\�b�h
        private void JumpStateManager() 
        {
            // Player�X�e�[�g�ύX
            if (!_isLanding)
            {
                // �㏸���
                if (_rb.velocity.y > 0)
                {
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("JumpUp"), true);
                }
                // ���~���
                else if (_rb.velocity.y < 0f)
                {
                    _playerStatus._InputFlgX = true;
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), true);
                }
            }

        }
    }

}
