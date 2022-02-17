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
        // Player����������́I�H
        // Player��WallStick��ԏ���

        [SerializeField] private AudioClip _stickSE;

        public PlayerStateEnum StateType => PlayerStateEnum.WALLSTICK;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus _playerStatus;
        private PlayerAnimation _playerAnimation;
        private GroundChecker _groundChecker;
        private WallChecker _wallChecker;


        private Rigidbody2D _rb;
        private BoxCollider2D _boxCol;
        private BoxCollider2D _childWallCheckCol;
        private BoxCollider2D _childGroundCheckCol;
        private CapsuleCollider2D _capCol;
        private AudioSource _audioSource;

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
            _audioSource     ??= GetComponent<AudioSource>();


            // �ړ����̎q�I�u�W�F�N�g�ɂȂ锻��R���C�_�[
            _childWallCheckCol   = transform.GetChild(1).GetComponent<BoxCollider2D>();
            _childGroundCheckCol = transform.GetChild(2).GetComponent<BoxCollider2D>();

            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), true);
            _audioSource.PlayOneShot(_stickSE);

            _playerStatus._GroundJudge = false;
            _childGroundCheckCol.enabled = false;
        }

        void IPlayerState.OnUpdate(PlayerCore player)
        {
            Debug.Log(StateType);
            StateManager();
        }

        void IPlayerState.OnFixedUpdate(PlayerCore player)
        {
            WallStick();
        }

        void IPlayerState.OnEnd(PlayerStateEnum nextState, PlayerCore player)
        {
        }

        // Player�X�e�[�g�ύX���\�b�h
        private void StateManager()
        {
            if (_playerStatus._IsWindowTouching) return;

            if (!_wallChecker.CheckIsWall(_capCol)) 
            {
                _rb.gravityScale = _playerStatus._Gravity;
                _childWallCheckCol.enabled = false;
                _childGroundCheckCol.enabled = true;

                ChangeStateEvent(PlayerStateEnum.WALLJUMPFALL);
            }

            // �ǒ���t�����~
            if (transform.localScale.x > 0)
            {
                if (!_inputReceivable.MoveKey_D())
                {
                    _rb.gravityScale = _playerStatus._Gravity;
                    _childWallCheckCol.enabled �@= false;
                    _childGroundCheckCol.enabled = true;

                    ChangeStateEvent(PlayerStateEnum.WALLJUMPFALL);
                }
            }
            else if (transform.localScale.x < 0)
            {
                if (!_inputReceivable.MoveKey_A())
                {
                    _rb.gravityScale = _playerStatus._Gravity;
                    _childWallCheckCol.enabled = false;
                    _childGroundCheckCol.enabled = true;

                    ChangeStateEvent(PlayerStateEnum.WALLJUMPFALL);
                }
            }


            // �ǃW�����v
            if (transform.localScale.x > 0)
            {
                if (_inputReceivable.WallJumpKey_A() || _inputReceivable.JumpKey())
                {
                    _rb.gravityScale = _playerStatus._Gravity;
                    _childWallCheckCol.enabled = false;
                    _childGroundCheckCol.enabled = true;

                    ChangeStateEvent(PlayerStateEnum.WALLJUMP);
                }
            }
            else if (transform.localScale.x < 0)
            {
                if (_inputReceivable.WallJumpKey_D() || _inputReceivable.JumpKey())
                {
                    _rb.gravityScale = _playerStatus._Gravity;
                    _childWallCheckCol.enabled = false;
                    _childGroundCheckCol.enabled = true;

                    ChangeStateEvent(PlayerStateEnum.WALLJUMP);
                }
            }
        }

        // �ǒ���t���������\�b�h
        private void WallStick()
        {         
            if (!_wallChecker.CheckIsWall(_capCol)) return;

            if (transform.localScale.x > 0)
            {
                if (_inputReceivable.MoveKey_D())
                {
                    // Player��Î~��Ԃɂ���
                    _rb.velocity = Vector2.zero;
                    _rb.gravityScale = 0;

                    _childWallCheckCol.enabled = true;
                    _playerStatus.DirectionNum = -1;
                }
            }
            else if (transform.localScale.x < 0)
            {
                if (_inputReceivable.MoveKey_A())
                {
                    // Player��Î~��Ԃɂ���
                    _rb.velocity = Vector2.zero;
                    _rb.gravityScale = 0;

                    _childWallCheckCol.enabled = true;
                    _playerStatus.DirectionNum = 1;
                }
            }
        }
    }
}
