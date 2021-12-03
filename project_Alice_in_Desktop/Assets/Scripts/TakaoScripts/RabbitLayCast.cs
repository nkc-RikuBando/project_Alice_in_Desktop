using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitLayCast : MonoBehaviour
{
    #pragma warning disable 649

    // 地面判定処理

    // クラス変数
    private Rigidbody2D rb2d;
    private CapsuleCollider2D col2d;

    // 地面判定用の変数
    [SerializeField] private float raylength = 1f;  // レイの長さ

    [SerializeField] LayerMask groundLayer;         // 地面のレイヤー 

    

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントを取得
        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsGround(col2d);
    }

    /// <summary>
    /// 着地判定メソッド(レイの処理)
    /// </summary>
    /// <param name="col"></param>
    /// <returns></returns>
    public bool CheckIsGround(CapsuleCollider2D col)
    {
        bool hit;                                       // 当たった時の判定変数
        const int MAX_LOOP = 3;                         // ループの回数（レイの本数）
        Vector3 checkPos = transform.position;          // プレイヤーの座標
        float colHalfWidth = col.size.y / 3.5f;         // プレイヤーの半分のコライダーの大きさ(要調節)
        Vector3 lineLength = transform.right * raylength;  // レイの長さ(要調節)

        // checkPosの位置を左端に移動
        checkPos.y -= colHalfWidth;

        // レイを引く(3本)
        for (int loop = 0; loop < MAX_LOOP; ++loop)
        {
            Debug.DrawLine(checkPos + transform.right * 0.1f, checkPos - lineLength, Color.red);// デバッグでレイを表示
            hit = Physics2D.Linecast(checkPos + transform.right * 0.1f, checkPos - lineLength, groundLayer);
            if (hit) return true;
            checkPos.y += colHalfWidth;// 座標を＋１していく
        }

        return false;
    }

}


