using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerDashWallJumpState : MonoBehaviour, IPlayerState
    {
        // Player��WallDashJump��ԏ���

        public PlayerStateEnum StateType => PlayerStateEnum.DASHWALLJUMP;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus     _playerStatus;
        private PlayerAnimation  _playerAnimation;
        private GroundChecker    _groundChecker;


        private Rigidbody2D   _rb;
        private BoxCollider2D _boxCol;


        void IPlayerState.OnStart(PlayerStateEnum beforeState)
        {
            _playerStatus    ??= GetComponent<PlayerStatus>();
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _groundChecker   ??= GetComponent<GroundChecker>();
            _rb              ??= GetComponent<Rigidbody2D>();
            _boxCol          ??= GetComponent<BoxCollider2D>();

            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);

            _playerStatus._InputFlgX = false;

            // �O�ɏo�����͒n�ʔ��肵�Ȃ�
            if (_playerStatus._InsideFlg) _playerStatus._GroundJudge = true;
        }

        void IPlayerState.OnUpdate()
        {
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
            // �ǃW�����v��ԂɑJ��
            if (_inputReceivable.MoveH() == 0) 
            {
                ChangeStateEvent(PlayerStateEnum.WALLJUMP);
            }

            // �ǃW�����v�ړ��㏸�ɑJ��
            if (_rb.velocity.y > 0.1f)
            {
                ChangeStateEvent(PlayerStateEnum.DASHWALLJUMPUP);
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
    }
}
