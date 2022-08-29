using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerWallJumpFallState : MonoBehaviour, IPlayerState
    {
        // PlayerのWallFall状態処理

        public PlayerStateEnum StateType => PlayerStateEnum.WALLJUMPFALL;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus     _playerStatus;
        private PlayerAnimation  _playerAnimation;
        private GroundChecker    _groundChecker;

        private BoxCollider2D _boxCol;


        void IPlayerState.OnStart(PlayerStateEnum beforeState)
        {
            _playerStatus    ??= GetComponent<PlayerStatus>();
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _groundChecker   ??= GetComponent<GroundChecker>();
            _boxCol          ??= GetComponent<BoxCollider2D>();

            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), true);

            // ウィンドウを操作している場合は入力を受けつけない
            if (!_playerStatus._IsWindowTouching) _playerStatus._InputFlgX = true;

            // ウィンドウの外にいる場合は地面判定をしない
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
            // 壁張り付き移動下降状態に遷移
            if (_inputReceivable.MoveH() != 0)
            {
                ChangeStateEvent(PlayerStateEnum.DASHWALLJUMPFALL);
            }

            // 着地状態に遷移
            if (_groundChecker.CheckIsGround(_boxCol))
            {
                ChangeStateEvent(PlayerStateEnum.LANDING);
            }
        }

    }

}
