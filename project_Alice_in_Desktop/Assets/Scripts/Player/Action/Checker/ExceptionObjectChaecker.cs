using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.MySceneManager;
using Gimmicks;

namespace Player
{
    public class ExceptionObjectChaecker : MonoBehaviour
    {
        // 例外オブジェクト処理

        [SerializeField, Tooltip("Debug用Flg")] private bool _debugFlg;

        private ISceneChange _sceneChange;
        private PlayerStatus _playerStatus;
        private Rigidbody2D _rb;
        private CapsuleCollider2D _capCol;
        private GameObject _parentObj;

        private bool _colHitFlg;


        void Start()
        {
            _parentObj    = transform.parent.gameObject;
            _playerStatus = _parentObj.GetComponent<PlayerStatus>();
            _capCol       = _parentObj.GetComponent<CapsuleCollider2D>();
            _rb           = _parentObj.GetComponent<Rigidbody2D>();
            _sceneChange  = GameObject.Find("SceneManager").GetComponent<ISceneChange>();
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
            if (_colHitFlg)
            {
                // これいるっけ？
                //_capCol.enabled = false;

                if (Input.GetMouseButtonUp(0))
                {
                    StartCoroutine("PlayerDead");
                    _colHitFlg = true;
                }
            }
            //else _capCol.enabled = true;
        }

        // Playerが死ぬコルーチン
        IEnumerator PlayerDead()
        {
            // 死ぬアニメーション再生

            Debug.Log("死にそう！");

            // 入力停止
            _playerStatus._InputFlgX = false;
            _playerStatus._InputFlgY = false;
            _playerStatus._InputFlgAction = false;

            // 動きを停止
            _rb.velocity = Vector2.zero;
            _rb.bodyType = RigidbodyType2D.Kinematic;

            yield return new WaitForSeconds(2);

            Debug.Log("死んだ！");

            _sceneChange.ReloadScene();
        }

        // 当たったら
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<BoxCollider2D>())
            {
                var box = collision.GetComponent<BoxCollider2D>();

                if (!box.isTrigger)
                {
                    _colHitFlg = true;
                }
            }
            else if (collision.GetComponent<CompositeCollider2D>())
            {
                var tile = collision.GetComponent<CompositeCollider2D>();

                if (!tile.isTrigger)
                {
                    _colHitFlg = true;
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
                    _colHitFlg = false;
                }
            }
            else if (collision.GetComponent<CompositeCollider2D>())
            {
                var tile = collision.GetComponent<CompositeCollider2D>();

                if (!tile.isTrigger)
                {
                    _colHitFlg = false;
                }
            }
        }
    }

}
