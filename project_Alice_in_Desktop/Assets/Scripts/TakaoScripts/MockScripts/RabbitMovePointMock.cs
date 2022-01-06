using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RabbitMovePointMock : MonoBehaviour,IRenderingFlgSettable
{
    [SerializeField] PlayerPositionGet player; //Mock�Ȃ̂őg�ݍ��ލۂ͕ύX�K�{
    [SerializeField] GameObject rabbit;
    [SerializeField] RabbitMovePointMock[] rabbitMovePoint;
    [SerializeField] RabbitMovePointMock[] rabbitMovePointsAll;

    private BoxCollider2D box2d;
    private PointRayHit pointRayHit;
    Vector3 playerPos;
    Vector3 rabbitPos;
    Vector3 myPos;
    float toPlayerDistanceValue; //�]���l
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
        UpdatePos(); //���̃|�W�V�������󂯎��
        UpdateDisValue(); //�]���l���󂯎��
        playerPos = player.GetPlayerPosition(); //�v���C���[�̍��W���󂯎��
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

    public RabbitMovePointMock GetRabbitMovePointPos() //���̓_��n��
    {
        RabbitMovePointMock tempMock = null;
        var insideDisValueLinq = rabbitMovePoint
            .Where(x => x.GetUseFlg() == true) //foreach�̒���if (x.GetOutsideFlg() == true)������̂ƈꏏ
            .OrderByDescending(x => x.GetToPlayerDisValue()); //x�̍~��(�擪����ԍ������l)
        tempMock = insideDisValueLinq.First();

        return tempMock; //�_��n��
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
