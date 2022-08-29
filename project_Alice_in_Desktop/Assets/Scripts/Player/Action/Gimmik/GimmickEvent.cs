using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class GimmickEvent : MonoBehaviour
    {
        // ギミックによる影響管理処理

        [SerializeField] private AudioClip _pushSE;

        private PlayerStatus        _playerStatus;
        private PlayerStatusManager _statusManager;
        private AudioSource         _audioSource;
        

        void Start()
        {
            _playerStatus  = GetComponent<PlayerStatus>(); 
            _statusManager = GetComponent<PlayerStatusManager>();
            _audioSource   = GetComponent<AudioSource>();
        }


        // -------- ↓Animationのイベントで呼ぶ関数 --------

        // サイズ変更メソッド
        public void PlayerSizeChange() 
        {
            _playerStatus._SizeMag = _statusManager.GetSize();
            transform.localScale   = transform.localScale = new Vector3(_playerStatus.DirectionNum * _playerStatus._SizeMag, 1f * _playerStatus._SizeMag, 1f);
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

        // 押した時SE再生メソッド
        public void PushPlaySE() 
        {
            _audioSource.PlayOneShot(_pushSE);
        }
    }

}

