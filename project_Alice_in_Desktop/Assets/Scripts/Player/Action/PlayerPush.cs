using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;
using Connector.Inputer;

namespace Player
{
    public class PlayerPush : MonoBehaviour
    {
        // Playerがオブジェクトを押す処理

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



        // 押してる状態メソッド
        private void Push()
        {
            bool _isPushObj;

            // 押してる状態判定
            _isPushObj = _objChecker.PushObjWidthChecker(_capCol);

            if (_isPushObj)
            {
                if (_inputReceivable.MoveH() != 0) _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"), true);
                else _playerAnimation.AnimationBoolenChange(Animator.StringToHash("Push"), false);
            }
        }

        // 押せるオブジェクトの上にいるメソッド
        private void OnPushObj()
        {
            bool _isOnPushObj;

            // 押せるオブジェクトの上にいるか判定
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
