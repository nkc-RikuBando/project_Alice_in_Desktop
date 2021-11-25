using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBox : MonoBehaviour
{
    private GameObject player;
    private Animator animator;

    [SerializeField] private GameObject hideKey;

    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();

        hideKey.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            Break();
            Destroy(gameObject);
        }
    }

    public void Break() // 箱(自分が壊れる)
    {
        hideKey.SetActive(true); //壊れたら鍵が出現
    }
}
