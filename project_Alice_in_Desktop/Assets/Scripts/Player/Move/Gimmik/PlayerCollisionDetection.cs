using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;

namespace Player
{
    public class PlayerCollisionDetection : MonoBehaviour
    {
        // Playerが移動するギミックに追従する処理

        [SerializeField] private string[] _objName;


        // 触れたら
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // 親オブジェクト切り替え処理
            if (collision.gameObject.name == _objName[0])
            {
                transform.SetParent(collision.transform);
            }
        }


        // 離れたら
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == _objName[0])
            {
                transform.SetParent(null);
            }
        }
    }
}
