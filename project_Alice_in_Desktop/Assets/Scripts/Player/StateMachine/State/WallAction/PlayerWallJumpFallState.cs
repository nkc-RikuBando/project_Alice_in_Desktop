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
        // Player����������́I�H
        // Player��WallFall��ԏ���

        public PlayerStateEnum StateType => PlayerStateEnum.WALLJUMPFALL;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus _playerStatus;
        private PlayerAnimation _playerAnimation;
        private GroundChecker _groundChecker;
        private WallChecker _wallChecker;


        private Rigidbody2D _rb;
        private BoxCollider2D _boxCol;
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

            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Stick"), false);
            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), true);

            // �E�B���h�E�𑀍삵�Ă���ꍇ�͓��͂��󂯂��Ȃ�
            if (!_playerStatus._IsWindowTouching) _playerStatus._InputFlgX = true;

            // �E�B���h�E�̊O�ɂ���ꍇ�͒n�ʔ�������Ȃ�
            if (_playerStatus._InsideFlg) _playerStatus._GroundJudge = true;

            //// Playern�̌����𔽑΂ɂ���
            //_playerStatus.DirectionNum *= -1;
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

        // Player�X�e�[�g�ύX���\�b�h
        private void StateManager()
        {
            if (_inputReceivable.MoveH() != 0)
            {
                ChangeStateEvent(PlayerStateEnum.DASHWALLJUMPFALL);
            }

            if (_groundChecker.CheckIsGround(_boxCol))
            {
                ChangeStateEvent(PlayerStateEnum.LANDING);
            }
        }

    }

}
