using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerStayState : MonoBehaviour, IPlayerState
    {
        // Player��Stay��ԏ���

        public PlayerStateEnum StateType => PlayerStateEnum.STAY;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private GroundChecker    _groundChecker;
        private PushObjChecker   _pushObjChecker;
        private PlayerAnimation  _playerAnimation;
        private PlayerStatus     _playerStatus;

        private BoxCollider2D     _boxCol;
        private CapsuleCollider2D _capCol;
        private Rigidbody2D       _rb;

        void IPlayerState.OnStart(PlayerStateEnum beforeState)
        {
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _playerStatus    ??= GetComponent<PlayerStatus>();
            _groundChecker   ??= GetComponent<GroundChecker>();
            _pushObjChecker  ??= GetComponent<PushObjChecker>();
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _boxCol          ??= GetComponent<BoxCollider2D>();
            _capCol          ??= GetComponent<CapsuleCollider2D>();
            _rb              ??= GetComponent<Rigidbody2D>();
        }

        void IPlayerState.OnUpdate()
        {
            AnimationReset();
            Dash();
            StateManager();
        }

        void IPlayerState.OnFixedUpdate()
        {

        }

        void IPlayerState.OnEnd(PlayerStateEnum nextState)
        {
        }

        // Player�̃X�e�[�g�ύX���\�b�h
        private void StateManager()
        {
            // �ړ���ԂɑJ��
            if (_playerStatus._InputFlgX)
            {
                if (_inputReceivable.MoveH() != 0 && _groundChecker.CheckIsGround(_boxCol))
                {
                    ChangeStateEvent(PlayerStateEnum.DASH);
                }
            }

            // �W�����v��ԂɑJ��
            if (_playerStatus._InputFlgY)
            {
                if (_inputReceivable.JumpKey() && _groundChecker.CheckIsGround(_boxCol))
                {
                    ChangeStateEvent(PlayerStateEnum.JUMP);
                }
            }

            // ���~��ԂɑJ��
            if (_rb.velocity.y < -1f)
            {
                ChangeStateEvent(PlayerStateEnum.FALL);
            }

            // �ؔ��̏�ɂ��鎞��Velocity��0�ɂ���
            if (_pushObjChecker.PushObjOnChecker(_capCol)) _rb.velocity = Vector2.zero;
        }

        // Animation���������\�b�h
        private void AnimationReset()
        {
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Dash"),  false);
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"),  false);
        }

        // Player�ړ����\�b�h
        private void Dash()
        {
            if (!_playerStatus._InputFlgX) return;

            // �ړ��̕�������
            _rb.velocity = new Vector2(_inputReceivable.MoveH() * _playerStatus._Speed, _rb.velocity.y);
        }

    }

}
