using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Player;
using MyUtility;

namespace PlayerState
{
    public class PlayerFallState : MonoBehaviour, IPlayerState
    {
        // Player����������́I�H
        // Player��Stay��ԏ���

        public PlayerStateEnum StateType => PlayerStateEnum.FALL;
        public event Action<PlayerStateEnum> ChangeStateEvent;

        private IInputReceivable _inputReceivable;
        private GroundChecker _groundChecker;
        private PlayerAnimation _playerAnimation;

        private BoxCollider2D _boxCol;

        void IPlayerState.OnStart(PlayerStateEnum beforeState, PlayerCore player)
        {
            _inputReceivable = GetComponent<IInputReceivable>();
            _groundChecker   = GetComponent<GroundChecker>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _boxCol          = GetComponent<BoxCollider2D>();
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

        // �X�e�[�g�ύX���\�b�h
        private void StateManager()
        {
            if (_inputReceivable.MoveH() != 0)
            {
                ChangeStateEvent(PlayerStateEnum.DASH);
            }

            if (_groundChecker.CheckIsGround(_boxCol)) 
            {
                ChangeStateEvent(PlayerStateEnum.LANDING);
            }
        }

    }

}
