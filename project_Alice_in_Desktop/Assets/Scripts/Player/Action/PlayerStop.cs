using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStop : MonoBehaviour
    {
        // Playerの動きを止める処理

        private PlayerStatus      _playerStatus;
        private Rigidbody2D       _rb;
        private Animator          _anim;
        private CapsuleCollider2D _capCol;
        private BoxCollider2D     _boxCol;


        void Start()
        {
            _playerStatus = GetComponent<PlayerStatus>();
            _rb         　= GetComponent<Rigidbody2D>();
            _anim         = GetComponent<Animator>();
            _capCol       = GetComponent<CapsuleCollider2D>();
            _boxCol       = GetComponent<BoxCollider2D>();
        }
        void Update()
        {
            Stop();
            Play();
        }


        // Playerの挙動停止メソッド
        private void Stop()
        {
            // 停止
            if (Input.GetMouseButton(0))
            {
                // 入力停止
                _playerStatus._InputFlgX = false;
                _playerStatus._InputFlgY = false;

                // 動きを停止
                _rb.velocity = Vector2.zero;
                _rb.bodyType = RigidbodyType2D.Kinematic;

                // Animationを停止
                _anim.enabled = false;

                // コライダーを消す
                _capCol.enabled = false;
                _boxCol.enabled = false;
            }
        }

        // Playerの挙動再生メソッド
        private void Play()
        {
            // 再生
            if (Input.GetMouseButtonUp(0))
            {
                // 入力可能
                _playerStatus._InputFlgX = true;
                _playerStatus._InputFlgY = true;

                // 物理判定可能
                _rb.bodyType = RigidbodyType2D.Dynamic;

                // Animation可能
                _anim.enabled = true;

                // コライダーをActive化
                _capCol.enabled = true;
                _boxCol.enabled = true;
            }

        }

    }

}

