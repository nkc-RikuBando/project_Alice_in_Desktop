using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RabbitMovePointMock : MonoBehaviour,IRenderingFlgSettable
{
    [SerializeField] PlayerPositionGet player; //Mock‚È‚Ì‚Å‘g‚İ‚ŞÛ‚Í•ÏX•K{
    [SerializeField] GameObject rabbit;
    [SerializeField] RabbitMovePointMock[] rabbitMovePoint;
    [SerializeField] RabbitMovePointMock[] rabbitMovePointsAll;
    Vector3 playerPos;
    Vector3 rabbitPos;
    Vector3 myPos;
    float distanceValue; //•]‰¿’l
    bool insideFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePos(); //¡‚Ìƒ|ƒWƒVƒ‡ƒ“‚ğó‚¯æ‚é
        UpdateDisValue(); //•]‰¿’l‚ğó‚¯æ‚é
        playerPos = player.GetPlayerPosition(); //ƒvƒŒƒCƒ„[‚ÌÀ•W‚ğó‚¯æ‚é

    }

    private void UpdatePos()
    {
        myPos = this.transform.position;
    }

    private void UpdateDisValue()
    {
        distanceValue = Vector3.Distance(playerPos, myPos);
    }

    public RabbitMovePointMock GetRabbitMovePointPos() //Ÿ‚Ì“_‚ğ“n‚·
    {
        RabbitMovePointMock tempMock = null;
        var insideDisValueLinq = rabbitMovePoint
            .Where(x => x.GetOutsideFlg() == true) //foreach‚Ì’†‚Åif (x.GetOutsideFlg() == true)‚ğ‚·‚é‚Ì‚Æˆê
            .OrderByDescending(x => x.GetDisValue()); //x‚Ì~‡(æ“ª‚ªˆê”Ô‚‚¢”’l)
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

        //    if(rabbitMovePoint[i].GetDisValue() > tempMock.GetDisValue()) //¡‚Ì•]‰¿’l‚ÆŒq‚ª‚Á‚Ä‚¢‚é•]‰¿’l‚ğ”ä‚×‚é
        //    {
        //        tempMock = rabbitMovePoint[i]; //¡‚Ì•]‰¿’l‚æ‚èA”ä‚×‚½•]‰¿’l‚Ì•û‚ª‚‚©‚Á‚½‚ç“n‚·
        //    }
        //}
        return tempMock; //“_‚ğ“n‚·
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
