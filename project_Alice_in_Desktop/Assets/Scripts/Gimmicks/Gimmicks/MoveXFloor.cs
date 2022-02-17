using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Window;

namespace Gimmicks
{
    public class MoveXFloor : MonoBehaviour,IWindowTouch,IWindowLeave
    {
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

        // ↓三輪が書きました
        private bool moveFlg = true;

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
        }

        void FixedUpdate()
        {
            // ↓三輪が書きました
            // moveFlgがtureのときしか動かない
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

        // ↓三輪が書きました
        // インターフェースの関数を実装
        public void WindowTouchAction()
        {
            // ウィンドウ触ったとき
            moveFlg = false;
        }

        public void WindowLeaveAction()
        {
            // ウィンドウ離した時
            moveFlg = true;
        }
    }
}