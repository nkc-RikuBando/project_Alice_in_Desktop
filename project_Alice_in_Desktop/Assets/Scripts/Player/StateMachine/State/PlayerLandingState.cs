using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;

namespace PlayerState
{
    public class PlayerLandingState : MonoBehaviour, IPlayerState
    {
        // Player��Landing��ԏ���

        [SerializeField] private AudioClip _landingSE;

        public PlayerStateEnum StateType => PlayerStateEnum.LANDING;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private PlayerAnimation  _playerAnimation;
        private PlayerStatus     _playerStatus;
        private IInputReceivable _inputReceivable;
        private Rigidbody2D      _rb;
        private AudioSource      _audioSource;


        void IPlayerState.OnStart(PlayerStateEnum beforeState)
        {
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _playerStatus    ??= GetComponent<PlayerStatus>();
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _rb              ??= GetComponent<Rigidbody2D>();
            _audioSource     ??= GetComponent<AudioSource>();

            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("JumpUp"), false);
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), false);

            _playerStatus._InputFlgAction = true;
            _audioSource.PlayOneShot(_landingSE);
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


        // Player�̃X�e�[�g�ύX���\�b�h
        private void StateManager()
        {
            // �ҋ@��ԂɑJ��
            ChangeStateEvent(PlayerStateEnum.STAY);
        }

        // Player�ړ����\�b�h
        private void Dash()
        {
            int dir = 0;

            if (!_playerStatus._InputFlgX) return;

            if (_inputReceivable.MoveKey_D())      dir =  1;
            else if (_inputReceivable.MoveKey_A()) dir = -1;
            else                                   dir =  0;

            // �ړ��̕�������
            _rb.velocity = new Vector2(dir * _playerStatus._Speed, _rb.velocity.y);
        }

    }

}
