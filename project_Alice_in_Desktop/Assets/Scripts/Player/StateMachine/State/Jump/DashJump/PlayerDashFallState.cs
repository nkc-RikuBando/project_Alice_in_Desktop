using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerDashFallState : MonoBehaviour, IPlayerState
    {
        // Player����������́I�H
        // Player��DashFall��ԏ���

        public PlayerStateEnum StateType => PlayerStateEnum.DASHFALL;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private PlayerStatus�@�@ _playerStatus;
        private PlayerAnimation  _playerAnimation;
        private GroundChecker �@ _groundChecker;
        private WallChecker      _wallChecker;

        private Rigidbody2D _rb;
        private BoxCollider2D _boxCol;
        private CapsuleCollider2D _capCol;


        void IPlayerState.OnStart(PlayerStateEnum beforeState, PlayerCore player)
        {
            // ��ł��
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

            // �E�B���h�E�̊O�ɂ���ꍇ�͒n�ʔ�������Ȃ�
            if (_playerStatus._InsideFlg) _playerStatus._GroundJudge = true;
        }

        void IPlayerState.OnUpdate(PlayerCore player)
        {
            //Debug.Log(StateType);
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

            // ���~���
            if (_inputReceivable.MoveH() == 0)
            {
                ChangeStateEvent(PlayerStateEnum.FALL);
            }

            // ���n���
            if (_groundChecker.CheckIsGround(_boxCol))
            {
                ChangeStateEvent(PlayerStateEnum.LANDING);
            }
            
            if (_groundChecker.CheckIsGround(_boxCol)) return;

            if (_wallChecker.CheckIsWall(_capCol)) 
            {
                ChangeStateEvent(PlayerStateEnum.WALLSTICK);
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
