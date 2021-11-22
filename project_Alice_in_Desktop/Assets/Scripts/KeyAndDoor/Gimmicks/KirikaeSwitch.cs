using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirikaeSwitch : MonoBehaviour
{
    [SerializeField] private GameObject gimmick;
    private bool addSwitch = false;
    private GameObject player;

    void Start()
    {
        player = PlayerTest.player;
    }
    void Update()
    {
        //Debug.Log("–{‘Ì " + addSwitch);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            IHitSwitch hitGimmick = gimmick.GetComponent<IHitSwitch>();
            if (hitGimmick != null)
            {
                addSwitch = addSwitch ? false : true;
                hitGimmick.Switch(addSwitch);
            }
        }
    }
}
