using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerWallJumpUpState : MonoBehaviour, IPlayerState
    {
        // Player��WallJumpUp��ԏ���

        public PlayerStateEnum StateType => PlayerStateEnum.WALLJUMPUP;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus     _playerStatus;
        private PlayerAnimation  _playerAnimation;
        private GroundChecker    _groundChecker;

        private Rigidbody2D   _rb;
        private BoxCollider2D _boxCol;


        void IPlayerState.OnStart(PlayerStateEnum beforeState)
        {
            _playerStatus    ??= GetComponent<PlayerStatus>();
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _groundChecker   ??= GetComponent<GroundChecker>();
            _rb              ??= GetComponent<Rigidbody2D>();
            _boxCol          ??= GetComponent<BoxCollider2D>();

            _playerAnimation.AnimationBoolenChange(Animator.StringToHash("JumpUp"), true);
            if (_playerStatus._InsideFlg) _playerStatus._GroundJudge = true;

        }

        void IPlayerState.OnUpdate()
        {
            StateManager();
            transform.localScale = new Vector3(_playerStatus.DirectionNum * _playerStatus._SizeMag, 1f * _playerStatus._SizeMag, 1f);
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
            // �ǒ���t���ړ��㏸��ԂɑJ��
            if (_inputReceivable.MoveH() != 0)
            {
                ChangeStateEvent(PlayerStateEnum.DASHWALLJUMPUP);
            }

            // �ǒ���t�����~��ԂɑJ�ڂ�
            if (_rb.velocity.y < -1f)
            {
                ChangeStateEvent(PlayerStateEnum.WALLJUMPFALL);
            }

            // ���n��ԂɑJ��
            if (_groundChecker.CheckIsGround(_boxCol))
            {
                _playerStatus._InputFlgX = true;
                ChangeStateEvent(PlayerStateEnum.LANDING);
            }

        }
    }

}
