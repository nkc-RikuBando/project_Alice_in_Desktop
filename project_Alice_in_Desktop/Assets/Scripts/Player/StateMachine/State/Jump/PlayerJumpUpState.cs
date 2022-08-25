using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerJumpUpState : MonoBehaviour, IPlayerState
    {
        // Player‚ÌJumpUpó‘Ôˆ—

        public PlayerStateEnum StateType => PlayerStateEnum.JUMPUP;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerAnimation  _playerAnimation;
        private GroundChecker    _groundChecker;

        private Rigidbody2D   _rb;
        private BoxCollider2D _boxCol;


        void IPlayerState.OnStart(PlayerStateEnum beforeState)
        {
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _groundChecker   ??= GetComponent<GroundChecker>();
            _rb              ??= GetComponent<Rigidbody2D>();
            _boxCol          ??= GetComponent<BoxCollider2D>();

            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("JumpUp"), true);
        }

        void IPlayerState.OnUpdate()
        {
            StateManager();
        }

        void IPlayerState.OnFixedUpdate()
        {
        }

        void IPlayerState.OnEnd(PlayerStateEnum nextState)
        {
        }


        private void StateManager()
        {
            // ’…’nó‘Ô‚É‘JˆÚ
            if (_groundChecker.CheckIsGround(_boxCol))
            {
                ChangeStateEvent(PlayerStateEnum.LANDING);
            }

            // ˆÚ“®ó‘Ô‚É‘JˆÚ
            if (_inputReceivable.MoveH() != 0)
            {
                ChangeStateEvent(PlayerStateEnum.DASHJUMPUP);
            }

            // ‰º~ó‘Ô‚É‘JˆÚ
            if (_rb.velocity.y < -1f)
            {
                ChangeStateEvent(PlayerStateEnum.FALL);
            }
        }
    }
}
