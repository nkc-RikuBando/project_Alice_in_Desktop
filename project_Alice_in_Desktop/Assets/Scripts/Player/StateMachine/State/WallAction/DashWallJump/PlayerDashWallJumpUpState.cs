using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerDashWallJumpUpState : MonoBehaviour, IPlayerState
    {
        // Playerが実装するの！？
        // PlayerのWallDashJumpUp状態処理

        public PlayerStateEnum StateType => PlayerStateEnum.DASHWALLJUMPUP;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus _playerStatus;
        private PlayerAnimation _playerAnimation;
        private GroundChecker _groundChecker;
        private WallChecker _wallChecker;


        private Rigidbody2D _rb;
        private BoxCollider2D _boxCol;
        private CapsuleCollider2D _capCol;


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

            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("JumpUp"), true);

            if (_playerStatus._InsideFlg) _playerStatus._GroundJudge = true;
        }

        void IPlayerState.OnUpdate(PlayerCore player)
        {
            //Debug.Log(StateType);
            StateManager(); 
            transform.localScale = new Vector3(_playerStatus.DirectionNum * _playerStatus._SizeMag, 1f * _playerStatus._SizeMag, 1f);
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
                ChangeStateEvent(PlayerStateEnum.WALLJUMPUP);
            }

            if (_rb.velocity.y < -1f)
            {
                ChangeStateEvent(PlayerStateEnum.DASHWALLJUMPFALL);
            }

            if (_groundChecker.CheckIsGround(_boxCol))
            {
                _playerStatus._InputFlgX = true;
                ChangeStateEvent(PlayerStateEnum.LANDING);
            }

        }
    }

}
