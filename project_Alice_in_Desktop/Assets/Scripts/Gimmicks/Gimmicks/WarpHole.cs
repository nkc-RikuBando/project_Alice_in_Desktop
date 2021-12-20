using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;

namespace Gimmicks

    // 頑張っててえらい！！！！！！
{
    public class WarpHole : MonoBehaviour
    {
        [SerializeField] private GameObject player; // プレイヤーオブジェクトを取得
        private ITestKey _ITestKey;
        [SerializeField] private GameObject warpPoint; // ワープ先オブジェクトを取得
        private bool stayFlg = false;                  // 滞在しているかフラグ

        private GameObject camObj;
        private Camera cam;
        private bool cameraZoomFlg;

        void Start()
        {
            _ITestKey = GetComponent<ITestKey>();
            //cameraZoomFlg = false;
            //camObj = GameObject.Find("Main Camera");
            //cam = camObj.GetComponent<Camera>();
        }

        void Update()
        {
            if (StayInput())
            {
                Warp();
                //cameraZoomFlg = true;
            }
        }

        void FixedUpdate()
        {
            //if (cameraZoomFlg == true)
            //{
            //    cam.orthographicSize -= 5 * Time.deltaTime;
            //    if (cam.orthographicSize <= 0)
            //    {
            //        cam.orthographicSize += 5 * Time.deltaTime;
            //    }
            //    else if (cam.orthographicSize >= 7)
            //    {
            //        cam.orthographicSize = 7;
            //        cameraZoomFlg = false;
            //    }
            //}
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            // プレイヤーが入って来たら
            if (collision.gameObject == player) stayFlg = true; // 滞在フラグをtrue
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // プレイヤーが出て行ったら
            if (collision.gameObject == player) stayFlg = false; // 滞在フラグをfalse
        }

        /// <summary>
        /// プレイヤーが触れている、かつ、Qキーを押す
        /// </summary>
        /// <returns></returns>
        bool StayInput()
        {
            return stayFlg == true && _ITestKey.EventKey();
        }

        /// <summary>
        /// ワープ先にプレイヤーを移動させる
        /// </summary>
        void Warp()
        {
            player.transform.position = warpPoint.transform.position;
        }

        //IEnumerator Damage()
        //{
        //    // while文を10回ループ
        //    int count = 10;
        //    while (count > 0)
        //    {
        //        // 0.05秒待つ
        //        yield return new WaitForSeconds(0.05f);
                
        //        // 0.05秒待つ
        //        yield return new WaitForSeconds(0.05f);
        //        count--;
        //    }
        //}
    }
}
