using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBox : MonoBehaviour
{
    private Rigidbody2D rigid; // リジッドボディの保存
    [SerializeField] private GameObject hideKey; // 鍵オブジェクトを取得

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); // リジッドボディの取得
        hideKey.SetActive(false); // 鍵を非アクティブにする
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 自身が地面に当たったら
        if (collision.gameObject.layer == 6 && rigid.velocity.y <= 0)
        {
            hideKey.SetActive(true);         // 鍵をアクティブにする
            hideKey.transform.parent = null; // 鍵を子オブジェクトからはずす
            Destroy(gameObject);             // 自身を破棄する
        }
    }
}
