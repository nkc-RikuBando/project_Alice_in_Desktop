using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class TestKeyUI : MonoBehaviour/*, IKeyCount*/
    {
        ////高さ
        ////public float high;
        ////オブジェクト間の幅
        //public float width;
        ////上から見て縦、Z軸のオブジェクトの量
        ////public int vertical;
        ////上から見て横、X軸のオブジェクトの量
        //public int horizontal;

        ////Prefabを入れる欄を作る
        //public GameObject cube;

        ////位置を入れる変数
        //Vector3 pos;

        [SerializeField] private float width; //オブジェクト間の幅
        [SerializeField] private GameObject geneKeyUI; // 生成するUI
        private Vector3 uiPos; // UIの生成位置を保存
        private int geneKeyNum;

        void Start()
        {
            //Debug.Log(geneKeyNum);
        }

        void Test()
        {
            //// このスクリプトを入れたオブジェクトの位置
            //pos = transform.position;

            ////Z軸にverticalの数だけ並べる
            ////for (int vi = 0; vi < vertical; vi++)
            //{
            //    //X軸にhorizontalの数だけ並べる
            //    for (int hi = 0; hi < horizontal; hi++)
            //    {
            //        //PrefabのCubeを生成する
            //        GameObject copy
            //            = Instantiate(cube, new Vector3(pos.x + horizontal * width / 2 - hi * width - width / 2, pos.y), Quaternion.identity);
            //    }
            //}
        }

        //public void keyCount(int num)
        //{
        //    geneKeyNum = num;
        //}

        void KeyCountUI()
        {
            // このスクリプトを入れたオブジェクトの位置
            uiPos = transform.position;

            //X軸にhorizontalの数だけ並べる
            for (int hi = 0; hi < geneKeyNum; hi++)
            {
                Vector3 genePos = new Vector3(uiPos.x + geneKeyNum * width / 2 - hi * width - width / 2, uiPos.y);
                //PrefabのCubeを生成する
                GameObject copy = Instantiate(geneKeyUI, genePos, Quaternion.identity);
            }
        }
    }
}
