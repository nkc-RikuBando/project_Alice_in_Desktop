using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerJumpState : MonoBehaviour, IPlayerState
    {
        // Playerが実装するの！？
        // PlayerのStay状態処理

        public PlayerStateEnum StateType => PlayerStateEnum.JUMP;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus 　　_playerStatus;
        private PlayerAnimation  _playerAnimation;

        private Rigidbody2D _rb;


        void IPlayerState.OnStart(PlayerStateEnum beforeState, PlayerCore player)
        {
            _playerStatus    = GetComponent<PlayerStatus>();
            _inputReceivable = GetComponent<IInputReceivable>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _rb              = GetComponent<Rigidbody2D>();

            // 物理挙動
            JumpAction();
        }

        void IPlayerState.OnUpdate(PlayerCore player)
        {
            Debug.Log(StateType);
            StateManager();
        }

        void IPlayerState.OnFixedUpdate(PlayerCore player)
        {
        }

        void IPlayerState.OnEnd(PlayerStateEnum nextState, PlayerCore player)
        {
        }

        private void StateManager()
        {
            if (_inputReceivable.MoveH() != 0)
            {
                ChangeStateEvent(PlayerStateEnum.DASH);
            }

            // 上昇状態
            if (_rb.velocity.y > 0.1f)
            {
                ChangeStateEvent(PlayerStateEnum.JUMPUP);
            }
            // 下降状態
            else if (_rb.velocity.y < -0.1f)
            {
                ChangeStateEvent(PlayerStateEnum.FALL);
            }
        }

        // ジャンプアクションメソッド
        private void JumpAction()
        {
            // 物理挙動
            _rb.velocity = Vector2.zero;
            _rb.AddForce(Vector2.up * _playerStatus._JumpPower);
        }

    }

}
