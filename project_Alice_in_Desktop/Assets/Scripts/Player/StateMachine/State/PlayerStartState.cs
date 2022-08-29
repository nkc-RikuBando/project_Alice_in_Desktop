using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class PlayerStartState : MonoBehaviour, IPlayerState
    {
        // Player‚ÌStartó‘Ôˆ—

        public PlayerStateEnum StateType => PlayerStateEnum.START;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        void IPlayerState.OnStart(PlayerStateEnum beforeState)
        {
        }

        void IPlayerState.OnUpdate()
        {
            // ‘Ò‹@ó‘Ô‚É‘JˆÚ
            ChangeStateEvent(PlayerStateEnum.STAY);
        }

        void IPlayerState.OnFixedUpdate()
        {
        }

        void IPlayerState.OnEnd(PlayerStateEnum nextState)
        {
        }


    }

}
