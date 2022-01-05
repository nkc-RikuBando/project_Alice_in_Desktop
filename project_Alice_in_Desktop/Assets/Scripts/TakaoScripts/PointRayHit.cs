using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRayHit : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private BoxCollider2D box2d;
    public int distance = 10;
    [SerializeField, Tooltip("接触オブジェクトのレイヤー")] private LayerMask layerMasks;

    // 地面判定用の変数
    [SerializeField] private float raylength = 1f;  // レイの長さ

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントを取得
        rb2d = GetComponent<Rigidbody2D>();
        box2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit();
        CheakIsCollisionObj(box2d);
    }

    public bool CheakIsCollisionObj(BoxCollider2D col)
    {
        bool hit = false;
        Vector3 checkPos = transform.position;          // プレイヤーの座標
        float colHalfWidth = col.size.y / 0.5f;         // プレイヤーの半分のコライダーの大きさ(要調節)
        Vector3 lineLength = transform.up * raylength;  // レイの長さ(要調節)

        // checkPosの位置を左端に移動
        checkPos.y += colHalfWidth;

        Debug.DrawLine(checkPos + transform.up, checkPos - lineLength, Color.red);// デバッグでレイを表示
        hit = Physics2D.Linecast(checkPos + transform.up , checkPos - lineLength,layerMasks);

        Debug.Log(hit);
        return hit;
    }
    private void RaycastHit()
    {
        //RaycastHit2D raycastHit = Physics2D.Raycast(gameObject.transform.position, transform.up);
        ////Rayの作成　　　　　　　↓Rayを飛ばす原点　　　↓Rayを飛ばす方向
        ////Ray ray = new Ray(gameObject.transform.position, transform.up);

        ////Rayが当たったオブジェクトの情報を入れる箱
        //RaycastHit hit;

        ////Rayの飛ばせる距離
        ////int distance = 0;

        ////Rayの可視化  ↓Rayの原点　↓Rayの方向　↓Rayの色
        //Debug.DrawRay(gameObject.transform.position, transform.up, Color.red);
    }
}
