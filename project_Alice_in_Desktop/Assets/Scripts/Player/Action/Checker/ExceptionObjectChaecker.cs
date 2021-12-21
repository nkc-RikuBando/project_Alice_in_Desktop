using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gimmicks;

namespace Player
{
    public class ExceptionObjectChaecker : MonoBehaviour
    {
        // ��O�I�u�W�F�N�g����

        [SerializeField, Tooltip("Debug�pFlg")] private bool _debugFlg;

        [SerializeField] private GameObject _fadeObj;

        private FadeEffect _fadeEffect;
        private PlayerStatus _playerStatus;
        private Rigidbody2D _rb;
        private GameObject _parentObj;

        private bool _colHitFlg;


        void Start()
        {
            _fadeEffect �@= _fadeObj.GetComponent<FadeEffect>();
            _parentObj    = transform.parent.gameObject;
            _playerStatus = _parentObj.GetComponent<PlayerStatus>();
            _rb           = _parentObj.GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            ExceptionDead();
        }


        // �I�u�W�F�N�g�ɏd�Ȃ��������\�b�h
        private void ExceptionDead()
        {
            // �f�o�b�O�p
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


            // �G���[�I�u�W�F�N�g�ɓ������Ă���ꍇ
            if (_playerStatus._DeadColFlg)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    // ���͒�~
                    _playerStatus._InputFlgX = false;
                    _playerStatus._InputFlgY = false;
                    _playerStatus._InputFlgAction = false;

                    // �������~
                    _rb.velocity = Vector2.zero;
                    _rb.bodyType = RigidbodyType2D.Kinematic;

                    _fadeEffect.StartCrushingEffect();
                }
            }
        }


        // ����������
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

        // ���ꂽ��
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
