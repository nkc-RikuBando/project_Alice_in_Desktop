using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Connector.Player;

public class RabbitMovePointMock : MonoBehaviour,IRenderingFlgSettable
{
    [SerializeField] GameObject player; //Mock�Ȃ̂őg�ݍ��ލۂ͕ύX�K�{
    [SerializeField] GameObject rabbit;
    [SerializeField] RabbitMovePointMock[] rabbitMovePoint;
    [SerializeField] RabbitMovePointMock[] rabbitMovePointsAll;

    private BoxCollider2D box2d;
    private PointRayHit pointRayHit;
    Vector3 playerPos;
    Vector3 rabbitPos;
    Vector3 myPos;
    IPlayerPotionSentable playerPositionSentable;
    float toPlayerDistanceValue; //�]���l
    float toRabbitDistanceValue;
    bool collisionPointFlg = false;
    bool outsideFlg = false;
    bool usePointFlg = true;
    bool tempUseFlg;

    // Start is called before the first frame update
    void Start()
    {
        pointRayHit = GetComponent<PointRayHit>();
        box2d = GetComponent<BoxCollider2D>();
        playerPositionSentable = player.gameObject.GetComponent<IPlayerPotionSentable>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePos(); //���̃|�W�V�������󂯎��
        UpdateDisValue(); //�]���l���󂯎��
        playerPos = playerPositionSentable.PlayerPotionSentable();//�v���C���[�̍��W���󂯎��
        if(!(rabbit == null))
        {
            rabbitPos = rabbit.transform.position;
        }
        
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
        for(int i = 0;i < rabbitMovePoint.Length;i++)
        {
            tempUseFlg = rabbitMovePoint[i].GetUseFlg();
            if (tempUseFlg)
            {
                break;
            }
        }

        if(tempUseFlg)
        {
            var insideDisValueLinq = rabbitMovePoint
            .Where(x => x.GetUseFlg() == true) //foreach�̒���if (x.GetOutsideFlg() == true)������̂ƈꏏ
            .OrderByDescending(x => x.GetToPlayerDisValue()); //x�̍~��(�擪����ԍ������l)
            tempMock = insideDisValueLinq.First();
        }
        else
        {
            tempMock = default;
        }
        return tempMock; //�_��n��
    }

    public RabbitMovePointMock GetRabbitMovePointPosFromAll()
    {
        RabbitMovePointMock nearRabbitTemp = null;
        for (int i = 0; i < rabbitMovePointsAll.Length; i++)
        {
            tempUseFlg = rabbitMovePointsAll[i].GetUseFlg();
            if(tempUseFlg)
            {
                break;
            }
        }

        if(tempUseFlg)
        {
            var insideDisValueFromAllLinq = rabbitMovePointsAll
            .Where(x => x.GetUseFlg() == true)
            .OrderBy(x => x.GetToRabbitDistanceValue());
            nearRabbitTemp = insideDisValueFromAllLinq.First();
        }
        else
        {
            nearRabbitTemp = default;
        }
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
        if(outsideFlg && collisionPointFlg)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetCollisionObjFlg()
    {
        if(pointRayHit.CheakIsCollisionObj(box2d))
        {
            collisionPointFlg = false;
        }
        else
        {
            collisionPointFlg = true;
        }
    }

    public void SetRenderingFlg(bool val)
    {
        outsideFlg = val;
    }
}
