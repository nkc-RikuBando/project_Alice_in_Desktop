using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerFollowObject : MonoBehaviour
    {
        // Playerが移動するギミックに追従する処理

        [SerializeField] private string _objName;


        // 親オブジェクト切り替え処理
        // 触れたら
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == _objName)
            {
                Debug.Log("あたった");
                transform.SetParent(collision.transform);
            }
        }
        // 離れたら
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == _objName)
            {
                transform.SetParent(null);
            } 
        }
    }

}
