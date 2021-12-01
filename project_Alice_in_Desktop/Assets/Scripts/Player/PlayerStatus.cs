using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStatus : MonoBehaviour
    {
        // Player�̃X�e�[�^�X�Ǘ�����

        [Header("Player�̃X�e�[�^�X�Ǘ�")]

        [SerializeField, Tooltip("�ړ����x")] �@�@            private float _speed = 5f;
        [SerializeField, Tooltip("���W�����v�l")]             private float _smallJumpPower = 200f;
        [SerializeField, Tooltip("��W�����v�l")]             private float _bigJumpPower   = 400f;
        [SerializeField, Tooltip("�ǃW�����v�l")] �@�@        private float _wallJumpPower  = 400f;
        [SerializeField, Tooltip("�ǃW�����v���̊p�x")]       private float _wallJumpAngle  = 45f;
        [SerializeField, Tooltip("�W�����v�܂ł̃t���[����")] private float _bigJumpFrame   = 3f;

        // ���̓t���O
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