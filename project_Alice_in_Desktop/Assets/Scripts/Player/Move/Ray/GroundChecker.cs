using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyUtility
{
    public class GroundChecker : MonoBehaviour
    {
        #pragma warning disable 649

        // 地面判定処理

        [SerializeField, Tooltip("レイの長さ")]     private float raylength = 1f;  
        [SerializeField, Tooltip("地面のレイヤー")] private LayerMask groundLayer;

        private Rigidbody2D rb2d;


        // Start is called before the first frame update
        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }


        // 着地判定メソッド(レイの処理)
        public bool CheckIsGround(CapsuleCollider2D col)
        {
            const float JUMPUP_CHECK_SPEED = 1f;
            bool hit;                                       // 当たった時の判定変数
            const int MAX_LOOP = 3;                         // ループの回数（レイの本数）
            Vector3 checkPos = transform.position;          // プレイヤーの座標
            float colHalfWidth = col.size.x / 3.5f;         // プレイヤーの半分のコライダーの大きさ(要調節)
            Vector3 lineLength = transform.up * raylength;  // レイの長さ(要調節)


            // 上昇中は何もしない
            if (rb2d.velocity.y > JUMPUP_CHECK_SPEED)
            {
                return false;
            }


            // checkPosの位置を左端に移動
            checkPos.x -= colHalfWidth;

            // レイを引く(3本)
            for (int loop = 0; loop < MAX_LOOP; ++loop)
            {
                Debug.DrawLine(checkPos + transform.up * 0.1f, checkPos - lineLength, Color.red);// デバッグでレイを表示
                hit = Physics2D.Linecast(checkPos + transform.up * 0.1f, checkPos - lineLength, groundLayer);
                if (hit) return true;
                checkPos.x += colHalfWidth;// 座標を＋１していく
            }

            return false;
        }

    }



}
