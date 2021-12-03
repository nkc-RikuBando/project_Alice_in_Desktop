using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameSystem;

public class PlayerTest : MonoBehaviour
{
    public static GameObject player { get; private set; }

    private ClearFlg clear;

    private Vector2 inputKey;
    private Vector2 moveDir;
    private Rigidbody2D rigid;

    private void Awake()
    {
        player = gameObject;
    }

    void Start()
    {
        clear = GameObject.Find("ClearFlg").GetComponent<ClearFlg>();

        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        OtaneshiMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "ClearFlg")
        {
            //if(clear.clearFlg == true)
            {
                FadeManager.Instance.LoadScene("SampleScene", 2f);
            }
        }
    }

    void OtaneshiMove()
    {
        inputKey.x = Input.GetAxisRaw("Horizontal");
        inputKey.y = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(inputKey.x, inputKey.y);
        rigid.velocity = moveDir.normalized * 5;
    }
}
