using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerDashWallJumpFallState : MonoBehaviour, IPlayerState
    {
        // Player��WallDashFall��ԏ���

        public PlayerStateEnum StateType => PlayerStateEnum.DASHWALLJUMPFALL;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus     _playerStatus;
        private PlayerAnimation  _playerAnimation;
        private GroundChecker    _groundChecker;
        private WallChecker      _wallChecker;

        private Rigidbody2D       _rb;
        private BoxCollider2D     _boxCol;
        private CapsuleCollider2D _capCol;


        void IPlayerState.OnStart(PlayerStateEnum beforeState)
        {
            _playerStatus    ??= GetComponent<PlayerStatus>();
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _groundChecker   ??= GetComponent<GroundChecker>();
            _wallChecker     ??= GetComponent<WallChecker>();
            _rb              ??= GetComponent<Rigidbody2D>();
            _boxCol          ??= GetComponent<BoxCollider2D>();
            _capCol          ??= GetComponent<CapsuleCollider2D>();

            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), true);

            // �E�B���h�E�𑀍삵�Ă���ꍇ�͓��͂��󂯂��Ȃ�
            if (!_playerStatus._IsWindowTouching) _playerStatus._InputFlgX = true;

            // �E�B���h�E�̊O�ɂ���ꍇ�͒n�ʔ�������Ȃ�
            if (_playerStatus._InsideFlg) _playerStatus._GroundJudge = true;
        }

        void IPlayerState.OnUpdate()
        {
            Dash();
            StateManager();
        }

        void IPlayerState.OnFixedUpdate()
        {
        }
        void IPlayerState.OnEnd(PlayerStateEnum nextState)
        {
        }


        // Player�X�e�[�g�ύX���\�b�h
        private void StateManager()
        {
            // �ǒ���t����ԂɑJ��
            if (_wallChecker.CheckIsWall(_capCol))
            {
                ChangeStateEvent(PlayerStateEnum.WALLSTICK);
            }

            // �ǒ���t�����~��ԂɑJ��
            if (_inputReceivable.MoveH() == 0)
            {
                ChangeStateEvent(PlayerStateEnum.WALLJUMPFALL);
            }

            // ���n��ԂɑJ��
            if (_groundChecker.CheckIsGround(_boxCol))
            {
                ChangeStateEvent(PlayerStateEnum.LANDING);
            }
        }

        // Player�ړ����\�b�h
        private void Dash()
        {
            if (!_playerStatus._InputFlgX) return;

            // �ړ��̕�������
            _rb.velocity = new Vector2(_inputReceivable.MoveH() * _playerStatus._Speed, _rb.velocity.y);
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Dash"), true);
        }
    }
}
