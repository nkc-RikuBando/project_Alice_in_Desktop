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
        // PlayerのWallDashJump状態処理

        public PlayerStateEnum StateType => PlayerStateEnum.DASHWALLJUMP;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus     _playerStatus;
        private PlayerAnimation  _playerAnimation;
        private GroundChecker    _groundChecker;


        private Rigidbody2D   _rb;
        private BoxCollider2D _boxCol;


        void IPlayerState.OnStart(PlayerStateEnum beforeState)
        {
            _playerStatus    ??= GetComponent<PlayerStatus>();
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _groundChecker   ??= GetComponent<GroundChecker>();
            _rb              ??= GetComponent<Rigidbody2D>();
            _boxCol          ??= GetComponent<BoxCollider2D>();

            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);

            _playerStatus._InputFlgX = false;

            // 外に出た時は地面判定しない
            if (_playerStatus._InsideFlg) _playerStatus._GroundJudge = true;
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


        // Playerステート変更メソッド
        private void StateManager()
        {
            // 壁ジャンプ状態に遷移
            if (_inputReceivable.MoveH() == 0) 
            {
                ChangeStateEvent(PlayerStateEnum.WALLJUMP);
            }

            // 壁ジャンプ移動上昇に遷移
            if (_rb.velocity.y > 0.1f)
            {
                ChangeStateEvent(PlayerStateEnum.DASHWALLJUMPUP);
            }

            // 地面判定 ＆ 着地状態に遷移
            if (_groundChecker.CheckIsGround(_boxCol))
            {
                // 足折れバグ回避用アニメーター変数
                _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Exit"));
                _playerStatus._InputFlgX = true;
                ChangeStateEvent(PlayerStateEnum.LANDING);
            }
        }
    }
}
