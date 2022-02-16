using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Window;

namespace Gimmicks
{
    public class MoveXFloor : MonoBehaviour,IWindowTouch,IWindowLeave
    {
        [Header("�������ɒu������")]
        [SerializeField] private GameObject startPos; // ���ɒu������
        [Header("�E����ɒu������")]
        [SerializeField] private GameObject endPos;// �E�ɒu������
        private float PosY;                        // Y ���W�̕ۑ��ϐ�
        private bool rightFlg = true;              // �E�ɓ����t���O
        private Vector3 rightDir = Vector3.right;�@// �E�ɓ���
        private Vector3 leftDir = Vector3.left;    // ���ɓ���
        [Range(0, 10)]
        [SerializeField] private float moveSpeed = 3f; // �������x

        [Header("�㉺�ړ��ɕύX")]
        [SerializeField] private bool UpDownFlg;
        private float PosX;                        // X ���W�̕ۑ��ϐ�
        private Vector3 upDir = Vector3.up;        // ��ɓ���
        private Vector3 downDir = Vector3.down;    // ���ɓ���

        // ���O�ւ������܂���
        private bool moveFlg = true;

        void Start()
        {
            if(UpDownFlg == false)
            {
                // ���g�� Y���W���擾
                PosY = transform.position.y;
                // ���̕��̂�Y���W�����g��Y���W�ƈꏏ�ɂ��� 
                startPos.transform.position = new Vector3(startPos.transform.position.x, PosY, 0);
                // �E�̕��̂�Y���W�����g��Y���W�ƈꏏ�ɂ��� 
                endPos.transform.position = new Vector3(endPos.transform.position.x, PosY, 0);
            }
            else
            {
                // ���g�� X���W���擾
                PosX = transform.position.x;
                // ���̕��̂�X���W�����g��X���W�ƈꏏ�ɂ��� 
                startPos.transform.position = new Vector3(PosX, startPos.transform.position.y , 0);
                // ��̕��̂�X���W�����g��X���W�ƈꏏ�ɂ��� 
                endPos.transform.position = new Vector3(PosX,endPos.transform.position.y, 0);
            }
        }

        void FixedUpdate()
        {
            // ���O�ւ������܂���
            // moveFlg��ture�̂Ƃ����������Ȃ�
            if (!moveFlg) return;

            //Move();
            if (UpDownFlg == false)
            {
                if (IsRightMove())
                    transform.position += rightDir * Time.deltaTime * moveSpeed;

                if (IsLeftMove())
                    transform.position += leftDir * Time.deltaTime * moveSpeed;
            }
            else
            {
                if (IsUpMove())
                    transform.position += upDir * Time.deltaTime * moveSpeed;

                if (IsDownMove())
                    transform.position += downDir * Time.deltaTime * moveSpeed;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == startPos) rightFlg = true;
            if (collision.gameObject == endPos) rightFlg = false;
        }

        /// <summary>
        /// �E�ɓ�������
        /// </summary>
        /// <returns></returns>
        bool IsRightMove()
        {
            return transform.position.x <= endPos.transform.position.x && rightFlg == true;
        }

        /// <summary>
        /// ���ɓ�������
        /// </summary>
        /// <returns></returns>
        bool IsLeftMove()
        {
            return transform.position.x >= startPos.transform.position.x && rightFlg == false;
        }

        bool IsUpMove()
        {
            return transform.position.y <= endPos.transform.position.y && rightFlg == true;
        }

        bool IsDownMove()
        {
            return transform.position.y >= startPos.transform.position.y && rightFlg == false;
        }

        // ���O�ւ������܂���
        // �C���^�[�t�F�[�X�̊֐�������
        public void WindowTouchAction()
        {
            // �E�B���h�E�G�����Ƃ�
            moveFlg = false;
        }

        public void WindowLeaveAction()
        {
            // �E�B���h�E��������
            moveFlg = true;
        }
    }
}