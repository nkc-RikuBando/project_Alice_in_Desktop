using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerDashJumpState : MonoBehaviour, IPlayerState
    {
        // Playerが実装するの！？
        // PlayerのDashJump状態処理

        [SerializeField] private AudioClip _jumpSE;

        public PlayerStateEnum StateType => PlayerStateEnum.DASHJUMP;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus _playerStatus;
        private PlayerAnimation _playerAnimation;
        private GroundChecker _groundChecker;
        private WallChecker _wallChecker;

        private Rigidbody2D _rb;
        private CapsuleCollider2D _capCol;
        private BoxCollider2D _boxCol;
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

            _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Jump"));

            // 物理挙動
            JumpAction();
        }

        void IPlayerState.OnUpdate(PlayerCore player)
        {
            //Debug.Log(StateType);
            Dash();
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
            if (_rb.velocity.y > 0.1f)
            {
                ChangeStateEvent(PlayerStateEnum.DASHJUMPUP);
            }
            else if (_rb.velocity.y > 0.1f && _inputReceivable.MoveH() == 0)
            {
                ChangeStateEvent(PlayerStateEnum.JUMPUP);
            }

            // 地面判定
            if (_groundChecker.CheckIsGround(_boxCol))
            {
                // 足折れバグ回避用アニメーター変数
                _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Exit"));
                _playerStatus._InputFlgX = true;
                ChangeStateEvent(PlayerStateEnum.LANDING);
            }

        }

        // ジャンプアクションメソッド
        private void JumpAction()
        {
            // 物理挙動
            _rb.velocity = Vector2.zero;
            _rb.AddForce(Vector2.up * _playerStatus._JumpPower);
            _audioSource.PlayOneShot(_jumpSE);
        }


        // Player移動メソッド
        private void Dash()
        {
            if (!_playerStatus._InputFlgX) return;

            // 移動の物理処理
            _rb.velocity = new Vector2(_inputReceivable.MoveH() * _playerStatus._Speed, _rb.velocity.y);
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Dash"), true);
        }


    }

}
