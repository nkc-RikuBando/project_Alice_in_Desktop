using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Connector.StageObject;


namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        // Player‚ÌˆÚ“®ˆ—

        private IInputReceivable _inputReceivable;
        private PlayerStatus _playerStatus;
        private PlayerState _playerState;
        private Rigidbody2D _rb;


        private void Start()
        {
            _inputReceivable = GetComponent<IInputReceivable>();
            _playerStatus = GetComponent<PlayerStatus>();
            _playerState = GetComponent<PlayerState>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_playerStatus._InputFlgX) HorizontalMove();
        }


        // ˆÚ“®ˆ—
        private void HorizontalMove()
        {
            _rb.velocity = new Vector2(_inputReceivable.MoveH() * _playerStatus._Speed, _rb.velocity.y);

            if (_inputReceivable.MoveH() != 0)
            {
                transform.localScale = new Vector3(_inputReceivable.MoveH(), 1f, 1f);
                _playerState._StateEnum = PlayerState.PlayerStateEnum.DASH;
            }
            else if (_inputReceivable.MoveH() == 0 && _rb.velocity.y == 0)
            {
                _playerState._StateEnum = PlayerState.PlayerStateEnum.STAY;
            }
        }

    }

}
