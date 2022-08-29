using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class PlayerStateManager : MonoBehaviour
    {
        // �X�e�[�g�ύX���鏈��

        public PlayerStateEnum crrentPlayerState = PlayerStateEnum.START;
        private Dictionary<PlayerStateEnum, IPlayerState> playerStateDic = new Dictionary<PlayerStateEnum, IPlayerState>((int)PlayerStateEnum.COUNT);


        void Start()
        {
            // IPlayer��z��ŕ����擾
            IPlayerState[] stateComponents = GetComponents<IPlayerState>();

            if (stateComponents.Length != (int)PlayerStateEnum.COUNT)
            {
                Debug.LogError("State�̐����Ⴂ�܂�");
            }

            // Dictionary�̒��g��ݒ�
            for (int i = 0; i < stateComponents.Length; i++)
            {
                IPlayerState state = stateComponents[i];

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



                // playerStateDic���Ă̂����݂�Player�̃X�e�[�g��ێ����Ă�
                // Dictionary��[Key]��value�������Ă���
                // Key��Player�̃X�e�[�gEnum�ɂ��Ă���
                // Value�͌��݂�Player�̃X�e�[�g�����Ă�
                // playerStateDic[�Y����] = �l�����Ă��� 
            }
        }

        void Update()
        {
            playerStateDic[crrentPlayerState].OnUpdate();
        }

        private void FixedUpdate()
        {
            playerStateDic[crrentPlayerState].OnFixedUpdate();
        }


        // �X�e�[�g�ύX���\�b�h(event�ϐ��ɑ�����郁�\�b�h)
        public void ChangeState(PlayerStateEnum playerState)
        {
            if (playerState is PlayerStateEnum.COUNT)
            {
                Debug.LogErrorFormat($"Count state is specified ,CurrentState{crrentPlayerState}");
                return;
            }

            playerStateDic[crrentPlayerState].OnEnd(playerState);

            // ���g��ύX
            crrentPlayerState = playerState;

            playerStateDic[crrentPlayerState].OnStart(playerState);
        }
    }
}
