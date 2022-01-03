using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;

namespace Player
{
    public class PlayerStatusManager : MonoBehaviour, IPlayerStatusSentable
    {
        public float ScaleMagnification { get; set; } = 1;

        private PlayerStatus _playerStatus;

        private void Start()
        {
            _playerStatus = GetComponent<PlayerStatus>();
        }

        private void Update()
        {
        }


        // 入力受付管理メソッド
        public void PlayerIsInput(bool flg)
        {
            _playerStatus._InputFlgX = flg;
            _playerStatus._InputFlgY = flg;
            _playerStatus._InputFlgAction = flg;
        }
    }

}
