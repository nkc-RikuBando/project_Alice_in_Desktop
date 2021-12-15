using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    [SerializeField] private GameObject[] movePoint; // �ړ��o�H
    [SerializeField] private float moveSpeed;        // ����

    private Rigidbody2D rigid;
    private int nowPoint = 0;
    private bool returnPoint = false;

    private Vector2 oldPos = Vector2.zero;
    private Vector2 myVelocity = Vector2.zero;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        if (movePoint != null && movePoint.Length > 0 && rigid != null)
        {
            rigid.position = movePoint[0].transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (movePoint != null && movePoint.Length > 1 && rigid != null)
        {
            //�ʏ�i�s
            if (!returnPoint)
            {
                int nextPoint = nowPoint + 1;

                //�ڕW�|�C���g�Ƃ̌덷���킸���ɂȂ�܂ňړ�
                if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
                {
                    //���ݒn���玟�̃|�C���g�ւ̃x�N�g�����쐬
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, moveSpeed * Time.deltaTime);

                    //���̃|�C���g�ֈړ�
                    rigid.MovePosition(toVector);
                }
                //���̃|�C���g���P�i�߂�
                else
                {
                    rigid.MovePosition(movePoint[nextPoint].transform.position);
                    ++nowPoint;

                    //���ݒn���z��̍Ōゾ�����ꍇ
                    if (nowPoint + 1 >= movePoint.Length)
                    {
                        returnPoint = true;
                    }
                }
            }
            //�ܕԂ��i�s
            else
            {
                int nextPoint = nowPoint - 1;

                //�ڕW�|�C���g�Ƃ̌덷���킸���ɂȂ�܂ňړ�
                if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
                {
                    //���ݒn���玟�̃|�C���g�ւ̃x�N�g�����쐬
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, moveSpeed * Time.deltaTime);

                    //���̃|�C���g�ֈړ�
                    rigid.MovePosition(toVector);
                }
                //���̃|�C���g���P�߂�
                else
                {
                    rigid.MovePosition(movePoint[nextPoint].transform.position);
                    --nowPoint;

                    //���ݒn���z��̍ŏ��������ꍇ
                    if (nowPoint <= 0)
                    {
                        returnPoint = false;
                    }
                }
            }
            myVelocity = (rigid.position - oldPos) / Time.deltaTime;
            oldPos = rigid.position;
        }
    }
}
