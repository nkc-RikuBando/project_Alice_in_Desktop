using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;

namespace Player
{
    public class PlayerStatusManager : MonoBehaviour, IPlayerStatusSentable
    {
        // ステータスを変更する処理

        private PlayerStatus _playerStatus;

        private void Awake()
        {
            _playerStatus = GetComponent<PlayerStatus>();
        }


        // 入力受付管理メソッド
        public void PlayerIsInput(bool flg)
        {
            _playerStatus._InputFlgX = flg;
            _playerStatus._InputFlgY = flg;
            _playerStatus._InputFlgAction = flg;
        }

        // Playerサイズ変更メソッド
        void IPlayerStatusSentable.PlayerSizeChange(float mag)
        {
            Vector3 _playerScale = transform.localScale;

            // 大きい→小さい or 小さい→大きい　=　通常
            if (Mathf.Abs(_playerScale.x) < 1 && mag > 1)　　   mag = 1;
            else if (Mathf.Abs(_playerScale.x) > 1 && mag < 1)　mag = 1;

            // サイズによってステータスを変更
            switch (mag) 
            {
                case 1:   // 通常
                    _playerStatus._Speed     = _playerStatus._Speed;
                    _playerStatus._JumpPower = _playerStatus._JumpPower;
                    break;
                case 0.5f:// 小さい時
                    _playerStatus._Speed     = _playerStatus._SmallStateSpeed;
                    _playerStatus._JumpPower = _playerStatus._SmallStateJumpPower;
                    break;
                case 1.5f:  // 大きい時
                    _playerStatus._Speed     = _playerStatus._BigStateSpeed;
                    _playerStatus._JumpPower = _playerStatus._BigStateJumpPower;
                    break;
                default:
                    break;
            }

            // サイズを変更
            _playerStatus._SizeMag = mag;
            transform.localScale = new Vector3(mag, mag, 1f);
        }
    }

}
