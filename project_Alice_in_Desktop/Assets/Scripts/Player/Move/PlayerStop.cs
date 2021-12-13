using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStop : MonoBehaviour
    {
        // Player�̓������~�߂鏈��

        private PlayerStatus _playerStatus;
        private Rigidbody2D _rb;
        private Animator _anim;

        void Start()
        {
            _playerStatus = GetComponent<PlayerStatus>();
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
        }

        void Update()
        {
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
            }
            if (Input.GetMouseButtonUp(0))
            {
                // ���͉\
                _playerStatus._InputFlgX = true;
                _playerStatus._InputFlgY = true;

                // ��������\
                _rb.bodyType = RigidbodyType2D.Dynamic;

                // Animation�\
                _anim.enabled = true;
            }
        }
    }

}

