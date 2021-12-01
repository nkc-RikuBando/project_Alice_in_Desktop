using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using MyUtility;

namespace Player
{
    public class PlayerSloopMove : MonoBehaviour
    {
        // ç‚à⁄ìÆèàóù

        private IInputReceivable _inputReceivable;
        private PlayerStatus _playerStatus;
        private SloopChecker _sloopChecker;
        private Rigidbody2D _rb;
        private CapsuleCollider2D _capCol;

        private float _angle = 45;
        private float _angle2;
        private Vector3 _vec;

        private void Start()
        {
            _inputReceivable = GetComponent<IInputReceivable>();
            _playerStatus = GetComponent<PlayerStatus>();
            _sloopChecker = GetComponent<SloopChecker>();
            _rb = GetComponent<Rigidbody2D>();
            _capCol = GetComponent<CapsuleCollider2D>();
        }

        private void Update()
        {
            SloopMove();
            //if (_sloopChecker.CheckIsGround(_capCol))
            //{
            //    Debug.Log("ç‚ÇæÇÊÅI");
            //    SloopMove();
            //}
        }

        private void SloopMove()
        {
            _angle2 = _angle * Mathf.Deg2Rad;

            _vec = new Vector2(Mathf.Cos(_angle2), Mathf.Sin(_angle2)).normalized;

            _rb.velocity = new Vector2(_vec.x * _inputReceivable.MoveH() * 5f, _vec.y);
        }

    }
}
