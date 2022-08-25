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
        // PlayerのStay状態処理

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

        // Playerのステート変更メソッド
        private void StateManager()
        {
            // 移動状態に遷移
            if (_playerStatus._InputFlgX)
            {
                if (_inputReceivable.MoveH() != 0 && _groundChecker.CheckIsGround(_boxCol))
                {
                    ChangeStateEvent(PlayerStateEnum.DASH);
                }
            }

            // ジャンプ状態に遷移
            if (_playerStatus._InputFlgY)
            {
                if (_inputReceivable.JumpKey() && _groundChecker.CheckIsGround(_boxCol))
                {
                    ChangeStateEvent(PlayerStateEnum.JUMP);
                }
            }

            // 下降状態に遷移
            if (_rb.velocity.y < -1f)
            {
                ChangeStateEvent(PlayerStateEnum.FALL);
            }

            // 木箱の上にいる時はVelocityを0にする
            if (_pushObjChecker.PushObjOnChecker(_capCol)) _rb.velocity = Vector2.zero;
        }

        // Animation初期化メソッド
        private void AnimationReset()
        {
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Dash"),  false);
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"),  false);
        }

        // Player移動メソッド
        private void Dash()
        {
            if (!_playerStatus._InputFlgX) return;

            // 移動の物理処理
            _rb.velocity = new Vector2(_inputReceivable.MoveH() * _playerStatus._Speed, _rb.velocity.y);
        }

    }

}
