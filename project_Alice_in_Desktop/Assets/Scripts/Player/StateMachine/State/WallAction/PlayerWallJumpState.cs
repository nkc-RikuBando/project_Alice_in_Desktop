using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerWallJumpState : MonoBehaviour, IPlayerState
    {
        // Playerが実装するの！？
        // PlayerのWallJump状態処理

        [SerializeField] private AudioClip _jumpSE;

        public PlayerStateEnum StateType => PlayerStateEnum.WALLJUMP;
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


        // 壁ジャンプ用の垂直な角度
        private const float VERTICAL_ANGLE = 90;


        void IPlayerState.OnStart(PlayerStateEnum beforeState, PlayerCore player)
        {
            _playerStatus ??= GetComponent<PlayerStatus>();
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _groundChecker ??= GetComponent<GroundChecker>();
            _wallChecker ??= GetComponent<WallChecker>();
            _rb         ??= GetComponent<Rigidbody2D>();
            _boxCol ??= GetComponent<BoxCollider2D>();
            _capCol ??= GetComponent<CapsuleCollider2D>();
            _audioSource ??= GetComponent<AudioSource>();

            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
            _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Jump"));

            _playerStatus._InputFlgX = false;

            WallJumpAction();
        }

        void IPlayerState.OnUpdate(PlayerCore player)
        {
            Debug.Log(StateType);
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
            if (_inputReceivable.MoveH() != 0) 
            {
                ChangeStateEvent(PlayerStateEnum.DASHWALLJUMP);
            }

            if (_rb.velocity.y > 0.1f) 
            {
                ChangeStateEvent(PlayerStateEnum.WALLJUMPUP);
            }

            // ここからDashJumpUPに遷移するのか？？
        }

        // 壁ジャンプメソッド
        private void WallJumpAction() 
        {
            // 物理挙動
            _rb.velocity = Vector2.zero;
            _rb.AddForce(JumpAngle().normalized * _playerStatus._WallJumpPower);

            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            _audioSource.PlayOneShot(_jumpSE);
        }

        // 壁ジャンプする角度からベクトルに変換するメソッド
        private Vector2 JumpAngle()
        {
            float tempAngle = (VERTICAL_ANGLE + (_playerStatus._WallJumpAngle * transform.localScale.x / _playerStatus._SizeMag)) * Mathf.Deg2Rad;

            return new Vector2(Mathf.Cos(tempAngle), Mathf.Sin(tempAngle));
        }

    }
}
