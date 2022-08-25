using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace MyUtility
{
    public class WallChecker : MonoBehaviour
    {
    #pragma warning disable 649

        // 壁判定処理

        [SerializeField, Tooltip("レイの長さ")]     　　private float raylength = 1f;
        [SerializeField, Tooltip("地面(壁)のレイヤー")] private LayerMask groundLayer;

        private PlayerStatus _playerStatus;


        void Start()
        {
            _playerStatus = GetComponent<PlayerStatus>();
        }

        // 着地判定メソッド(レイの処理)
        public bool CheckIsWall(CapsuleCollider2D col)
        {
            bool hit;                                               // 当たった時の判定変数
            float rayInitialPos  = col.size.y + RayPostionAdj();    // RayのY座標初期位置
            float colHalfHeight  = col.size.y / RayInterval();      // RayのY座標間隔
            Vector3 lineLength = transform.right * raylength;       // レイを飛ばす方向と長さ
            Vector3 checkPos = transform.position;                  // プレイヤーの座標

            const int MAX_LOOP = 3;                                 // ループの回数（レイの本数）

            // 壁判定フラグ
            if (!_playerStatus._WallJudge) return false;

            // checkPosの初期位置
            checkPos.y += rayInitialPos;


            // レイを飛ばす
            hit = true;
            for (int loop = 0; loop < MAX_LOOP; ++loop)
            {
                Debug.DrawLine(checkPos + transform.right * 0.1f, checkPos - lineLength * -transform.localScale.x, Color.red);// デバッグでレイを表示
                hit &= Physics2D.Linecast(checkPos + transform.right * 0.1f, checkPos - lineLength * -transform.localScale.x, groundLayer);
                checkPos.y -= colHalfHeight;// 座標を--していく
            }

            return hit;
        }

        // Ray同士の間隔変更メソッド(Playerのサイズによって変更)
        private float RayInterval()
        {
            float scaleY = transform.localScale.y;
            float rayInterval_defaltSize = 2.4f;
            float rayInterval_BigSize    = 1.8f;
            float rayInterval_SmallSize  = 6.4f;

            if (scaleY > 1) return rayInterval_BigSize;
            if (scaleY < 1) return rayInterval_SmallSize;
            return rayInterval_defaltSize;
        }

        // Rayの位置調整メソッド(Playerのサイズによって変更)
        private float RayPostionAdj() 
        {
            float scaleY = transform.localScale.y;
            float rayPostionAdj_defaltSize = -0.2f;
            float rayPostionAdj_BigSize    =  1.3f;
            float rayPostionAdj_SmallSize  =   -2f;

            if (scaleY > 1) return rayPostionAdj_BigSize;
            if (scaleY < 1) return rayPostionAdj_SmallSize;
            return rayPostionAdj_defaltSize;
        }
    }
}
