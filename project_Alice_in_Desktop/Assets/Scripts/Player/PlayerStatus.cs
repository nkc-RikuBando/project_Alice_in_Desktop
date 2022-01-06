using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStatus : MonoBehaviour
    {
        // Playerのステータス管理処理

        [Header("Playerのステータス管理")]

        [SerializeField, Tooltip("Playerの重力")]         private float _gravity             = 1f;
        [SerializeField, Tooltip("移動速度")]             private float _speed               = 5f;
        [SerializeField, Tooltip("大ジャンプ値")]         private float _jumpPower           = 400f;
        [SerializeField, Tooltip("壁ジャンプ値")]         private float _wallJumpPower       = 400f;
        [SerializeField, Tooltip("壁ジャンプ時の角度")]   private float _wallJumpAngle       = 45f;
        [SerializeField, Tooltip("ジャンプまでの時間")]   private float _jumpFeasibleCount   = 0.2f;
        [SerializeField, Tooltip("サイズ倍率")]           private float _sizeMag             = 1f;
        [SerializeField, Tooltip("大きい時の倍率")]       private float _bigSizeMag 　　　　 = 1.5f;
        [SerializeField, Tooltip("小さい時の倍率")]       private float _smallSizeMag        = 0.5f;
        [SerializeField, Tooltip("大きい時のスピード")]   private float _bigStateSpeed       = 5f;
        [SerializeField, Tooltip("大きい時のスピード")]   private float _smallStateSpeed     = 5f;
        [SerializeField, Tooltip("大きい時のジャンプ値")] private float _bigStateJumpPower   = 550f;
        [SerializeField, Tooltip("小さい時のジャンプ値")] private float _smallStateJumpPower = 300f;

        // 入力フラグ
        public bool _InputFlgX { get; set; } = true;
        public bool _InputFlgY { get; set; } = true;
        public bool _InputFlgAction { get; set; } = false;

        // 判定フラグ
        public bool _GroundJudge { get; set; } = true;
        public bool _WallJudge { get; set; } = true;
        public bool _PushJudge { get; set; } = true;
        public bool _GroundChecker { get; set; } = false;
        public bool _IsWall { get; set; } = false;
        public bool _DeadColFlg { get; set; } = false;
        public bool _insideFlg { get; set; } = true;

        // ステータスプロパティ
        public float _Gravity
        {
            get
            {
                return _gravity;
            }
            set
            {
                _gravity = value;
            }
        }

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

        public float _JumpPower
        {
            get
            {
                return _jumpPower;
            }
            set
            {
                _jumpPower = value;
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

        public float _JumpFeasibleCount
        {
            get
            {
                return _jumpFeasibleCount;
            }
            set
            {
                _jumpFeasibleCount = value;
            }
        }

        public float _SizeMag
        {
            get
            {
                return _sizeMag;
            }
            set
            {
                _sizeMag = value;
            }
        }

        public float _BigSizeMag 
        {
            get 
            {
                return _bigSizeMag;
            }
            set 
            {
                _bigSizeMag = value;
            }
        }

        public float _SmallSizeMag 
        {
            get 
            {
                return _smallSizeMag;
            }
            set 
            {
                _smallSizeMag = value;
            }
        }

        public float _BigStateSpeed
        {
            get
            {
                return _bigStateSpeed;
            }
            set
            {
                _bigStateSpeed = value;
            }
        }

        public float _SmallStateSpeed 
        {
            get 
            {
                return _smallStateSpeed;
            }
            set 
            {
                _smallStateSpeed = value;
            }
        }

        public float _BigStateJumpPower
        {
            get
            {
                return _bigStateJumpPower;
            }
            set
            {
                _bigStateJumpPower = value;
            }
        }

        public float _SmallStateJumpPower
        {
            get
            {
                return _smallStateJumpPower;
            }
            set
            {
                _smallStateJumpPower = value;
            }
        }
    }
}