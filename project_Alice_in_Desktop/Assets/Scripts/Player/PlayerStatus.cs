using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStatus : MonoBehaviour
    {
        // Playerのステータス管理処理

        [Header("Playerのステータス管理")]

        [SerializeField, Tooltip("移動速度")] 　　            private float _speed = 5f;
        [SerializeField, Tooltip("小ジャンプ値")]             private float _smallJumpPower = 200f;
        [SerializeField, Tooltip("大ジャンプ値")]             private float _bigJumpPower   = 400f;
        [SerializeField, Tooltip("壁ジャンプ値")] 　　        private float _wallJumpPower  = 400f;
        [SerializeField, Tooltip("壁ジャンプ時の角度")]       private float _wallJumpAngle  = 45f;
        [SerializeField, Tooltip("ジャンプまでのフレーム数")] private float _bigJumpFrame   = 3f;

        // 入力フラグ
        public bool _InputFlgX { get; set; }  = true;
        public bool _InputFlgY { get; set; }  = true;


        public float _Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        public float _SmallJumpPower
        {
            get
            {
                return _smallJumpPower;
            }
            set
            {
                _smallJumpPower = value;
            }
        }

        public float _BigJumpPower
        {
            get
            {
                return _bigJumpPower;
            }
            set
            {
                _bigJumpPower = value;
            }
        }

        public float _WallJumpPower
        {
            get
            {
                return _wallJumpPower;
            }
            set
            {
                _wallJumpPower = value;
            }
        }

        public float _WallJumpAngle
        {
            get
            {
                return _wallJumpAngle;
            }
            set
            {
                _wallJumpAngle = value;
            }
        }

        public float _BigJumpFrame
        {
            get
            {
                return _bigJumpFrame;
            }
            set
            {
                _bigJumpFrame = value;
            }
        }

    }
}