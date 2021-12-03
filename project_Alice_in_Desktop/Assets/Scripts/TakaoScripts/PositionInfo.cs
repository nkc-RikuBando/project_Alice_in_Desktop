using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionInfo : MonoBehaviour
{
    [SerializeField] GameObject[] pointObjectsArray; //繋がっている点
    PositionInfo[] positionInfo;

    private GameObject player;
    private GameObject rabbit;
    public bool usePosFlg { get; private set; } = true;　//点が使えるかどうか
    private bool goPosFlg = false; //点に向かうフラグ

    public float toPlayerDistance { get; private set; } //プレイヤーとの距離(評価値)

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rabbit = GameObject.Find("Rabbit");

        positionInfo = new PositionInfo[pointObjectsArray.Length];

        for(int i = 0; i < pointObjectsArray.Length; i ++)
        {
            positionInfo[i] = pointObjectsArray[i].GetComponent<PositionInfo>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ToPlayerDistance();

        if (rabbit.transform.position == gameObject.transform.position)
        {
            GoPosition();
        }
        
    }

    public void ToPlayerDistance()
    {
        Vector3 playerPos = player.transform.position;   //プレイヤーの座標
        Vector3 pointPos = gameObject.transform.position; //点の座標

        toPlayerDistance = Vector3.Distance(pointPos, playerPos); //点の座標　-　プレイヤーの座標　=　toPlayerDistance
        Debug.Log(this.gameObject.name + "のtoPlayerDistance = " + toPlayerDistance);
    }

    public void GoPosition()
    {
        for(int i = 1; i < pointObjectsArray.Length; i++)
        {
            if(positionInfo[0].toPlayerDistance > positionInfo[i].toPlayerDistance)
            {
                goPosFlg = true;
                Debug.Log(this.gameObject.name + " のgoPosFlg " + " = " + goPosFlg);
            }
            else
            {
                goPosFlg = false;
            }
        }
    }
}
