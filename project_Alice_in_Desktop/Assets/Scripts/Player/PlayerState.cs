using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;

namespace Player
{
    public class PlayerState :MonoBehaviour
    {
        // Playerの状態管理処理

        public enum PlayerStateEnum
        {
            STAY,
            DASH,
            JUMP_PREVIOUS,
            JUMP_UP,
            JUMP_DOWN,
            LANDING,
            WALLSTICK,
        }


        public PlayerStateEnum _StateEnum { get; set; } = PlayerStateEnum.STAY;

        public bool _IsAnim { get; set; } = false;
    }
}