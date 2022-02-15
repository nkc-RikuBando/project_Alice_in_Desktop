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
        // PlayerÇ™é¿ëïÇ∑ÇÈÇÃÅIÅH
        // PlayerÇÃJumpUpèÛë‘èàóù

        public PlayerStateEnum StateType => PlayerStateEnum.JUMPUP;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerAnimation  _playerAnimation;
        private GroundChecker _groundChecker;

        private Rigidbody2D _rb;
        private BoxCollider2D _boxCol;


        void IPlayerState.OnStart(PlayerStateEnum beforeState, PlayerCore player)
        {
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _groundChecker   ??= GetComponent<GroundChecker>();
            _rb              ??= GetComponent<Rigidbody2D>();
            _boxCol          ??= GetComponent<BoxCollider2D>();

            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("JumpUp"), true);
        }

        void IPlayerState.OnUpdate(PlayerCore player)
        {
            //Debug.Log(StateType);
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
            // íÖínèÛë‘
            if (_groundChecker.CheckIsGround(_boxCol))
            {
                ChangeStateEvent(PlayerStateEnum.LANDING);
            }


            // à⁄ìÆèÛë‘
            if (_inputReceivable.MoveH() != 0)
            {
                ChangeStateEvent(PlayerStateEnum.DASHJUMPUP);
            }

            // â∫ç~èÛë‘
            if (_rb.velocity.y < -1f)
            {
                ChangeStateEvent(PlayerStateEnum.FALL);
            }

        }

    }

}
