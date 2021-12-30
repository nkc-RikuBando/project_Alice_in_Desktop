using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using MyUtility;

namespace PlayerState
{
    public class PlayerStayState : MonoBehaviour, IPlayerState
    {
        // Player‚ªŽÀ‘•‚·‚é‚ÌIH
        // Player‚ÌStayó‘Ôˆ—

        public PlayerStateEnum StateType => PlayerStateEnum.STAY;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private GroundChecker _groundChecker;

        private CapsuleCollider2D _capCol;

        void IPlayerState.OnStart(PlayerStateEnum beforeState, PlayerCore player)
        {
            _inputReceivable = GetComponent<IInputReceivable>();
            _groundChecker = GetComponent<GroundChecker>();
            _capCol = GetComponent<CapsuleCollider2D>();
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
            if (_inputReceivable.MoveH() != 0 && _groundChecker.CheckIsGround(_capCol))
            {
                ChangeStateEvent(PlayerStateEnum.DASH);
            }

            if (_inputReceivable.JumpKey_W() || _inputReceivable.JumpKey_Space())
            {
                ChangeStateEvent(PlayerStateEnum.JUMP);
            }
        }

    }

}
