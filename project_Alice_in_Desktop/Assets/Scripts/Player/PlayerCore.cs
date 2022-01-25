using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;
using Connector.Inputer;
using Player;

namespace PlayerState
{
    public class PlayerCore : MonoBehaviour
    {
        private IInputReceivable _inputReceivable;

        private void Start()
        {
            _inputReceivable = GetComponent<IInputReceivable>();
        }

        private void Update()
        {
            PlayerDirection();
        }

        // Playerの向き変更メソッド
        private void PlayerDirection() 
        {
            if(_inputReceivable.MoveH() != 0) 
            {
                transform.localScale = new Vector3(_inputReceivable.MoveH(), 1f, 1f);
            }
        }
    }
}
