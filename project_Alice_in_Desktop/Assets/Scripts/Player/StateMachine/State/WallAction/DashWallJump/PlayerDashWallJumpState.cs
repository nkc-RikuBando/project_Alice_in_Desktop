using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerDashWallJumpState : MonoBehaviour, IPlayerState
    {
        // Playerが実装するの！？
        // PlayerのWallDashJump状態処理

        [SerializeField] private AudioClip _jumpSE;

        public PlayerStateEnum StateType => PlayerStateEnum.DASHWALLJUMP;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus _playerStatus;
        private PlayerAnimation _playerAnimation;
        private GroundChecker _groundChecker;
        private WallChecker _wallChecker;


        private Rigidbody2D _rb;
        private BoxCollider2D _boxCol;
        private CapsuleCollider2D _capCol;
        private AudioSource _audioSource;


        void IPlayerState.OnStart(PlayerStateEnum beforeState, PlayerCore player)
        {
            _playerStatus ??= GetComponent<PlayerStatus>();
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _groundChecker ??= GetComponent<GroundChecker>();
            _wallChecker ??= GetComponent<WallChecker>();
            _rb ??= GetComponent<Rigidbody2D>();
            _boxCol ??= GetComponent<BoxCollider2D>();
            _capCol ??= GetComponent<CapsuleCollider2D>();
            _audioSource ??= GetComponent<AudioSource>();

            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);

            _playerStatus._InputFlgX = false;
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

        // Playerステート変更メソッド
        private void StateManager()
        {
            if (_inputReceivable.MoveH() == 0) 
            {
                ChangeStateEvent(PlayerStateEnum.WALLJUMP);
            }

            if (_rb.velocity.y > 0.1f)
            {
                ChangeStateEvent(PlayerStateEnum.DASHWALLJUMPUP);
            }
        }
    }
}
