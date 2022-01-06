using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gimmicks
{
    public class MoveXFloor : MonoBehaviour
    {
        #region BlackHole
        //[SerializeField] private GameObject[] movePoint; // 移動経路
        //[SerializeField] private float moveSpeed;        // 速さ

        //private Rigidbody2D rigid;
        //private int nowPoint = 0;
        //private bool returnPoint = false;

        //private Vector2 oldPos = Vector2.zero;
        //private Vector2 myVelocity = Vector2.zero;
        #endregion

        [Header("左か下に置くもの")]
        [SerializeField] private GameObject startPos; // 左に置くもの
        [Header("右か上に置くもの")]
        [SerializeField] private GameObject endPos;// 右に置くもの
        private float PosY;                        // Y 座標の保存変数
        private bool rightFlg = true;              // 右に動くフラグ
        private Vector3 rightDir = Vector3.right;　// 右に動く
        private Vector3 leftDir = Vector3.left;    // 左に動く
        [Range(0, 10)]
        [SerializeField] private float moveSpeed = 3f; // 動く速度

        [Header("上下移動に変更")]
        [SerializeField] private bool UpDownFlg;
        private float PosX;                        // X 座標の保存変数
        private Vector3 upDir = Vector3.up;        // 上に動く
        private Vector3 downDir = Vector3.down;    // 下に動く

        void Start()
        {
            if(UpDownFlg == false)
            {
                // 自身の Y座標を取得
                PosY = transform.position.y;
                // 左の物体のY座標を自身のY座標と一緒にする 
                startPos.transform.position = new Vector3(startPos.transform.position.x, PosY, 0);
                // 右の物体のY座標を自身のY座標と一緒にする 
                endPos.transform.position = new Vector3(endPos.transform.position.x, PosY, 0);
            }
            else
            {
                // 自身の X座標を取得
                PosX = transform.position.x;
                // 下の物体のX座標を自身のX座標と一緒にする 
                startPos.transform.position = new Vector3(PosX, startPos.transform.position.y , 0);
                // 上の物体のX座標を自身のX座標と一緒にする 
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
        /// 右に動く条件
        /// </summary>
        /// <returns></returns>
        bool IsRightMove()
        {
            return transform.position.x <= endPos.transform.position.x && rightFlg == true;
        }

        /// <summary>
        /// 左に動く条件
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
            //    //通常進行
            //    if (!returnPoint)
            //    {
            //        int nextPoint = nowPoint + 1;

            //        //目標ポイントとの誤差がわずかになるまで移動
            //        if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
            //        {
            //            //現在地から次のポイントへのベクトルを作成
            //            Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, moveSpeed * Time.deltaTime);

            //            //次のポイントへ移動
            //            rigid.MovePosition(toVector);
            //        }
            //        //次のポイントを１つ進める
            //        else
            //        {
            //            rigid.MovePosition(movePoint[nextPoint].transform.position);
            //            ++nowPoint;

            //            //現在地が配列の最後だった場合
            //            if (nowPoint + 1 >= movePoint.Length)
            //            {
            //                returnPoint = true;
            //            }
            //        }
            //    }
            //    //折返し進行
            //    else
            //    {
            //        int nextPoint = nowPoint - 1;

            //        //目標ポイントとの誤差がわずかになるまで移動
            //        if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
            //        {
            //            //現在地から次のポイントへのベクトルを作成
            //            Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, moveSpeed * Time.deltaTime);

            //            //次のポイントへ移動
            //            rigid.MovePosition(toVector);
            //        }
            //        //次のポイントを１つ戻す
            //        else
            //        {
            //            rigid.MovePosition(movePoint[nextPoint].transform.position);
            //            --nowPoint;

            //            //現在地が配列の最初だった場合
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