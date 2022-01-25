using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerLandingState : MonoBehaviour, IPlayerState
    {
        // Playerが実装するの！？
        // PlayerのStay状態処理

        public PlayerStateEnum StateType => PlayerStateEnum.LANDING;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private PlayerAnimation _playerAnimation;
        private Rigidbody2D _rb;

        void IPlayerState.OnStart(PlayerStateEnum beforeState, PlayerCore player)
        {
            _playerAnimation = GetComponent<PlayerAnimation>();
            _rb = GetComponent<Rigidbody2D>();
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
            // 保留
            ChangeStateEvent(PlayerStateEnum.STAY);
        }

    }

}
