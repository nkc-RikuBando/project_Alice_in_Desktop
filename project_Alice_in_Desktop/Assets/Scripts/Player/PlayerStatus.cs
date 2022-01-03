using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStatus : MonoBehaviour
    {
        // Player�̃X�e�[�^�X�Ǘ�����

        [Header("Player�̃X�e�[�^�X�Ǘ�")]

        [SerializeField, Tooltip("Player�̏d��")]             private float _gravity = 1f;
        [SerializeField, Tooltip("�ړ����x")] �@�@            private float _speed   = 5f;
        [SerializeField, Tooltip("��W�����v�l")]             private float _jumpPower         = 400f;
        [SerializeField, Tooltip("�ǃW�����v�l")] �@�@        private float _wallJumpPower     = 400f;
        [SerializeField, Tooltip("�ǃW�����v���̊p�x")]       private float _wallJumpAngle     = 45f;
        [SerializeField, Tooltip("�W�����v�܂ł̎���")]       private float _jumpFeasibleCount = 0.2f;

        // ���̓t���O
        public bool _InputFlgX { get; set; }  = true;
        public bool _InputFlgY { get; set; }  = true;
        public bool _InputFlgAction { get; set; } = false;

        // ����t���O
        public bool _GroundJudge { get; set; } = true;
        public bool _WallJudge { get; set; } = true;
        public bool _GroundChecker { get; set; } = false;
        public bool _DeadColFlg { get; set; } = false;
        public bool _insideFlg { get; set; } = true;

        // �X�e�[�^�X�v���p�e�B
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