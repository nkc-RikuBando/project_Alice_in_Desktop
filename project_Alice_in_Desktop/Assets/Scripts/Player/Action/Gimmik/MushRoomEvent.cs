using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class MushRoomEvent : MonoBehaviour
    {
        // キノコに触れるとPlayerの大きさが変わる処理

        PlayerStatusManager _statusManager;
        
        void Start()
        {
            _statusManager = GetComponent<PlayerStatusManager>();
        }


        // ↓Animationのイベントで呼ぶ関数

        // 小さくなるメソッド
        public void PlayerSizeChange_Small() 
        {
            _statusManager.PlayerSizeChange(0.5f);
        }

        // 大きくなるメソッド
        public void PlayerSizeChange_Big() 
        {
            _statusManager.PlayerSizeChange(1.5f);
        }

        // 入力可能メソッド
        public void PlayerInput_True()
        {
            _statusManager.PlayerIsInput(true);
        }

        // 入力不可メソッド
        public void PlayerInput_False()
        {
            _statusManager.PlayerIsInput(false);
        }
    }

}

