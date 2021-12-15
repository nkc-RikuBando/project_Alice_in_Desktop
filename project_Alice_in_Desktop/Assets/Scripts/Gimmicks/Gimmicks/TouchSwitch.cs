using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gimmicks
{
    public class TouchSwitch : MonoBehaviour
    {
        [SerializeField] private GameObject gimmick;
        private bool addSwitch = false;
        private GameObject player;

        void Start()
        {
            player = GameObject.Find("PlayerTest");
            //player = PlayerTest.player;
        }
        void Update()
        {
            //Debug.Log("–{‘Ì " + addSwitch);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!gimmick) return;
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
}
