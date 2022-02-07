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
        // Playerが実装するの！？
        // PlayerのStay状態処理

        public PlayerStateEnum StateType => PlayerStateEnum.DASH;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private GroundChecker 　 _groundChecker;
        private PlayerStatus     _playerStatus;
        private PlayerAnimation  _playerAnimation;

        private BoxCollider2D _boxCol;
        private Rigidbody2D   _rb;


        void IPlayerState.OnStart(PlayerStateEnum beforeState, PlayerCore player)
        {
            _inputReceivable = GetComponent<IInputReceivable>();
            _groundChecker   = GetComponent<GroundChecker>();
            _playerStatus    = GetComponent<PlayerStatus>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _boxCol          = GetComponent<BoxCollider2D>();
            _rb              = GetComponent<Rigidbody2D>();
        }

        void IPlayerState.OnUpdate(PlayerCore player)
        {
            Debug.Log(StateType);
            StateManager();
            Dash();
        }

        void IPlayerState.OnFixedUpdate(PlayerCore player)
        {
        }

        void IPlayerState.OnEnd(PlayerStateEnum nextState, PlayerCore player)
        {
        }

        // Playerのステート変更メソッド
        private void StateManager()
        {
            if (_inputReceivable.MoveH() == 0 && _groundChecker.CheckIsGround(_boxCol))
            {
                ChangeStateEvent(PlayerStateEnum.STAY);
            }

            if (_inputReceivable.JumpKey_W() && _groundChecker.CheckIsGround(_boxCol) || _inputReceivable.JumpKey_Space() && _groundChecker.CheckIsGround(_boxCol))
            {
                ChangeStateEvent(PlayerStateEnum.JUMP);
            }

            if (_rb.velocity.y > 0.1f) 
            {
                ChangeStateEvent(PlayerStateEnum.JUMPUP);

            }
            else if(_rb.velocity.y < -0.1f) 
            {
                ChangeStateEvent(PlayerStateEnum.FALL);
            }
        }

        // Player移動メソッド
        private void Dash()
        {
            // 移動の物理処理
            _rb.velocity = new Vector2(_inputReceivable.MoveH() * _playerStatus._Speed, _rb.velocity.y);
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Dash"), true);
        }

    }

}
