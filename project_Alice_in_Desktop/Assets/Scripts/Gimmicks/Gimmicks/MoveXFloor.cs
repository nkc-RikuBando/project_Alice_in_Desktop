using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gimmicks
{
    public class MoveXFloor : MonoBehaviour
    {
        #region BlackHole
        //[SerializeField] private GameObject[] movePoint; // �ړ��o�H
        //[SerializeField] private float moveSpeed;        // ����

        //private Rigidbody2D rigid;
        //private int nowPoint = 0;
        //private bool returnPoint = false;

        //private Vector2 oldPos = Vector2.zero;
        //private Vector2 myVelocity = Vector2.zero;
        #endregion

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
            

            #region BlackHole
            //startPos.transform.position.y = transform.position.y;
            //rigid = GetComponent<Rigidbody2D>();
            //if (movePoint != null && movePoint.Length > 0 && rigid != null)
            //{
            //    rigid.position = movePoint[0].transform.position;
            //}
            #endregion
        }

        void FixedUpdate()
        {
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

        void Move()
        {
            //if (movePoint != null && movePoint.Length > 1 && rigid != null)
            //{
            //    //�ʏ�i�s
            //    if (!returnPoint)
            //    {
            //        int nextPoint = nowPoint + 1;

            //        //�ڕW�|�C���g�Ƃ̌덷���킸���ɂȂ�܂ňړ�
            //        if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
            //        {
            //            //���ݒn���玟�̃|�C���g�ւ̃x�N�g�����쐬
            //            Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, moveSpeed * Time.deltaTime);

            //            //���̃|�C���g�ֈړ�
            //            rigid.MovePosition(toVector);
            //        }
            //        //���̃|�C���g���P�i�߂�
            //        else
            //        {
            //            rigid.MovePosition(movePoint[nextPoint].transform.position);
            //            ++nowPoint;

            //            //���ݒn���z��̍Ōゾ�����ꍇ
            //            if (nowPoint + 1 >= movePoint.Length)
            //            {
            //                returnPoint = true;
            //            }
            //        }
            //    }
            //    //�ܕԂ��i�s
            //    else
            //    {
            //        int nextPoint = nowPoint - 1;

            //        //�ڕW�|�C���g�Ƃ̌덷���킸���ɂȂ�܂ňړ�
            //        if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
            //        {
            //            //���ݒn���玟�̃|�C���g�ւ̃x�N�g�����쐬
            //            Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, moveSpeed * Time.deltaTime);

            //            //���̃|�C���g�ֈړ�
            //            rigid.MovePosition(toVector);
            //        }
            //        //���̃|�C���g���P�߂�
            //        else
            //        {
            //            rigid.MovePosition(movePoint[nextPoint].transform.position);
            //            --nowPoint;

            //            //���ݒn���z��̍ŏ��������ꍇ
            //            if (nowPoint <= 0)
            //            {
            //                returnPoint = false;
            //            }
            //        }
            //    }
            //    myVelocity = (rigid.position - oldPos) / Time.deltaTime;
            //    oldPos = rigid.position;
            //}
        }
    }
}