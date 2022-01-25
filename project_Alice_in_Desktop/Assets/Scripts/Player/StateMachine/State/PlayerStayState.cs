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
        // Playerが実装するの！？
        // PlayerのStay状態処理

        public PlayerStateEnum StateType => PlayerStateEnum.STAY;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private GroundChecker _groundChecker;
        private PlayerAnimation _playerAnimation;

        private BoxCollider2D _boxCol;
        private Rigidbody2D _rb;

        void IPlayerState.OnStart(PlayerStateEnum beforeState, PlayerCore player)
        {
            _inputReceivable = GetComponent<IInputReceivable>();
            _groundChecker   = GetComponent<GroundChecker>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _boxCol          = GetComponent<BoxCollider2D>();
            _rb              = GetComponent<Rigidbody2D>();
        }

        void IPlayerState.OnUpdate(PlayerCore player)
        {
            Debug.Log(StateType);
            AnimationReset();
            StateManager();
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
            // 移動状態に変更
            if (_inputReceivable.MoveH() != 0 && _groundChecker.CheckIsGround(_boxCol))
            {
                ChangeStateEvent(PlayerStateEnum.DASH);
            }

            // ジャンプ状態に変更
            if (_inputReceivable.JumpKey_W() && _groundChecker.CheckIsGround(_boxCol) || _inputReceivable.JumpKey_Space() && _groundChecker.CheckIsGround(_boxCol)) 
            {
                ChangeStateEvent(PlayerStateEnum.JUMP);
            }

            // 上昇状態
            if (_rb.velocity.y < -0.1f)
            {
                ChangeStateEvent(PlayerStateEnum.JUMPUP);
            }


            // 下降状態
            if (_rb.velocity.y < -0.1f) 
            {
                ChangeStateEvent(PlayerStateEnum.FALL);
            }
        }

        private void AnimationReset() 
        {
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Dash"), false);
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("JumpUp"), false);
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), false);
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"), false);
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Dash"), false);
        }
    }

}
