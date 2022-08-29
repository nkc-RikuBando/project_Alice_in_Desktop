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
        // Player��WallFall��ԏ���

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

            // �E�B���h�E�𑀍삵�Ă���ꍇ�͓��͂��󂯂��Ȃ�
            if (!_playerStatus._IsWindowTouching) _playerStatus._InputFlgX = true;

            // �E�B���h�E�̊O�ɂ���ꍇ�͒n�ʔ�������Ȃ�
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


        // Player�X�e�[�g�ύX���\�b�h
        private void StateManager()
        {
            // �ǒ���t���ړ����~��ԂɑJ��
            if (_inputReceivable.MoveH() != 0)
            {
                ChangeStateEvent(PlayerStateEnum.DASHWALLJUMPFALL);
            }

            // ���n��ԂɑJ��
            if (_groundChecker.CheckIsGround(_boxCol))
            {
                ChangeStateEvent(PlayerStateEnum.LANDING);
            }
        }

    }

}
