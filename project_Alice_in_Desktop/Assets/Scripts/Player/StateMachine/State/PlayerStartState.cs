using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class PlayerStartState : MonoBehaviour, IPlayerState
    {
        // Player����������́I�H
        // Player��Start��ԏ���

        public PlayerStateEnum StateType => PlayerStateEnum.START;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        void IPlayerState.OnStart(PlayerStateEnum beforeState, PlayerCore player)
        {
        }

        void IPlayerState.OnUpdate(PlayerCore player)
        {
            //Debug.Log(StateType);
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
