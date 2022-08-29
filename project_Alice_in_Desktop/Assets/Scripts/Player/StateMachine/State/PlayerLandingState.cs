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
        // PlayerのLanding状態処理

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


        // Playerのステート変更メソッド
        private void StateManager()
        {
            // 待機状態に遷移
            ChangeStateEvent(PlayerStateEnum.STAY);
        }

        // Player移動メソッド
        private void Dash()
        {
            int dir = 0;

            if (!_playerStatus._InputFlgX) return;

            if (_inputReceivable.MoveKey_D())      dir =  1;
            else if (_inputReceivable.MoveKey_A()) dir = -1;
            else                                   dir =  0;

            // 移動の物理処理
            _rb.velocity = new Vector2(dir * _playerStatus._Speed, _rb.velocity.y);
        }

    }

}
