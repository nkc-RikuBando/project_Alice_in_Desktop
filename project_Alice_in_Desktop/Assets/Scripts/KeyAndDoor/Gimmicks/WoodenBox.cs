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

    public void Break() // ��(����������)
    {
        hideKey.SetActive(true); //��ꂽ�献���o��
    }
}
