using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKeyUI : MonoBehaviour
{
    
    //高さ
    public float high;
    //オブジェクト間の幅
    public float width;
    //上から見て縦、Z軸のオブジェクトの量
    public int vertical;
    //上から見て横、X軸のオブジェクトの量
    public int horizontal;

    //Prefabを入れる欄を作る
    public GameObject cube;

    //位置を入れる変数
    Vector3 pos;


    void Start()
    {
        //for(int i = 0; i < )

        Test();

        //GameObject sqObj = GameObject.Find("TestImage"); // 目的のスプライトのオブジェクトを取得
        //SpriteRenderer sqSr = sqObj.GetComponent<SpriteRenderer>();//目的のスプライトのSpriteRendererを取得

        //GameObject sqObj2 = GameObject.Find("TestImage (1)"); // 目的のスプライトのオブジェクトを取得
        ////Debug.Log("中心の座標は " + sqSr.bounds.center + " です");//中心の座標は (-0.5, 0.2, 0.0) です
        //sqObj2.transform.position = sqSr.bounds.center * -1;
    }

    void Test()
    {
        /*// このスクリプトを入れたオブジェクトの位置
        pos = transform.position;

        //Z軸にverticalの数だけ並べる
        for (int vi = 0; vi < vertical; vi++)
        {
            //X軸にhorizontalの数だけ並べる
            for (int hi = 0; hi < horizontal; hi++)
            {
                //PrefabのCubeを生成する
                GameObject copy = Instantiate(cube,
                    //生成したものを配置する位置
                    new Vector3(
                        //X軸
                        pos.x + horizontal * width / 2 - hi * width - width / 2,
                        //Y軸
                        pos.z + vertical * width / 2 - vi * width - width / 2), Quaternion.identity);
                //Z軸
            }
        }*/
    }
}
