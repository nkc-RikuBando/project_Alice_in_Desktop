using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyUtility
{
    public class PushObjChecker : MonoBehaviour
    {
#pragma warning disable 649

        // 壁判定処理

        [SerializeField, Tooltip("レイの長さ(下)")] private float raylength_Under = 1f;
        [SerializeField, Tooltip("レイの長さ(横)")] private float raylength_Width = 1f;
        [SerializeField, Tooltip("地面(壁)のレイヤー")] private LayerMask groundLayer;

        private Rigidbody2D rb2d;


        private void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }


        // 押すオブジェクト判定メソッド(レイの処理)
        public bool PushObjWidthChecker(CapsuleCollider2D col)
        {
            bool hit;                                                 // 当たった時の判定変数
            float colHalfHeight = col.size.y / 3f;                    // RayのY座標初期位置
            float colHalfHeight2 = col.size.y / 15f;                  // RayのY座標間隔
            Vector3 lineLength = transform.right * raylength_Width;   // レイを飛ばす方向と長さ
            Vector3 checkPos = transform.position;                    // プレイヤーの座標

            const int MAX_LOOP = 3;                                   // ループの回数（レイの本数）


            // checkPosの初期位置
            checkPos.y += colHalfHeight;


            // レイを飛ばす
            hit = true;
            for (int loop = 0; loop < MAX_LOOP; ++loop)
            {
                //Debug.DrawLine(checkPos + transform.right * 0.1f, checkPos - lineLength * -transform.localScale.x, Color.green);// デバッグでレイを表示
                hit &= Physics2D.Linecast(checkPos + transform.right * 0.1f, checkPos - lineLength * -transform.localScale.x, groundLayer);
                checkPos.y -= colHalfHeight2;// 座標を--していく
            }

            return hit;
        }


        // 着地判定メソッド(レイの処理)
        public bool PushObjOnChecker(CapsuleCollider2D col)
        {
            bool hit;                                               // 当たった時の判定変数
            float colHalfWidth = col.size.x / 3.5f;                 // X軸のRayの位置
            Vector3 checkPos = transform.position;                  // プレイヤーの座標
            Vector3 lineLength = transform.up * raylength_Under;    // レイの長さ(要調節)

            const float JUMPUP_CHECK_SPEED = 1f;                    // 上昇状態変数
            const int MAX_LOOP = 3;                                 // ループの回数（レイの本数）


            // 上昇中は何もしない
            if (rb2d.velocity.y > JUMPUP_CHECK_SPEED)
            {
                return false;
            }


            // checkPosの位置を左端に移動
            checkPos.x -= colHalfWidth;


            // レイを引く
            for (int loop = 0; loop < MAX_LOOP; ++loop)
            {
                //Debug.DrawLine(checkPos + transform.up * 0.1f, checkPos - lineLength, Color.green);// デバッグでレイを表示
                hit = Physics2D.Linecast(checkPos + transform.up * 0.1f, checkPos - lineLength, groundLayer);
                if (hit) return true;
                checkPos.x += colHalfWidth;// 座標を++していく
            }

            return false;
        }

    }
}
