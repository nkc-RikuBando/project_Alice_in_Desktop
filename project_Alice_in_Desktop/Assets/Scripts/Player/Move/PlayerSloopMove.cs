using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Connector.StageObject;

namespace Player
{
    public class PlayerSloopMove : MonoBehaviour
    {
        // ç‚à⁄ìÆèàóù

        private IInputReceivable _inputReceivable;
        private PlayerStatus _playerStatus;
        private Rigidbody2D _rb;

        private Vector2 vec;
        private const float VERTICAL_ANGLE = 90;


        private void Start()
        {
            _inputReceivable = GetComponent<IInputReceivable>();
            _playerStatus = GetComponent<PlayerStatus>();
            _rb = GetComponent<Rigidbody2D>();
        }


        // ç‚à⁄ìÆ
        private void SloopMove(float angle)
        {
            float _angle = (VERTICAL_ANGLE + (angle * transform.localScale.x)) * Mathf.Deg2Rad;
            vec = new Vector2(Mathf.Cos(_angle), Mathf.Sin(_angle));
            Debug.Log(_angle);

            _rb.velocity = new Vector2(_inputReceivable.MoveH() * _playerStatus._Speed, _rb.velocity.y) * vec.normalized;
        }

        private void PlayerStop()
        {
            _rb.sharedMaterial.friction = 100;
        }

        private void PlayerMove()
        {
            _rb.sharedMaterial.friction = 0;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var _sloop = gameObject.GetComponent<ISloopAngleSetable>();

            if(_sloop != null)
            {
                PlayerStop();
            }
        }
    }
}
