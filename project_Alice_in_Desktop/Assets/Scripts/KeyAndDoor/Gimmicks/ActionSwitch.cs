using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSwitch : MonoBehaviour
{
    [SerializeField] private GameObject gimmick;
    private bool addSwitch = false;
    private GameObject player;

    void Start()
    {
        player = PlayerTest.player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            Debug.Log("QQQ");
            IHitSwitch hitGimmick = gimmick.GetComponent<IHitSwitch>();
            if (hitGimmick != null)
            {
                if (addSwitch == false) addSwitch = true;
                else if (addSwitch == true) addSwitch = false;
                hitGimmick.Switch(addSwitch);
            }
        }
    }
}
