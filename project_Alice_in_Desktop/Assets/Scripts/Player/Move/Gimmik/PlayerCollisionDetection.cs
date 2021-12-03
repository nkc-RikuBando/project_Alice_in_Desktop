using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;

namespace Player
{
    public class PlayerCollisionDetection : MonoBehaviour
    {
        // Player���ړ�����M�~�b�N�ɒǏ]���鏈��

        [SerializeField] private string[] _objName;

        private PlayerAnimation _playerAnimation;
        private IInputReceivable _inputReceivable;
        private Rigidbody2D _rb;

        bool _flg;

        private void Start()
        {
            _playerAnimation = GetComponent<PlayerAnimation>();
            _inputReceivable = GetComponent<IInputReceivable>();
        }

        private void Update()
        {
            if (_flg)
            {
                if (_inputReceivable.MoveH() != 0)
                {
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"), true);
                }
                else
                {
                    _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"), false);
                }
            }
            else      _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"), false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //_flg = collision.gameObject.name == _objName[1] && _inputReceivable.MoveH() != 0 ? true : false;

            if (collision.gameObject.name == _objName[1])
            {
                _flg = true;
            }
            else
            {
                _flg = false;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.name == _objName[1])
            {
                _flg = false;
            }
        }


        // �G�ꂽ��
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // �e�I�u�W�F�N�g�؂�ւ�����
            if (collision.gameObject.name == _objName[0])
            {
                transform.SetParent(collision.transform);
            }
        }


        // ���ꂽ��
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == _objName[0])
            {
                transform.SetParent(null);
            }
        }
    }
}
