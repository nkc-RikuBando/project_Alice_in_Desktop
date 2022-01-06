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

    private BoxCollider2D box2d;
    private PointRayHit pointRayHit;
    Vector3 playerPos;
    Vector3 rabbitPos;
    Vector3 myPos;
    float toPlayerDistanceValue; //評価値
    float toRabbitDistanceValue;
    bool usePointFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        pointRayHit = GetComponent<PointRayHit>();
        box2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePos(); //今のポジションを受け取る
        UpdateDisValue(); //評価値を受け取る
        playerPos = player.GetPlayerPosition(); //プレイヤーの座標を受け取る
        rabbitPos = rabbit.transform.position;
        SetCollisionObjFlg();
    }

    private void UpdatePos()
    {
        myPos = this.transform.position;
    }

    private void UpdateDisValue()
    {
        toPlayerDistanceValue = Vector3.Distance(playerPos, myPos);
        toRabbitDistanceValue = Vector3.Distance(rabbitPos, myPos);
    }

    public RabbitMovePointMock GetRabbitMovePointPos() //次の点を渡す
    {
        RabbitMovePointMock tempMock = null;
        var insideDisValueLinq = rabbitMovePoint
            .Where(x => x.GetUseFlg() == true) //foreachの中でif (x.GetOutsideFlg() == true)をするのと一緒
            .OrderByDescending(x => x.GetToPlayerDisValue()); //xの降順(先頭が一番高い数値)
        tempMock = insideDisValueLinq.First();

        return tempMock; //点を渡す
    }

    public RabbitMovePointMock GetRabbitMovePointPosFromAll()
    {
        RabbitMovePointMock nearRabbitTemp = null;
        var insideDisValueFromAllLinq = rabbitMovePointsAll
            .Where(x => x.GetUseFlg() == true)
            .OrderBy(x => x.GetToRabbitDistanceValue());
        nearRabbitTemp = insideDisValueFromAllLinq.First();
        return nearRabbitTemp;
    }

    public float GetToPlayerDisValue()
    {
        return toPlayerDistanceValue;
    }

    public float GetToRabbitDistanceValue()
    {
        return toRabbitDistanceValue;
    }

    public Vector3 GetMyPosition()
    {
        return myPos;
    }

    public Vector3 GetRabbitPos()
    {
        return rabbitPos;
    }

    public bool GetUseFlg()
    {
        return usePointFlg;
    }

    public void SetCollisionObjFlg()
    {
        if(pointRayHit.CheakIsCollisionObj(box2d))
        {
            usePointFlg = false;
        }
        else
        {
            usePointFlg = true;
        }
    }

    public void SetRenderingFlg(bool val)
    {
        usePointFlg = val;
    }
}
