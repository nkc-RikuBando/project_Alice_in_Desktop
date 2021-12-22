using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Window;

namespace Player
{
    public class PlayerStop : MonoBehaviour, IWindowTouch, IWindowLeave
    {
        // Player�̓������~�߂鏈��


        [SerializeField] private GameObject _fadeObj;
        [SerializeField] private GameObject _postObj;

        private FadeEffect _fadeEffect;
        private WindowEffect _windowEffect;
        private PlayerStatus _playerStatus;
        private Rigidbody2D _rb;
        private Animator _anim;
        private CapsuleCollider2D _capCol;
        private BoxCollider2D _boxCol;

        // ���݂�velocity��ۑ�����ϐ�
        private Vector2 _currentVec;


        void Start()
        {
            _fadeEffect = _fadeObj.GetComponent<FadeEffect>();
            _windowEffect = _postObj.GetComponent<WindowEffect>();
            _playerStatus = GetComponent<PlayerStatus>();
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _capCol = GetComponent<CapsuleCollider2D>();
            _boxCol = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            _windowEffect.DeadCaution(_playerStatus._DeadColFlg);
        }

        // Player�̋�����~���\�b�h
        void IWindowTouch.WindowTouchAction()
        {
            Debug.Log("�Ƃ܂�");

            // ���͒�~
            _playerStatus._InputFlgX = false;
            _playerStatus._InputFlgY = false;
            _playerStatus._InputFlgAction = false;

            // �n�ʔ����~
            //_playerStatus._GroundJudge = false;

            // �������~
            _currentVec = _rb.velocity;
            _rb.velocity = Vector2.zero;
            _rb.bodyType = RigidbodyType2D.Kinematic;

            // Animation���~
            _anim.enabled = false;

            // �R���C�_�[������
            _capCol.enabled = false;
            _boxCol.enabled = false;

            _windowEffect.StartWindowEffect();
        }


        // Player�̋����Đ����\�b�h
        void IWindowLeave.WindowLeaveAction()
        {
            Debug.Log("����");
            // ���͉\
            _playerStatus._InputFlgX = _rb.velocity.y < 0f ? true : false;// �������������Ă�
            _playerStatus._InputFlgY = true;
            _playerStatus._InputFlgAction = true;

            // �n�ʔ����~
            _playerStatus._GroundJudge = true;

            // ��������\
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.velocity = _currentVec;

            // Animation�\
            _anim.enabled = true;

            // �R���C�_�[��Active��
            _capCol.enabled = true;
            _boxCol.enabled = true;

            _windowEffect.EndWindowEffect();

            if (_playerStatus._DeadColFlg)
            {
                // ���͒�~
                _playerStatus._InputFlgX = false;
                _playerStatus._InputFlgY = false;
                _playerStatus._InputFlgAction = false;

                // �������~
                _rb.velocity = Vector2.zero;
                _rb.bodyType = RigidbodyType2D.Kinematic;

                _fadeEffect.StartCrushingEffect();
            }
        }
    }

}

