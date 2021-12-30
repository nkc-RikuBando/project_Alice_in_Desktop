using System;

namespace PlayerState
{
    public interface IPlayerState
    {
        // ステート処理のインターフェース

        event Action<PlayerStateEnum> ChangeStateEvent;

        PlayerStateEnum StateType { get; }


        void OnStart(PlayerStateEnum beforeState, PlayerCore player);

        void OnUpdate(PlayerCore player);

        void OnFixedUpdate(PlayerCore player);

        void OnEnd(PlayerStateEnum nextState, PlayerCore player);

    }
}
