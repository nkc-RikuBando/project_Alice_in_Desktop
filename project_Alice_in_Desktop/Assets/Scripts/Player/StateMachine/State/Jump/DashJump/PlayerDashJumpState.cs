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
        // Player����������́I�H
        // Player��DashJump��ԏ���

        public PlayerStateEnum StateType => PlayerStateEnum.DASHJUMP;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus _playerStatus;
        private PlayerAnimation _playerAnimation;
        private WallChecker _wallChecker;

        private Rigidbody2D _rb;
        private CapsuleCollider2D _capCol;



        void IPlayerState.OnStart(PlayerStateEnum beforeState, PlayerCore player)
        {
            _playerStatus ??= GetComponent<PlayerStatus>();
            _inputReceivable ??= GetComponent<IInputReceivable>();
            _playerAnimation ??= GetComponent<PlayerAnimation>();
            _wallChecker ??= GetComponent<WallChecker>();
            _rb ??= GetComponent<Rigidbody2D>();
            _capCol ??= GetComponent<CapsuleCollider2D>();


            _playerAnimation.AnimationTriggerChange(Animator.StringToHash("Jump"));

            // ��������
            JumpAction();
        }

        void IPlayerState.OnUpdate(PlayerCore player)
        {
            Debug.Log(StateType);
            Dash();
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
            if (_wallChecker.CheckIsWall(_capCol))
            {
                ChangeStateEvent(PlayerStateEnum.WALLSTICK);
            }

            if (_rb.velocity.y > 0.1f)
            {
                ChangeStateEvent(PlayerStateEnum.DASHJUMPUP);
            }
            else if (_rb.velocity.y > 0.1f && _inputReceivable.MoveH() == 0)
            {
                ChangeStateEvent(PlayerStateEnum.JUMPUP);
            }

            // ���͂��Ȃ��ꍇ��JUMP�ɑJ�ڂ��Ȃ��̂��H

        }

        // �W�����v�A�N�V�������\�b�h
        private void JumpAction()
        {
            // ��������
            _rb.velocity = Vector2.zero;
            _rb.AddForce(Vector2.up * _playerStatus._JumpPower);
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
