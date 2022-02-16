using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class PlayerStateManager : MonoBehaviour
    {
        // �X�e�[�g�ύX���鏈��

        [SerializeField]
        private PlayerCore player;

        public PlayerStateEnum crrentPlayerState = PlayerStateEnum.START;
        private Dictionary<PlayerStateEnum, IPlayerState> playerStateDic = new Dictionary<PlayerStateEnum, IPlayerState>((int)PlayerStateEnum.COUNT);


        void Start()
        {
            // IPlayer��z��ŕ����擾���Ă���̂͂Ȃ��H
            IPlayerState[] stateComponents = GetComponents<IPlayerState>();

            if (stateComponents.Length != (int)PlayerStateEnum.COUNT)
            {
                Debug.LogError("State�̐����Ⴂ�܂�");
            }

            // ������Dictionary�̒��g��ݒ肵�Ă�
            for (int i = 0; i < stateComponents.Length; i++)
            {
                IPlayerState state = stateComponents[i];
                // �N���X���z��ɂȂ��Ă�I�H�����I�H
                // ����N���X�̕ϐ��ł͂Ȃ��̂��H

                state.ChangeStateEvent += ChangeState;

                if (state.StateType == PlayerStateEnum.COUNT)
                {
                    Debug.LogError("������Enum�ł�");
                    return;
                }
                if (playerStateDic.ContainsKey(state.StateType))
                {
                    Debug.LogError("State���d�����Ă��܂�");
                    return;
                }

                // �Ō�ɂ�����Player�̃X�e�[�g����
                playerStateDic[state.StateType] = state;
                //Debug.Log(state.StateType);

                // playerStateDic���Ă̂����݂�Player�̃X�e�[�g��ێ����Ă�
                // Dictionary��[Key]��value�������Ă���
                // Key(���O�݂����Ȃ���H)��Player�̃X�e�[�gEnum�ɂ��Ă���
                // Value�͌��݂�Player�̃X�e�[�g�����Ă�

                // playerStateDic[�Y����] = �l�����Ă��� 
            }
        }

        void Update()
        {
            playerStateDic[crrentPlayerState].OnUpdate(player);
        }

        private void FixedUpdate()
        {
            playerStateDic[crrentPlayerState].OnFixedUpdate(player);
        }


        // �����̒���Enum�^�ɂ��Ă��������ۂ��I
        // �X�e�[�g�ύX���\�b�h(event�ϐ��ɑ�����郁�\�b�h)
        public void ChangeState(PlayerStateEnum playerState)
        {
            if (playerState is PlayerStateEnum.COUNT)
            {
                Debug.LogErrorFormat($"Count state is specified ,CurrentState{crrentPlayerState}");
                return;
            }

            playerStateDic[crrentPlayerState].OnEnd(playerState, player);

            // ���g��ύX
            crrentPlayerState = playerState;

            playerStateDic[crrentPlayerState].OnStart(playerState, player);
        }
    }
}
