using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerJumpState : MonoBehaviour, IPlayerState
    {
        // PlayerのJump状態処理

        [SerializeField] private AudioClip _jumpSE;

        public PlayerStateEnum StateType => PlayerStateEnum.JUMP;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus 　　_playerStatus;
        private PlayerAnimation  _playerAnimation;
        private GroundChecker    _groundChecker;

        private Rigidbody2D   _rb;
        private BoxCollider2D _boxCol;
        private AudioSource   _audioSource;
        

        void IPlayerState.OnStart(PlayerStateEnum beforeState)
        {
            _playerStatus    ??= GetComponent<PlayerStatus>();
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _groundChecker   ??= GetComponent<GroundChecker>();
            _rb              ??= GetComponent<Rigidbody2D>();
            _boxCol          ??= GetComponent<BoxCollider2D>();
            _audioSource     ??= GetComponent<AudioSource>();

            _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Jump"));

            // 物理挙動
            JumpAction();
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
            // 上昇状態に遷移
            if (_rb.velocity.y > 0.1f)
            {
                ChangeStateEvent(PlayerStateEnum.JUMPUP);
            }
            // 移動上昇状態に遷移
            if (_rb.velocity.y > 0.1f &&_inputReceivable.MoveH() != 0)
            {
                ChangeStateEvent(PlayerStateEnum.DASHJUMPUP);
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

        // ジャンプアクションメソッド
        private void JumpAction()
        {
            // 物理挙動
            _rb.velocity = Vector2.zero;
            _rb.AddForce(Vector2.up * _playerStatus._JumpPower);
            _audioSource.PlayOneShot(_jumpSE);
        }

    }

}
