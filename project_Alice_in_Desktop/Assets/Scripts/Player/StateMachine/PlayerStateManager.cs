using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class PlayerStateManager : MonoBehaviour
    {
        // ステート変更する処理

        [SerializeField]
        private PlayerCore player;

        public PlayerStateEnum crrentPlayerState = PlayerStateEnum.START;
        private Dictionary<PlayerStateEnum, IPlayerState> playerStateDic = new Dictionary<PlayerStateEnum, IPlayerState>((int)PlayerStateEnum.COUNT);


        void Start()
        {
            // IPlayerを配列で複数取得しているのはなぜ？
            IPlayerState[] stateComponents = GetComponents<IPlayerState>();

            if (stateComponents.Length != (int)PlayerStateEnum.COUNT)
            {
                Debug.LogError("Stateの数が違います");
            }

            // ここでDictionaryの中身を設定してる
            for (int i = 0; i < stateComponents.Length; i++)
            {
                IPlayerState state = stateComponents[i];
                // クラスが配列になってる！？ええ！？
                // いやクラスの変数ではないのか？

                state.ChangeStateEvent += ChangeState;

                if (state.StateType == PlayerStateEnum.COUNT)
                {
                    Debug.LogError("無効なEnumです");
                    return;
                }
                if (playerStateDic.ContainsKey(state.StateType))
                {
                    Debug.LogError("Stateが重複しています");
                    return;
                }

                // 最後にここでPlayerのステートを代入
                playerStateDic[state.StateType] = state;
                //Debug.Log(state.StateType);

                // playerStateDicってのが現在のPlayerのステートを保持してる
                // Dictionaryの[Key]にvalueを代入している
                // Key(名前みたいなもん？)をPlayerのステートEnumにしている
                // Valueは現在のPlayerのステートを入れてる

                // playerStateDic[添え字] = 値をしている 
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


        // 引数の中はEnum型にしてもいいっぽい！
        // ステート変更メソッド(event変数に代入するメソッド)
        public void ChangeState(PlayerStateEnum playerState)
        {
            if (playerState is PlayerStateEnum.COUNT)
            {
                Debug.LogErrorFormat($"Count state is specified ,CurrentState{crrentPlayerState}");
                return;
            }

            playerStateDic[crrentPlayerState].OnEnd(playerState, player);

            // 中身を変更
            crrentPlayerState = playerState;

            playerStateDic[crrentPlayerState].OnStart(playerState, player);
        }
    }
}
