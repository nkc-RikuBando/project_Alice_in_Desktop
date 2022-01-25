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
        // Player‚ªŽÀ‘•‚·‚é‚ÌIH
        // Player‚ÌStayó‘Ôˆ—

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
            // •Û—¯
            ChangeStateEvent(PlayerStateEnum.STAY);
        }

    }

}
