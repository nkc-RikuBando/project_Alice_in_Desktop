using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStop : MonoBehaviour
    {
        // Player‚Ì“®‚«‚ğ~‚ß‚éˆ—

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
                // “ü—Í’â~
                _playerStatus._InputFlgX = false;
                _playerStatus._InputFlgY = false;

                // “®‚«‚ğ’â~
                _rb.velocity = Vector2.zero;
                _rb.bodyType = RigidbodyType2D.Kinematic;

                // Animation‚ğ’â~
                _anim.enabled = false;
            }
            if (Input.GetMouseButtonUp(0))
            {
                // “ü—Í‰Â”\
                _playerStatus._InputFlgX = true;
                _playerStatus._InputFlgY = true;

                // •¨—”»’è‰Â”\
                _rb.bodyType = RigidbodyType2D.Dynamic;

                // Animation‰Â”\
                _anim.enabled = true;
            }
        }
    }

}

