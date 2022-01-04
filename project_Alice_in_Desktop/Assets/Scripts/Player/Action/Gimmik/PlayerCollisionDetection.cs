using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Gimmicks;

namespace Player
{
    public class PlayerCollisionDetection : MonoBehaviour
    {
        // Playerが移動するギミックに追従する処理


        // 触れたら
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var moveFloor = collision.GetComponent<MoveFloor>();

            // 親オブジェクト切り替え処理
            if (moveFloor != null)
            {
                transform.SetParent(collision.transform);
            }
        }


        // 離れたら
        private void OnTriggerExit2D(Collider2D collision)
        {
            var moveFloor = collision.GetComponent<MoveFloor>();

            // 親オブジェクト切り替え処理
            if (moveFloor != null)
            {
                transform.SetParent(null);
            }
        }
    }
}
