using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RabbitMovePointMock : MonoBehaviour,IRenderingFlgSettable
{
    [SerializeField] PlayerPositionGet player; //Mockなので組み込む際は変更必須
    [SerializeField] GameObject rabbit;
    [SerializeField] RabbitMovePointMock[] rabbitMovePoint;
    [SerializeField] RabbitMovePointMock[] rabbitMovePointsAll;
    Vector3 playerPos;
    Vector3 rabbitPos;
    Vector3 myPos;
    float distanceValue; //評価値
    bool insideFlg = false;

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
        rabbitPos = rabbit.transform.position;
        Debug.Log(rabbitPos);
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
        var insideDisValueLinq = rabbitMovePoint
            .Where(x => x.GetOutsideFlg() == true) //foreachの中でif (x.GetOutsideFlg() == true)をするのと一緒
            .OrderByDescending(x => x.GetDisValue()); //xの降順(先頭が一番高い数値)
        tempMock = insideDisValueLinq.First();

        return tempMock; //点を渡す
    }

    public RabbitMovePointMock GetRabbitMovePointPosFromAll()
    {
        RabbitMovePointMock nearRabbitTemp = null;
        var insideDisValueFromAllLinq = rabbitMovePointsAll
            .Where(x => x.GetOutsideFlg() == true)
            .OrderBy(x => x.GetRabbitPos());
        nearRabbitTemp = insideDisValueFromAllLinq.First();
        return nearRabbitTemp;
    }

    public float GetDisValue()
    {
        return distanceValue;
    }

    public Vector3 GetMyPosition()
    {
        return myPos;
    }

    public Vector3 GetRabbitPos()
    {
        return rabbitPos;
    }

    public bool GetOutsideFlg()
    {
        Debug.Log("insideFlg  = " + insideFlg);
        //insideFlg = true;
        return insideFlg;
    }

    public void SetRenderingFlg(bool val)
    {
        insideFlg = val;
    }
}
