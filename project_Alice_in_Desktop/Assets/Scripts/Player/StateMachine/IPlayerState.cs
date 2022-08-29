using System;

namespace PlayerState
{
    public interface IPlayerState
    {
        // �X�e�[�g�����̃C���^�[�t�F�[�X

        event Action<PlayerStateEnum> ChangeStateEvent;

        PlayerStateEnum StateType { get; }


        void OnStart(PlayerStateEnum beforeState);

        void OnUpdate();

        void OnFixedUpdate();

        void OnEnd(PlayerStateEnum nextState);

    }
}
