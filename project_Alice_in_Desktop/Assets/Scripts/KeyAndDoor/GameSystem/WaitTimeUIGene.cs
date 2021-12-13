using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gimmicks;
using Connector.Inputer;

namespace GameSystem
{
    public class WaitTimeUIGene : MonoBehaviour
    {
        [SerializeField] private GameObject waitTimeUI; // 生成するUI
        private GameObject obj;
        private GameObject canvas; // キャンバスの保存
        private Box boxScr;        // Boxのスクリプトの保存
        private ITestKey _ITestKey; // 入力インターフェースの保存
        bool test;
        bool test2;

        void Start()
        {
            canvas = GameObject.Find("Canvas");   // キャンバスの取得
            boxScr = GetComponent<Box>();         // Boxスクリプトの取得
            _ITestKey = GetComponent<ITestKey>(); // 入力インターフェースの取得
        }

        void Update()
        {
            test = boxScr.PlHitFlg && _ITestKey.EventNagaoshiKey() ? true : false;
            if (test == true && !test2)
            {
                obj = Instantiate(waitTimeUI, canvas.transform);
                Vector3 UIpos = transform.position + new Vector3(0, 3);
                obj.transform.position = UIpos;
                test2 = true;
            }
            else if (!test)
            {
                if (obj == null) return;
                Destroy(obj.gameObject);
                test2 = false;
            }
        }
    }
}
