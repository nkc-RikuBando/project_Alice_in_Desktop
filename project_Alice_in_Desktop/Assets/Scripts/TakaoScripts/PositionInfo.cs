using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionInfo : MonoBehaviour
{
    [SerializeField] GameObject[] pointObjectsArray; //�q�����Ă���_
    PositionInfo[] positionInfo;

    private GameObject player;
    private GameObject rabbit;
    public bool usePosFlg { get; private set; } = true;�@//�_���g���邩�ǂ���
    private bool goPosFlg = false; //�_�Ɍ������t���O

    public float toPlayerDistance { get; private set; } //�v���C���[�Ƃ̋���(�]���l)

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
        Vector3 playerPos = player.transform.position;   //�v���C���[�̍��W
        Vector3 pointPos = gameObject.transform.position; //�_�̍��W

        toPlayerDistance = Vector3.Distance(pointPos, playerPos); //�_�̍��W�@-�@�v���C���[�̍��W�@=�@toPlayerDistance
        Debug.Log(this.gameObject.name + "��toPlayerDistance = " + toPlayerDistance);
    }

    public void GoPosition()
    {
        for(int i = 1; i < pointObjectsArray.Length; i++)
        {
            if(positionInfo[0].toPlayerDistance > positionInfo[i].toPlayerDistance)
            {
                goPosFlg = true;
                Debug.Log(this.gameObject.name + " ��goPosFlg " + " = " + goPosFlg);
            }
            else
            {
                goPosFlg = false;
            }
        }
    }
}
