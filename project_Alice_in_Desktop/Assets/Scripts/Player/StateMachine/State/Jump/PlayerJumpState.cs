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
        // Player����������́I�H
        // Player��Stay��ԏ���

        public PlayerStateEnum StateType => PlayerStateEnum.JUMP;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus �@�@_playerStatus;
        private PlayerAnimation  _playerAnimation;

        private Rigidbody2D _rb;


        void IPlayerState.OnStart(PlayerStateEnum beforeState, PlayerCore player)
        {
            _playerStatus    = GetComponent<PlayerStatus>();
            _inputReceivable = GetComponent<IInputReceivable>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _rb              = GetComponent<Rigidbody2D>();

            // ��������
            JumpAction();
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

        private void StateManager()
        {
            if (_inputReceivable.MoveH() != 0)
            {
                ChangeStateEvent(PlayerStateEnum.DASH);
            }

            // �㏸���
            if (_rb.velocity.y > 0.1f)
            {
                ChangeStateEvent(PlayerStateEnum.JUMPUP);
            }
            // ���~���
            else if (_rb.velocity.y < -0.1f)
            {
                ChangeStateEvent(PlayerStateEnum.FALL);
            }
        }

        // �W�����v�A�N�V�������\�b�h
        private void JumpAction()
        {
            // ��������
            _rb.velocity = Vector2.zero;
            _rb.AddForce(Vector2.up * _playerStatus._JumpPower);
        }

    }

}
