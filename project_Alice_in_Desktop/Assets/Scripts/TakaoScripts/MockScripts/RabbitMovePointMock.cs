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
    Vector3 playerPos;
    Vector3 rabbitPos;
    Vector3 myPos;
    float toPlayerDistanceValue; //�]���l
    float toRabbitDistanceValue;
    bool insideFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePos(); //���̃|�W�V�������󂯎��
        UpdateDisValue(); //�]���l���󂯎��
        playerPos = player.GetPlayerPosition(); //�v���C���[�̍��W���󂯎��
        rabbitPos = rabbit.transform.position;
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
            .Where(x => x.GetOutsideFlg() == true) //foreach�̒���if (x.GetOutsideFlg() == true)������̂ƈꏏ
            .OrderByDescending(x => x.GetToPlayerDisValue()); //x�̍~��(�擪����ԍ������l)
        tempMock = insideDisValueLinq.First();

        return tempMock; //�_��n��
    }

    public RabbitMovePointMock GetRabbitMovePointPosFromAll()
    {
        RabbitMovePointMock nearRabbitTemp = null;
        var insideDisValueFromAllLinq = rabbitMovePointsAll
            .Where(x => x.GetOutsideFlg() == true)
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
