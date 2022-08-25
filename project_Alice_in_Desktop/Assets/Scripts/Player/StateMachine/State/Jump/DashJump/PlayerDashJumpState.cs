using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerDashJumpState : MonoBehaviour, IPlayerState
    {
        // Player��DashJump��ԏ���

        [SerializeField] private AudioClip _jumpSE;

        public PlayerStateEnum StateType => PlayerStateEnum.DASHJUMP;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus     _playerStatus;
        private PlayerAnimation  _playerAnimation;
        private GroundChecker    _groundChecker;

        private Rigidbody2D   _rb;
        private BoxCollider2D _boxCol;
        private AudioSource   _audioSource;


        void IPlayerState.OnStart(PlayerStateEnum beforeState)
        {
            _playerStatus    ??= GetComponent<PlayerStatus>();
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _groundChecker   ??= GetComponent<GroundChecker>();
            _rb              ??= GetComponent<Rigidbody2D>();
            _boxCol          ??= GetComponent<BoxCollider2D>();
            _audioSource     ??= GetComponent<AudioSource>();

            _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Jump"));

            // ��������
            JumpAction();
        }

        void IPlayerState.OnUpdate()
        {
            Dash();
            StateManager();
        }

        void IPlayerState.OnFixedUpdate()
        {
        }
        void IPlayerState.OnEnd(PlayerStateEnum nextState)
        {
        }


        // Player�X�e�[�g�ύX���\�b�h
        private void StateManager()
        {
            // �ړ��W�����v��ԂɑJ��
            if (_rb.velocity.y > 0.1f)
            {
                ChangeStateEvent(PlayerStateEnum.DASHJUMPUP);
            }
            // �W�����v��ԂɑJ��
            else if (_rb.velocity.y > 0.1f && _inputReceivable.MoveH() == 0)
            {
                ChangeStateEvent(PlayerStateEnum.JUMPUP);
            }

            // �n�ʔ��� �� ���n��ԂɑJ��
            if (_groundChecker.CheckIsGround(_boxCol))
            {
                // ���܂�o�O���p�A�j���[�^�[�ϐ�
                _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Exit"));
                _playerStatus._InputFlgX = true;
                ChangeStateEvent(PlayerStateEnum.LANDING);
            }

        }

        // �W�����v�A�N�V�������\�b�h
        private void JumpAction()
        {
            // ��������
            _rb.velocity = Vector2.zero;
            _rb.AddForce(Vector2.up * _playerStatus._JumpPower);
            _audioSource.PlayOneShot(_jumpSE);
        }


        // Player�ړ����\�b�h
        private void Dash()
        {
            if (!_playerStatus._InputFlgX) return;

            // �ړ��̕�������
            _rb.velocity = new Vector2(_inputReceivable.MoveH() * _playerStatus._Speed, _rb.velocity.y);
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Dash"), true);
        }


    }

}
