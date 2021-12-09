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

        private IInputReceivable _inputReceivable;
        private PlayerStatus _playerStatus;
        private GroundChecker _groundChecker;
        private PlayerAnimation _playerAnimation;
        private Rigidbody2D _rb;
        private CapsuleCollider2D _capCol;

        private bool _jumpFlg;
        private bool _isLanding;
        private float _jumpCount;

        // �W�����v�\�J�E���g�ϐ�
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



        // --------- �W�����v���͏��� ---------
        private void JumpActionInput()
        {
            // ���̓t���O
            bool _isJumpInputKey_W;
            bool _isJumpInputKey_Space;


            // �n�ʔ��菈���Ăяo��
            if (_playerStatus._GroundJudge)
            {
                _playerStatus._GroundChecker = _groundChecker.CheckIsGround(_capCol);
            } 
            else _playerStatus._GroundChecker = false;


            // ���͂̕��򏈗�
            _isJumpInputKey_W = _inputReceivable.JumpKey_W() && _playerStatus._GroundChecker;
            _isJumpInputKey_Space = _inputReceivable.JumpKey_Space() && _playerStatus._GroundChecker;


            // �W�����v��Ԃɂ���
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



        // --------- �W�����v���� ---------
        private void JumpAction()
        {
            // �W�����v�̕�������
            if (_jumpFlg)
            {
                _rb.velocity = Vector2.zero;
                _rb.AddForce(Vector2.up * _playerStatus._JumpPower);

                _playerStatus._StateEnum = PlayerStateEnum.JUMP_UP; 

                _jumpFlg = false;
                _playerStatus._InputFlgY = false;
            }

            // ���n���
            if (_playerStatus._GroundChecker && _rb.velocity.y < 0f)
            {
                _isLanding = true;
            }

            // Player�X�e�[�g�ύX
            if (!_isLanding)
            {
                // �㏸���
                if (_rb.velocity.y > 0)
                {
                    // ��Œǉ�����
                }
                // ���~���
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

                // �����̊ԓ��͂ł��Ȃ�
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
