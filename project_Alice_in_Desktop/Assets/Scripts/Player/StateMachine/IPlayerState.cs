using System;

namespace PlayerState
{
    public interface IPlayerState
    {
        // �X�e�[�g�����̃C���^�[�t�F�[�X

        event Action<PlayerStateEnum> ChangeStateEvent;

        PlayerStateEnum StateType { get; }


        void OnStart(PlayerStateEnum beforeState, PlayerCore player);

        void OnUpdate(PlayerCore player);

        void OnFixedUpdate(PlayerCore player);

        void OnEnd(PlayerStateEnum nextState, PlayerCore player);

    }
}
