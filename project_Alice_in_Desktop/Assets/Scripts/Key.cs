using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private GameObject player;

    private GameObject goal;
    private IGetKey iGetter;

    void Start()
    {
        player = GameObject.Find("Player");

        goal = GameObject.Find("ClearFlg");
        iGetter = goal.GetComponent<IGetKey>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            iGetter.GetKey(gameObject);
        }
    }
}
