using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    [SerializeField] private GameObject[] movePoint; // 移動経路
    [SerializeField] private float moveSpeed;        // 速さ

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
            //通常進行
            if (!returnPoint)
            {
                int nextPoint = nowPoint + 1;

                //目標ポイントとの誤差がわずかになるまで移動
                if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
                {
                    //現在地から次のポイントへのベクトルを作成
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, moveSpeed * Time.deltaTime);

                    //次のポイントへ移動
                    rigid.MovePosition(toVector);
                }
                //次のポイントを１つ進める
                else
                {
                    rigid.MovePosition(movePoint[nextPoint].transform.position);
                    ++nowPoint;

                    //現在地が配列の最後だった場合
                    if (nowPoint + 1 >= movePoint.Length)
                    {
                        returnPoint = true;
                    }
                }
            }
            //折返し進行
            else
            {
                int nextPoint = nowPoint - 1;

                //目標ポイントとの誤差がわずかになるまで移動
                if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
                {
                    //現在地から次のポイントへのベクトルを作成
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, moveSpeed * Time.deltaTime);

                    //次のポイントへ移動
                    rigid.MovePosition(toVector);
                }
                //次のポイントを１つ戻す
                else
                {
                    rigid.MovePosition(movePoint[nextPoint].transform.position);
                    --nowPoint;

                    //現在地が配列の最初だった場合
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
