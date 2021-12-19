using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStop : MonoBehaviour
    {
        // Player�̓������~�߂鏈��

        private PlayerStatus      _playerStatus;
        private Rigidbody2D       _rb;
        private Animator          _anim;
        private CapsuleCollider2D _capCol;
        private BoxCollider2D     _boxCol;


        void Start()
        {
            _playerStatus = GetComponent<PlayerStatus>();
            _rb         �@= GetComponent<Rigidbody2D>();
            _anim         = GetComponent<Animator>();
            _capCol       = GetComponent<CapsuleCollider2D>();
            _boxCol       = GetComponent<BoxCollider2D>();
        }
        void Update()
        {
            Stop();
            Play();
        }


        // Player�̋�����~���\�b�h
        private void Stop()
        {
            // ��~
            if (Input.GetMouseButton(0))
            {
                // ���͒�~
                _playerStatus._InputFlgX = false;
                _playerStatus._InputFlgY = false;

                // �������~
                _rb.velocity = Vector2.zero;
                _rb.bodyType = RigidbodyType2D.Kinematic;

                // Animation���~
                _anim.enabled = false;

                // �R���C�_�[������
                _capCol.enabled = false;
                _boxCol.enabled = false;
            }
        }

        // Player�̋����Đ����\�b�h
        private void Play()
        {
            // �Đ�
            if (Input.GetMouseButtonUp(0))
            {
                // ���͉\
                _playerStatus._InputFlgX = true;
                _playerStatus._InputFlgY = true;

                // ��������\
                _rb.bodyType = RigidbodyType2D.Dynamic;

                // Animation�\
                _anim.enabled = true;

                // �R���C�_�[��Active��
                _capCol.enabled = true;
                _boxCol.enabled = true;
            }

        }

    }

}

