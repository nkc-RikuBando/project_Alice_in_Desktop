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
        private Rigidbody2D  _rb;

        private void Awake()
        {
            _playerStatus = GetComponent<PlayerStatus>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O)) 
            {
                _rb.velocity = Vector2.zero;
                PlayerIsInput(false);
            }
            else if (Input.GetKeyDown(KeyCode.P)) 
            {
                PlayerIsInput(true);
            }
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
