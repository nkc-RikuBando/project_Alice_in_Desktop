using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class MushRoomEvent : MonoBehaviour
    {
        // キノコに触れるとPlayerの大きさが変わる処理

        [SerializeField] private AudioClip _se_Big;
        [SerializeField] private AudioClip _se_Small;

        private PlayerStatus        _playerStatus;
        private PlayerStatusManager _statusManager;
        private AudioSource _audioSource;
        
        void Start()
        {
            _playerStatus = GetComponent<PlayerStatus>(); 
            _statusManager = GetComponent<PlayerStatusManager>();
            _audioSource = GetComponent<AudioSource>();
        }


        // ↓Animationのイベントで呼ぶ関数

        public void PlayerSizeChange() 
        {
            float size = transform.localScale.y;

            _playerStatus._SizeMag = _statusManager.GetSize();

            if (size == 1) return;
            if (size < 1) _audioSource.PlayOneShot(_se_Small);
            if (size > 1) _audioSource.PlayOneShot(_se_Big);
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

