using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using MyUtility;
using Player;

namespace PlayerState
{
    public class PlayerDashState : MonoBehaviour, IPlayerState
    {
        // PlayerのDash状態処理

        public PlayerStateEnum StateType => PlayerStateEnum.DASH;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private GroundChecker 　 _groundChecker;
        private PushObjChecker   _pushObjChecker;
        private PlayerStatus     _playerStatus;
        private PlayerAnimation  _playerAnimation;

        private BoxCollider2D     _boxCol;
        private CapsuleCollider2D _capCol;
        private Rigidbody2D       _rb;


        void IPlayerState.OnStart(PlayerStateEnum beforeState)
        {
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _groundChecker   ??= GetComponent<GroundChecker>();
            _pushObjChecker  ??= GetComponent<PushObjChecker>();
            _playerStatus    ??= GetComponent<PlayerStatus>();
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _boxCol          ??= GetComponent<BoxCollider2D>();
            _capCol          ??= GetComponent<CapsuleCollider2D>();
            _rb              ??= GetComponent<Rigidbody2D>();
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

            // 移動下降状態に遷移
            if (_rb.velocity.y < -1f)
            {
                ChangeStateEvent(PlayerStateEnum.DASHFALL);
            }


            // 押す状態に遷移
            if (_pushObjChecker.PushObjWidthChecker(_capCol))
            {
                ChangeStateEvent(PlayerStateEnum.PUSH);
            }


            if (!_playerStatus._InputFlgY) return;
            // 移動ジャンプ状態に遷移
            if (_inputReceivable.JumpKey() && _groundChecker.CheckIsGround(_boxCol))
            {
                ChangeStateEvent(PlayerStateEnum.DASHJUMP);
            }

        }

        // Player移動メソッド
        private void Dash()
        {
            if (!_playerStatus._InputFlgX) return;

            // 移動の物理処理
            _rb.velocity = new Vector2(_inputReceivable.MoveH() * _playerStatus._Speed, _rb.velocity.y);
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Dash"), true);
        }

    }

}
