using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gimmicks;

namespace Player
{
    public class ExceptionObjectChaecker : MonoBehaviour
    {
        // 例外オブジェクト処理

        private PlayerStatus _playerStatus;
        private GameObject _parentObj;


        void Start()
        {
            _parentObj    = transform.parent.gameObject;
            _playerStatus = _parentObj.GetComponent<PlayerStatus>();
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
