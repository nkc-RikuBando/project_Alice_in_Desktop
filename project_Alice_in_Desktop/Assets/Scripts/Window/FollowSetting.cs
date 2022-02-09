using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Window
{
    // ウィンドウに追従するオブジェクトの設定
    // ウィンドウをさわったときに、その方向に追従しないオブジェクトは色を暗くする

    public class FollowSetting : MonoBehaviour
    {
        private WindowManager windowManager;

        [SerializeField] private bool followUpWall = false;
        [SerializeField] private bool followRightWall = false;
        [SerializeField] private bool followDownWall = false;
        [SerializeField] private bool followLeftWall = false;

        private float movementX, movementY;
        private int moveObjNum;

        private SpriteRenderer sr;
        private Tilemap tilemap;
        private bool srFlg = false;
        private bool tileFlg = false;
        private Color32 defaultColor;

        private void Start()
        {
            windowManager = GameObject.Find("WindowManager").GetComponent<WindowManager>();

            // 普通のオブジェクトとタイルマップの場合で分ける
            if (GetComponent<SpriteRenderer>() != null)
            {
                sr = GetComponent<SpriteRenderer>();
                defaultColor = sr.color;
                srFlg = true;
            }

            if (GetComponent<Tilemap>() != null)
            {
                tilemap = GetComponent<Tilemap>();
                defaultColor = tilemap.color;
                tileFlg = true;
            }

        }

        private void FixedUpdate()
        {
            Follow();
        }


        private void Follow()
        {

            // どのウィンドウをさわっているか
            moveObjNum = windowManager.GetObjNum();
            ColorChange();

            if (moveObjNum == 0 || moveObjNum == 1 || moveObjNum == 2 || moveObjNum == 9)
            {
                if (followUpWall)
                {
                    movementY = windowManager.GetMovement().y;
                    ColorReSet();
                }
            }

            if (moveObjNum == 2 || moveObjNum == 3 || moveObjNum == 4 || moveObjNum == 9)
            {
                if (followRightWall)
                {
                    movementX = windowManager.GetMovement().x;
                    ColorReSet();
                }
            }

            if (moveObjNum == 4 || moveObjNum == 5 || moveObjNum == 6 || moveObjNum == 9)
            {
                if (followDownWall)
                {
                    movementY = windowManager.GetMovement().y;
                    ColorReSet();
                }
            }

            if (moveObjNum == 6 || moveObjNum == 7 || moveObjNum == 0 || moveObjNum == 9)
            {
                if (followLeftWall)
                {
                    movementX = windowManager.GetMovement().x;
                    ColorReSet();
                }
            }

            if (moveObjNum == 8)
            {
                movementX = 0;
                movementY = 0;
                ColorReSet();
            }

            transform.position += new Vector3(movementX, movementY, 0);
        }

        private void ColorChange()
        {
            if (srFlg)
            {
                sr.color = new Color32(60, 60, 60, 255);
            }
            else if(tileFlg)
            {
                tilemap.color = new Color32(60, 60, 60, 255);
            }
        }

        private void ColorReSet()
        {
            if (srFlg)
            {
                sr.color = defaultColor;
            }
            else if(tileFlg)
            {
                tilemap.color = defaultColor;
            }
        }
    }
}