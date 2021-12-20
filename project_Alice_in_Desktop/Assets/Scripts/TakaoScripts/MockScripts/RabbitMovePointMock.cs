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
    float distanceValue; //�]���l
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

    }

    private void UpdatePos()
    {
        myPos = this.transform.position;
    }

    private void UpdateDisValue()
    {
        distanceValue = Vector3.Distance(playerPos, myPos);
    }

    public RabbitMovePointMock GetRabbitMovePointPos() //���̓_��n��
    {
        RabbitMovePointMock tempMock = null;
        var insideDisValueLinq = rabbitMovePoint
            .Where(x => x.GetOutsideFlg() == true) //foreach�̒���if (x.GetOutsideFlg() == true)������̂ƈꏏ
            .OrderByDescending(x => x.GetDisValue()); //x�̍~��(�擪����ԍ������l)
        tempMock = insideDisValueLinq.First();

        //for (int i = 0; i < rabbitMovePoint.Length; i++)
        //{
        //    Debug.Log(rabbitMovePoint[i].GetOutsideFlg());
        //    if (rabbitMovePoint[i].GetOutsideFlg() == false)
        //    {
        //        Debug.Log(i);
        //        continue;
        //    }
        //    else
        //    {
        //        tempMock = rabbitMovePoint[i];
        //    }

        //    if(rabbitMovePoint[i].GetDisValue() > tempMock.GetDisValue()) //���̕]���l�ƌq�����Ă���]���l���ׂ�
        //    {
        //        tempMock = rabbitMovePoint[i]; //���̕]���l���A��ׂ��]���l�̕�������������n��
        //    }
        //}
        return tempMock; //�_��n��
    }

    public RabbitMovePointMock GetRabbitMovePointPosFromAll()
    {
        RabbitMovePointMock tempMock = null;
        var insideDisValueFromAllLinq = rabbitMovePointsAll
            .Where(x => x.GetOutsideFlg() == true);
        tempMock = insideDisValueFromAllLinq.First();
        return tempMock;
    }

    public float GetDisValue()
    {
        return distanceValue;
    }

    public Vector3 GetMyPosition()
    {
        return myPos;
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
