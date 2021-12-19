using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;
using Connector.Inputer;

namespace Player
{
    public class PlayerPush : MonoBehaviour
    {
        // Player���I�u�W�F�N�g����������

        private PushObjChecker    _objChecker;
        private PlayerAnimation   _playerAnimation;
        private PlayerStatus      _playerStatus;
        private IInputReceivable  _inputReceivable;
        private CapsuleCollider2D _capCol;
        private Rigidbody2D       _rb;


        void Start()
        {
            _objChecker      = GetComponent<PushObjChecker>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _playerStatus    = GetComponent<PlayerStatus>();
            _inputReceivable = GetComponent<IInputReceivable>();
            _capCol          = GetComponent<CapsuleCollider2D>();
            _rb              = GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            Push();
            OnPushObj();
        }



        // �����Ă��ԃ��\�b�h
        private void Push()
        {
            bool _isPushObj;

            // �����Ă��Ԕ���
            _isPushObj = _objChecker.PushObjWidthChecker(_capCol);

            if (_isPushObj)
            {
                if (_inputReceivable.MoveH() != 0) _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"), true);
                else _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"), false);
            }
        }

        // ������I�u�W�F�N�g�̏�ɂ��郁�\�b�h
        private void OnPushObj()
        {
            bool _isOnPushObj;

            // ������I�u�W�F�N�g�̏�ɂ��邩����
            _isOnPushObj = _objChecker.PushObjOnChecker(_capCol);

            if (_isOnPushObj)
            {
                _rb.velocity = Vector2.zero;
                _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Fall"), false);
            }
            else
            {
                _rb.gravityScale = _playerStatus._Gravity;
            }

        }
    }

}
