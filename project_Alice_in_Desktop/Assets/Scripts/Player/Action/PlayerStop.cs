using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Window;

namespace Player
{
    public class PlayerStop : MonoBehaviour, IWindowTouch, IWindowLeave
    {
        // Playerの動きを止める処理


        [SerializeField] private GameObject _fadeObj;
        [SerializeField] private GameObject _postObj;

        private FadeEffect _fadeEffect;
        private WindowEffect _windowEffect;
        private PlayerStatus _playerStatus;
        private Rigidbody2D _rb;
        private Animator _anim;
        private CapsuleCollider2D _capCol;
        private BoxCollider2D _boxCol;

        // 現在のvelocityを保存する変数
        private Vector2 _currentVec;


        void Start()
        {
            _fadeEffect = _fadeObj.GetComponent<FadeEffect>();
            _windowEffect = _postObj.GetComponent<WindowEffect>();
            _playerStatus = GetComponent<PlayerStatus>();
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _capCol = GetComponent<CapsuleCollider2D>();
            _boxCol = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            _windowEffect.DeadCaution(_playerStatus._DeadColFlg);
        }

        // Playerの挙動停止メソッド
        void IWindowTouch.WindowTouchAction()
        {
            Debug.Log("とまる");

            // 入力停止
            _playerStatus._InputFlgX = false;
            _playerStatus._InputFlgY = false;
            _playerStatus._InputFlgAction = false;

            // 地面判定停止
            //_playerStatus._GroundJudge = false;

            // 動きを停止
            _currentVec = _rb.velocity;
            _rb.velocity = Vector2.zero;
            _rb.bodyType = RigidbodyType2D.Kinematic;

            // Animationを停止
            _anim.enabled = false;

            // コライダーを消す
            _capCol.enabled = false;
            _boxCol.enabled = false;

            _windowEffect.StartWindowEffect();
        }


        // Playerの挙動再生メソッド
        void IWindowLeave.WindowLeaveAction()
        {
            Debug.Log("動く");
            // 入力可能
            _playerStatus._InputFlgX = _rb.velocity.y < 0f ? true : false;// ここが悪さしてる
            _playerStatus._InputFlgY = true;
            _playerStatus._InputFlgAction = true;

            // 地面判定停止
            _playerStatus._GroundJudge = true;

            // 物理判定可能
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.velocity = _currentVec;

            // Animation可能
            _anim.enabled = true;

            // コライダーをActive化
            _capCol.enabled = true;
            _boxCol.enabled = true;

            _windowEffect.EndWindowEffect();

            if (_playerStatus._DeadColFlg)
            {
                // 入力停止
                _playerStatus._InputFlgX = false;
                _playerStatus._InputFlgY = false;
                _playerStatus._InputFlgAction = false;

                // 動きを停止
                _rb.velocity = Vector2.zero;
                _rb.bodyType = RigidbodyType2D.Kinematic;

                _fadeEffect.StartCrushingEffect();
            }
        }
    }

}

