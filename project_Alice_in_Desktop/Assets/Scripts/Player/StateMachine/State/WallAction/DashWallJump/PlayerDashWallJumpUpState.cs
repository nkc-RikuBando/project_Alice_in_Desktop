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
        // PlayerのWallDashJumpUp状態処理

        public PlayerStateEnum StateType => PlayerStateEnum.DASHWALLJUMPUP;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus     _playerStatus;
        private PlayerAnimation  _playerAnimation;
        private GroundChecker    _groundChecker;

        private Rigidbody2D _rb;
        private BoxCollider2D _boxCol;


        void IPlayerState.OnStart(PlayerStateEnum beforeState)
        {
            _playerStatus    ??= GetComponent<PlayerStatus>();
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _groundChecker   ??= GetComponent<GroundChecker>();
            _rb              ??= GetComponent<Rigidbody2D>();
            _boxCol          ??= GetComponent<BoxCollider2D>();

            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("JumpUp"), true);

            if (_playerStatus._InsideFlg) _playerStatus._GroundJudge = true;
        }

        void IPlayerState.OnUpdate()
        {
            StateManager(); 
            transform.localScale = new Vector3(_playerStatus.DirectionNum * _playerStatus._SizeMag, 1f * _playerStatus._SizeMag, 1f);
        }

        void IPlayerState.OnFixedUpdate()
        {
        }
        void IPlayerState.OnEnd(PlayerStateEnum nextState)
        {
        }


        // Playerステート変更メソッド
        private void StateManager()
        {
            // 壁張り付き上昇状態に遷移
            if (_inputReceivable.MoveH() == 0)
            {
                ChangeStateEvent(PlayerStateEnum.WALLJUMPUP);
            }

            // 壁張り付き移動下降状態に遷移
            if (_rb.velocity.y < -1f)
            {
                ChangeStateEvent(PlayerStateEnum.DASHWALLJUMPFALL);
            }

            // 着地状態に遷移
            if (_groundChecker.CheckIsGround(_boxCol))
            {
                _playerStatus._InputFlgX = true;
                ChangeStateEvent(PlayerStateEnum.LANDING);
            }
        }
    }
}
