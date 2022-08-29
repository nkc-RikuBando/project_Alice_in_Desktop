using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerFallState : MonoBehaviour, IPlayerState
    {
        // PlayerのFall状態処理

        public PlayerStateEnum StateType => PlayerStateEnum.FALL;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus     _playerStatus;
        private GroundChecker    _groundChecker;
        private PlayerAnimation  _playerAnimation;

        private BoxCollider2D _boxCol;


        void IPlayerState.OnStart(PlayerStateEnum beforeState)
        {
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _playerStatus    ??= GetComponent<PlayerStatus>();
            _groundChecker   ??= GetComponent<GroundChecker>();
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _boxCol          ??= GetComponent<BoxCollider2D>();

            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), true);

            // ウィンドウの外にいる場合は地面判定をしない
            if (_playerStatus._InsideFlg) _playerStatus._GroundJudge = true;
            else                          _playerStatus._GroundJudge = false; 
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

        // ステート変更メソッド
        private void StateManager()
        {
            // 着地状態に遷移
            if (_groundChecker.CheckIsGround(_boxCol)) 
            {
                ChangeStateEvent(PlayerStateEnum.LANDING);
            }

            // 移動下降状態に遷移
            if (_inputReceivable.MoveH() != 0)
            {
                ChangeStateEvent(PlayerStateEnum.DASHFALL);
            }
        }

    }

}
