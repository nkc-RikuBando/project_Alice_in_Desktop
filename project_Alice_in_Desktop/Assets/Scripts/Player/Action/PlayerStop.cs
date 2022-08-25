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

        private FadeEffect        _fadeEffect;
        private WindowEffect      _windowEffect;
        private PlayerStatus      _playerStatus;
        private Rigidbody2D       _rb;
        private Animator          _anim;
        private CapsuleCollider2D _capCol;
        private BoxCollider2D     _boxCol;
        private BoxCollider2D     _childBoxCol;

        // 現在のvelocityを保存する変数
        private Vector2 _currentVec;


        void Start()
        {
            _fadeEffect   = _fadeObj.GetComponent<FadeEffect>();
            _windowEffect = _postObj.GetComponent<WindowEffect>();
            _childBoxCol  = transform.GetChild(2).gameObject.GetComponent<BoxCollider2D>();

            _playerStatus = GetComponent<PlayerStatus>();
            _rb           = GetComponent<Rigidbody2D>();
            _anim         = GetComponent<Animator>();
            _capCol       = GetComponent<CapsuleCollider2D>();
            _boxCol       = GetComponent<BoxCollider2D>();
        }
        private void Update()
        {
            // ウィンドウを触っている時
            if (_playerStatus._IsWindowTouching) _windowEffect.DeadCaution(_playerStatus._DeadColFlg);
        }


        // Playerの挙動停止メソッド
        void IWindowTouch.WindowTouchAction()
        {
            // 入力停止
            _playerStatus._InputFlgX = false;
            _playerStatus._InputFlgY = false;
            _playerStatus._InputFlgAction = false;

            // 動きを停止
            _currentVec = _rb.velocity;
            _rb.velocity = Vector2.zero;
            _rb.bodyType = RigidbodyType2D.Kinematic;

            // Animationを停止
            _anim.enabled = false;

            // コライダーを消す
            _capCol.isTrigger    = true;
            _boxCol.isTrigger    = true;
            _childBoxCol.enabled = false;

            // ウィンドウ操作Flg
            _playerStatus._IsWindowTouching = true;

            // PostProcessingを有効
            _windowEffect.StartWindowEffect();
            _playerStatus._DeadColFlg = false;
        }


        // Playerの挙動再生メソッド
        void IWindowLeave.WindowLeaveAction()
        {
            // 地面判定停止
            _playerStatus._GroundJudge = true;

            // 物理判定可能
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.velocity = _currentVec;

            // 入力可能
            _playerStatus._InputFlgX = true;
            _playerStatus._InputFlgY = true;
            _playerStatus._InputFlgAction = true;

            // Animation可能
            _anim.enabled = true;

            // コライダーをActive化
            _capCol.isTrigger = false;
            _boxCol.isTrigger = false;
            _childBoxCol.enabled = true;


            // PostProcessingを無効
            _windowEffect.EndWindowEffect();

            // ウィンドウ操作Flg
            _playerStatus._IsWindowTouching = false;


            // Player死亡判定
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

