using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gimmicks;

namespace Player
{
    public class ExceptionObjectChaecker : MonoBehaviour
    {
        // ��O�I�u�W�F�N�g����

        private PlayerStatus _playerStatus;
        private GameObject _parentObj;


        void Start()
        {
            _parentObj    = transform.parent.gameObject;
            _playerStatus = _parentObj.GetComponent<PlayerStatus>();
        }

        private void Update()
        {
        }


        // ����������
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // isTrigger�ł͂Ȃ��I�u�W�F�N�g�������肷��
            if (collision.GetComponent<BoxCollider2D>())
            {
                var box = collision.GetComponent<BoxCollider2D>();

                if (!box.isTrigger)
                {
                    Debug.Log("���ʂ�");
                    _playerStatus._DeadColFlg = true;
                }
            }
            else if (collision.GetComponent<CompositeCollider2D>())
            {
                var tile = collision.GetComponent<CompositeCollider2D>();

                if (!tile.isTrigger)
                {
                    Debug.Log("���ʂ�");
                    _playerStatus._DeadColFlg = true;
                }
            }
        }

        // ���ꂽ��
        private void OnTriggerExit2D(Collider2D collision)
        {
            // isTrigger�ł͂Ȃ��I�u�W�F�N�g�������肷��
            if (collision.GetComponent<BoxCollider2D>())
            {
                var box = collision.GetComponent<BoxCollider2D>();

                if (!box.isTrigger)
                {
                    Debug.Log("���ȂȂ���");
                    _playerStatus._DeadColFlg = false;
                }
            }
            else if (collision.GetComponent<CompositeCollider2D>())
            {
                var tile = collision.GetComponent<CompositeCollider2D>();

                if (!tile.isTrigger)
                {
                    Debug.Log("���ȂȂ���");
                    _playerStatus._DeadColFlg = false;
                }
            }
        }
    }

}
