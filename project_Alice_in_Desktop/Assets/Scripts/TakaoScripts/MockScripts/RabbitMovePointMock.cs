using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMovePointMock : MonoBehaviour
{
    [SerializeField] PlayerCoreMock player; //Mockなので組み込む際は変更必須
    [SerializeField] RabbitMovePointMock[] rabbitMovePoint;
    Vector3 playerPos;
    Vector3 myPos;
    float distanceValue; //評価値

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePos(); //今のポジションを受け取る
        UpdateDisValue(); //評価値を受け取る
        playerPos = player.GetPlayerPosition(); //プレイヤーの座標を受け取る
    }

    private void UpdatePos()
    {
        myPos = this.transform.position;
    }

    private void UpdateDisValue()
    {
        distanceValue = Vector3.Distance(playerPos, myPos);
    }

    public RabbitMovePointMock GetRabbitMovePointPos() //次の点を渡す
    {
        RabbitMovePointMock tempMock = null;
        for (int i = 0; i < rabbitMovePoint.Length; i++)
        {
            if(tempMock == null)
            {
                tempMock = rabbitMovePoint[i]; //とりあえずnullを渡す
            }

            if(rabbitMovePoint[i].GetDisValue() > tempMock.GetDisValue()) //今の評価値と繋がっている評価値を比べる
            {
                tempMock = rabbitMovePoint[i]; //今の評価値より、比べた評価値の方が高かったら渡す
            }
        }
        return tempMock; //点を渡す
    }

    public float GetDisValue()
    {
        return distanceValue;
    }

    public Vector3 GetMyPosition()
    {
        return myPos;
    }
}
