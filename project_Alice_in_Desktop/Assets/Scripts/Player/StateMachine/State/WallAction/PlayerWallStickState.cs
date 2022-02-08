using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerWallStickState : MonoBehaviour, IPlayerState
    {
        // Playerが実装するの！？
        // PlayerのWallStick状態処理

        public PlayerStateEnum StateType => PlayerStateEnum.WALLSTICK;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus _playerStatus;
        private PlayerAnimation _playerAnimation;
        private GroundChecker _groundChecker;
        private WallChecker _wallChecker;


        private Rigidbody2D _rb;
        private BoxCollider2D _boxCol;
        private BoxCollider2D _childBoxCol;
        private CapsuleCollider2D _capCol;


        void IPlayerState.OnStart(PlayerStateEnum beforeState, PlayerCore player)
        {
            _playerStatus    ??= GetComponent<PlayerStatus>();
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _groundChecker   ??= GetComponent<GroundChecker>();
            _wallChecker     ??= GetComponent<WallChecker>();
            _rb              ??= GetComponent<Rigidbody2D>();
            _boxCol          ??= GetComponent<BoxCollider2D>();
            _capCol          ??= GetComponent<CapsuleCollider2D>();

            // 移動床の子オブジェクトになる判定コライダー
            _childBoxCol = transform.GetChild(1).GetComponent<BoxCollider2D>();

            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), true);
            _playerStatus._GroundJudge = false;
            _childBoxCol.enabled 　　　= false;

            Debug.Log(_childBoxCol);
        }

        void IPlayerState.OnUpdate(PlayerCore player)
        {
        }

        void IPlayerState.OnFixedUpdate(PlayerCore player)
        {
            Debug.Log(StateType);
            WallStick();
            StateManager();
        }

        void IPlayerState.OnEnd(PlayerStateEnum nextState, PlayerCore player)
        {
        }

        // Playerステート変更メソッド
        private void StateManager()
        {
            if (_inputReceivable.JumpKey()) 
            {
                _rb.gravityScale = _playerStatus._Gravity;
                _childBoxCol.enabled = false;

                ChangeStateEvent(PlayerStateEnum.WALLJUMP);
            }

            if (_inputReceivable.MoveH() == 0)
            {
                _rb.gravityScale = _playerStatus._Gravity;
                _childBoxCol.enabled = false;

                ChangeStateEvent(PlayerStateEnum.WALLJUMPFALL);
            }
        }

        // 壁張り付き挙動メソッド
        private void WallStick() 
        {
            // この入力処理だと反対向いても貼り付けそう
            // でも左右入力で壁判定のRayの方向が変わるからそんなことない？？
            if (_inputReceivable.MoveH() != 0 && _wallChecker.CheckIsWall(_capCol)) 
            {
                // Playerを静止状態にする
                _rb.velocity = Vector2.zero;
                _rb.gravityScale = 0;

                _childBoxCol.enabled = true;
            }
        }
    }

}
