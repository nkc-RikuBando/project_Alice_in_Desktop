using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerPushState : MonoBehaviour, IPlayerState
    {
        // PlayerのPush状態処理

        public PlayerStateEnum StateType => PlayerStateEnum.PUSH;
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

            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"), true);
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
            if (_inputReceivable.MoveH() == 0 && _groundChecker.CheckIsGround(_boxCol))
            {
                ChangeStateEvent(PlayerStateEnum.STAY);
            }

            // ジャンプ状態に遷移
            if (_inputReceivable.JumpKey() && _groundChecker.CheckIsGround(_boxCol))
            {
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"), false);
                ChangeStateEvent(PlayerStateEnum.JUMP);
            }

            // 下降状態に遷移
            if (_rb.velocity.y < -1f)
            {
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"), false);
                ChangeStateEvent(PlayerStateEnum.FALL);
            }

            // 箱に触れている場合
            if (_pushObjChecker.PushObjWidthChecker(_capCol)) return;


            // 移動状態に遷移
            if (_inputReceivable.MoveH() != 0 && _groundChecker.CheckIsGround(_boxCol))
            {
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"), false);
                ChangeStateEvent(PlayerStateEnum.DASH);
            }
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
