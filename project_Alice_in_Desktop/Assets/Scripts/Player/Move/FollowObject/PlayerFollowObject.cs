using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerFollowObject : MonoBehaviour
    {
        // Playerが移動するギミックに追従する処理

        [SerializeField] private string _objName;



        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == _objName)
            {
                Debug.Log("あたった");
                transform.SetParent(collision.transform);//親オブジェクト切り替え
            }

        }

        private void OnTriggerExit2D(Collider2D collision)//離れたら
        {
            if (collision.gameObject.name == _objName)
            {
                transform.SetParent(null);//親オブジェクト切り替え
            } 
        }


        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    if (collision.gameObject.name == _objName)
        //    {
        //        Debug.Log("あたった");
        //        transform.SetParent(collision.transform);//親オブジェクト切り替え
        //    }

        //}

        //private void OnCollisionExit2D(Collision2D collision)
        //{
        //    if (collision.gameObject.name == _objName)
        //    {
        //        transform.SetParent(null);//親オブジェクト切り替え
        //    }

        //}
    }

}
