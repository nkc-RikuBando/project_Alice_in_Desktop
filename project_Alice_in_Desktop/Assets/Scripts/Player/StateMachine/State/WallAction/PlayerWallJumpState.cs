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
        // PlayerのWallJump状態処理

        [SerializeField] private AudioClip _jumpSE;

        public PlayerStateEnum StateType => PlayerStateEnum.WALLJUMP;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus     _playerStatus;
        private PlayerAnimation  _playerAnimation;
        private GroundChecker    _groundChecker;


        private Rigidbody2D   _rb;
        private BoxCollider2D _boxCol;
        private AudioSource   _audioSource;


        // 壁ジャンプ用の垂直な角度
        private const float VERTICAL_ANGLE = 90;


        void IPlayerState.OnStart(PlayerStateEnum beforeState)
        {
            _playerStatus    ??= GetComponent<PlayerStatus>();
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _groundChecker   ??= GetComponent<GroundChecker>();
            _rb              ??= GetComponent<Rigidbody2D>();
            _boxCol          ??= GetComponent<BoxCollider2D>();
            _audioSource     ??= GetComponent<AudioSource>();

            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
            _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Jump"));

            _playerStatus._InputFlgX = false;
            if (_playerStatus._InsideFlg) _playerStatus._GroundJudge = true;

            WallJumpAction();
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
            // 壁張り付き移動ジャンプ状態に遷移
            if (_inputReceivable.MoveH() != 0) 
            {
                ChangeStateEvent(PlayerStateEnum.DASHWALLJUMP);
            }

            // 壁張り付き上昇状態に遷移
            if (_rb.velocity.y > 0.1f) 
            {
                ChangeStateEvent(PlayerStateEnum.WALLJUMPUP);
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

        // 壁ジャンプメソッド
        private void WallJumpAction() 
        {
            // 物理挙動
            _rb.velocity = Vector2.zero;
            _rb.AddForce(JumpAngle().normalized * _playerStatus._WallJumpPower);

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
