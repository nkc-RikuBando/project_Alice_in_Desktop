using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    // Playerの状態管理処理
    public enum PlayerStateEnum
    {
        STAY,
        DASH,
        JUMP_PREVIOUS,
        JUMP_UP,
        JUMP_DOWN,
        LANDING,
        WALLSTICK,
        PUSH
    }


    public class PlayerStatus : MonoBehaviour
    {
        // Playerのステータス管理処理

        [Header("Playerのステータス管理")]

        [SerializeField, Tooltip("Playerの重力")]             private float _gravity = 1f;
        [SerializeField, Tooltip("移動速度")] 　　            private float _speed = 5f;
        [SerializeField, Tooltip("大ジャンプ値")]             private float _jumpPower   = 400f;
        [SerializeField, Tooltip("壁ジャンプ値")] 　　        private float _wallJumpPower  = 400f;
        [SerializeField, Tooltip("壁ジャンプ時の角度")]       private float _wallJumpAngle  = 45f;
        [SerializeField, Tooltip("ジャンプまでの時間")]       private float _jumpFeasibleCount = 0.2f;

        // Playerステート変数
        public PlayerStateEnum _StateEnum { get; set; } = PlayerStateEnum.STAY;

        // 入力フラグ
        public bool _InputFlgX { get; set; }  = true;
        public bool _InputFlgY { get; set; }  = true;

        // 判定フラグ
        public bool _GroundJudge { get; set; } = true;
        public bool _WallJudge { get; set; } = true;
        public bool _GroundChecker { get; set; } = false;


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

        public float JumpFeasibleCount
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

    }
}