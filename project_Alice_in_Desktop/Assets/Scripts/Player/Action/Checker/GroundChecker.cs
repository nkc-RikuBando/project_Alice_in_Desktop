using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace MyUtility
{
    public class GroundChecker : MonoBehaviour
    {
        #pragma warning disable 649

        // 地面判定処理

        [SerializeField, Tooltip("レイの長さ")]     private float raylength = 1f;  
        [SerializeField, Tooltip("地面のレイヤー")] private LayerMask groundLayer;

        private PlayerStatus _playerStatus;
        private Rigidbody2D _rb2d;


        void Start()
        {
            _playerStatus = GetComponent<PlayerStatus>();
            _rb2d = GetComponent<Rigidbody2D>();
        }


        // 着地判定メソッド(レイの処理)
        public bool CheckIsGround(BoxCollider2D col)
        {
            bool hit;                                                             // 当たった時の判定変数
            float colHalfWidth = col.size.x / RayInterval();                      // X軸のRayの位置
            Vector3 checkPos = transform.position + (Vector3)col.offset;          // colの座標
            Vector3 lineLength = transform.up * raylength;                        // レイの長さ(要調節)

            const float JUMPUP_CHECK_SPEED = 1f;                                  // 上昇状態変数
            const int MAX_LOOP = 3;                                               // ループの回数（レイの本数）


            // 地面判定フラグ
            if (!_playerStatus._GroundJudge) return false;


            // 上昇中は何もしない
            if (_rb2d.velocity.y > JUMPUP_CHECK_SPEED)
            {
                return false;
            }


            // checkPosの位置を左端に移動
            checkPos.x -= colHalfWidth;


            // レイを引く
            for (int loop = 0; loop < MAX_LOOP; ++loop)
            {
                Debug.DrawLine(checkPos + transform.up * 0.1f, checkPos - lineLength, Color.red);// デバッグでレイを表示
                hit = Physics2D.Linecast(checkPos + transform.up * 0.1f, checkPos - lineLength, groundLayer);
                if (hit) return true;
                checkPos.x += colHalfWidth;// 座標を++していく
            }

            return false;
        }

        // Ray同士の間隔変更メソッド
        private float RayInterval() 
        {
            float scaleY = transform.localScale.y;
            float rayInterval_defaltSize = 2f;
            float rayInterval_BigSize    = 1.8f;
            float rayInterval_SmallSize  = 4f;

            if (scaleY > 1) return rayInterval_BigSize;
            if (scaleY < 1) return rayInterval_SmallSize;
            return rayInterval_defaltSize;
        }
    }



}
