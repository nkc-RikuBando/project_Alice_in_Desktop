using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using MyUtility;

namespace PlayerState
{
    public class PlayerStartState : MonoBehaviour, IPlayerState
    {
        // Playerが実装するの！？
        // PlayerのStart状態処理

        public PlayerStateEnum StateType => PlayerStateEnum.STAY;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        void IPlayerState.OnStart(PlayerStateEnum beforeState, PlayerCore player)
        {
        }

        void IPlayerState.OnUpdate(PlayerCore player)
        {
            Debug.Log(StateType);
            ChangeStateEvent(PlayerStateEnum.STAY);
        }

        void IPlayerState.OnFixedUpdate(PlayerCore player)
        {
        }

        void IPlayerState.OnEnd(PlayerStateEnum nextState, PlayerCore player)
        {
        }


    }

}
