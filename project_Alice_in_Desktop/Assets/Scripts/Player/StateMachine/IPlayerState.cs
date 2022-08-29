using System;

namespace PlayerState
{
    public interface IPlayerState
    {
        // ステート処理のインターフェース

        event Action<PlayerStateEnum> ChangeStateEvent;

        PlayerStateEnum StateType { get; }


        void OnStart(PlayerStateEnum beforeState);

        void OnUpdate();

        void OnFixedUpdate();

        void OnEnd(PlayerStateEnum nextState);

    }
}
