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
        private bool _flg;

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
            if (_playerStatus._GroundJudge) _flg = _groundChecker.CheckIsGround(_capCol);
            else _flg = false;


            // ���͂̕��򏈗�
            _isJumpInputKey_W     = _inputReceivable.JumpKey_W() && _flg;
            _isJumpInputKey_Space = _inputReceivable.JumpKey_Space() && _flg;


            // �W�����v��Ԃɂ���
            if (_playerStatus._InputFlgY)
            {
                if (_isJumpInputKey_W || _isJumpInputKey_Space)
                {
                    _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Jump"));
                    _jumpCount = 0f;
                    _jumpFlg = true;
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
                _rb.AddForce(Vector2.up * _playerStatus._BigJumpPower);

                _jumpFlg = false;
                _playerStatus._InputFlgY = false;
            }


            // Player�X�e�[�g�ύX
            // �㏸���
            if (_rb.velocity.y > 0)
            {
                // ��Œǉ�����
            }
            // ���~���
            else if (_rb.velocity.y < -0.2f)
            {
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), true);
                _playerStatus._InputFlgX = true;
            }


            // ���n���
            if (_flg && _rb.velocity.y < 0)
            {
                _isLanding = true;
            }

            if (_isLanding)
            {
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), false);
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);

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
