using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMovePointMock : MonoBehaviour
{
    [SerializeField] PlayerCoreMock player; //Mock�Ȃ̂őg�ݍ��ލۂ͕ύX�K�{
    [SerializeField] RabbitMovePointMock[] rabbitMovePoint;
    Vector3 playerPos;
    Vector3 myPos;
    float distanceValue; //�]���l

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
        for (int i = 0; i < rabbitMovePoint.Length; i++)
        {
            if(tempMock == null)
            {
                tempMock = rabbitMovePoint[i]; //�Ƃ肠����null��n��
            }

            if(rabbitMovePoint[i].GetDisValue() > tempMock.GetDisValue()) //���̕]���l�ƌq�����Ă���]���l���ׂ�
            {
                tempMock = rabbitMovePoint[i]; //���̕]���l���A��ׂ��]���l�̕�������������n��
            }
        }
        return tempMock; //�_��n��
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
