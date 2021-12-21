using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gimmicks;

namespace Player
{
    public class ExceptionObjectChaecker : MonoBehaviour
    {
        // 例外オブジェクト処理

        [SerializeField, Tooltip("Debug用Flg")] private bool _debugFlg;

        [SerializeField] private GameObject _fadeObj;

        private FadeEffect _fadeEffect;
        private PlayerStatus _playerStatus;
        private Rigidbody2D _rb;
        private GameObject _parentObj;

        private bool _colHitFlg;


        void Start()
        {
            _fadeEffect 　= _fadeObj.GetComponent<FadeEffect>();
            _parentObj    = transform.parent.gameObject;
            _playerStatus = _parentObj.GetComponent<PlayerStatus>();
            _rb           = _parentObj.GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            ExceptionDead();
        }


        // オブジェクトに重なった時メソッド
        private void ExceptionDead()
        {
            // デバッグ用
            if (_debugFlg)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    _colHitFlg = true;
                    Debug.Log(_colHitFlg);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _colHitFlg = false;
                    Debug.Log(_colHitFlg);
                }
            }


            // エラーオブジェクトに当たっている場合
            if (_playerStatus._DeadColFlg)
            {
                if (Input.GetMouseButtonUp(0))
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


        // 当たったら
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<BoxCollider2D>())
            {
                var box = collision.GetComponent<BoxCollider2D>();

                if (!box.isTrigger)
                {
                    _playerStatus._DeadColFlg = true;
                }
            }
            else if (collision.GetComponent<CompositeCollider2D>())
            {
                var tile = collision.GetComponent<CompositeCollider2D>();

                if (!tile.isTrigger)
                {
                    _playerStatus._DeadColFlg = true;
                }
            }
        }

        // 離れたら
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<BoxCollider2D>())
            {
                var box = collision.GetComponent<BoxCollider2D>();

                if (!box.isTrigger)
                {
                    _playerStatus._DeadColFlg = false;
                }
            }
            else if (collision.GetComponent<CompositeCollider2D>())
            {
                var tile = collision.GetComponent<CompositeCollider2D>();

                if (!tile.isTrigger)
                {
                    _playerStatus._DeadColFlg = false;
                }
            }
        }
    }

}
