using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class MushRoomEvent : MonoBehaviour
    {
        // キノコに触れるとPlayerの大きさが変わる処理

        private PlayerStatus        _playerStatus;
        private PlayerStatusManager _statusManager;
        
        void Start()
        {
            _playerStatus = GetComponent<PlayerStatus>(); 
            _statusManager = GetComponent<PlayerStatusManager>();
        }


        // ↓Animationのイベントで呼ぶ関数

        public void PlayerSizeChange() 
        {
            _playerStatus._SizeMag = _statusManager.GetSize();
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

