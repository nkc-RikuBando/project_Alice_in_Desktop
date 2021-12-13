using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSetting : MonoBehaviour
{
    [SerializeField] private WindowManager windowManager;

    [SerializeField] private bool followUpWall = false;
    [SerializeField] private bool followRightWall = false;
    [SerializeField] private bool followDownWall = false;
    [SerializeField] private bool followLeftWall = false;

    private GameObject upWall;
    private GameObject rightWall;
    private GameObject downWall;
    private GameObject leftWall;

    private float movementX, movementY;
    private int moveObjNum;

    private void FixedUpdate()
    {
        Follow();
    }


    private void Follow()
    {
        moveObjNum = windowManager.GetObjNum();

        if(moveObjNum==0 || moveObjNum==1 || moveObjNum==2 || moveObjNum==9)
        {
            if (followUpWall) movementY = windowManager.GetMovement().y;
        }

        if (moveObjNum == 2 || moveObjNum == 3 || moveObjNum == 4 || moveObjNum == 9)
        {
            if (followRightWall) movementX = windowManager.GetMovement().x;
        }

        if (moveObjNum == 4 || moveObjNum == 5 || moveObjNum == 6 || moveObjNum == 9)
        {
            if (followDownWall) movementY = windowManager.GetMovement().y;
        }

        if (moveObjNum == 6 || moveObjNum == 7 || moveObjNum == 0 || moveObjNum == 9)
        {
            if (followLeftWall) movementX = windowManager.GetMovement().x;
        }

        transform.position += new Vector3(movementX, movementY, 0);
    }
}
