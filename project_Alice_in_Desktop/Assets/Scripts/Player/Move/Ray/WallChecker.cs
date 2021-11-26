using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyUtility
{
    public class WallChecker : MonoBehaviour
    {
#pragma warning disable 649

        // 壁判定処理

        [SerializeField, Tooltip("レイの長さ")]     private float raylength = 1f;
        [SerializeField, Tooltip("地面のレイヤー")] private LayerMask groundLayer;


        // 着地判定メソッド(レイの処理)
        public bool CheckIsGround(CapsuleCollider2D col)
        {
            bool hit;                                               // 当たった時の判定変数
            const int MAX_LOOP = 3;                                 // ループの回数（レイの本数）
            Vector3 checkPos = transform.position;                  // プレイヤーの座標
            float colHalfWidth = col.size.y / 3.5f;                 // プレイヤーの半分のコライダーの大きさ(要調節)
            Vector3 lineLength = transform.right * raylength;       // レイの長さ(要調節)


            // checkPosの位置を左端に移動
            checkPos.y -= colHalfWidth;


            // レイを引く(3本)
            for (int loop = 0; loop < MAX_LOOP; ++loop)
            {
                Debug.DrawLine(checkPos + transform.right * 0.1f, checkPos - lineLength * -transform.localScale.x, Color.red);// デバッグでレイを表示
                hit = Physics2D.Linecast(checkPos + transform.right * 0.1f, checkPos - lineLength * -transform.localScale.x, groundLayer);
                if (hit) return true;
                checkPos.y += colHalfWidth;// 座標を＋１していく
            }

            return false;
        }

    }
}
