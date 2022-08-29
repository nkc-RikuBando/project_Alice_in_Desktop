using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerDashJumpUpState : MonoBehaviour, IPlayerState
    {
        // PlayerのDashJumpUp状態処理

        public PlayerStateEnum StateType => PlayerStateEnum.DASHJUMPUP;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus     _playerStatus;
        private PlayerAnimation  _playerAnimation;
        private GroundChecker    _groundChecker;
        private WallChecker      _wallChecker;

        private Rigidbody2D       _rb;
        private BoxCollider2D     _boxCol;
        private CapsuleCollider2D _capCol;


        void IPlayerState.OnStart(PlayerStateEnum beforeState)
        {
            _playerStatus    ??= GetComponent<PlayerStatus>();
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _groundChecker   ??= GetComponent<GroundChecker>();
            _wallChecker     ??= GetComponent<WallChecker>();
            _rb              ??= GetComponent<Rigidbody2D>();
            _boxCol          ??= GetComponent<BoxCollider2D>();
            _capCol          ??= GetComponent<CapsuleCollider2D>();

            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("JumpUp"), true);
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


        // Playerステート変更メソッド
        private void StateManager()
        {
            // 上昇状態に遷移
            if (_inputReceivable.MoveH() == 0)
            {
                ChangeStateEvent(PlayerStateEnum.JUMPUP);
            }

            // 移動下降状態に遷移
            if (_rb.velocity.y < -1f)
            {
                ChangeStateEvent(PlayerStateEnum.DASHFALL);
            }

            // 着地状態に遷移
            if (_groundChecker.CheckIsGround(_boxCol))
            {
                ChangeStateEvent(PlayerStateEnum.LANDING);
            }

            if (_groundChecker.CheckIsGround(_boxCol)) return;

            // 壁張り付き状態に遷移
            if (_wallChecker.CheckIsWall(_capCol))
            {
                ChangeStateEvent(PlayerStateEnum.WALLSTICK);
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
