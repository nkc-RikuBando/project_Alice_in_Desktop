using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    enum PositionList
    {
        LEFT_UP,
        UP,
        RIGHT_UP,
        RIGHT,
        RIGHT_DOWN,
        DOWN,
        LEFT_DOWN,
        LEFT,
        COUNT, 
        CENTER

    }

    enum ObjType
    {
        CORNER,
        EDGE,
        WINDOW,
        COUNT
    }

    [SerializeField] private GameObject frame; // 枠
    private SpriteRenderer frameSR; // 枠のSpriteRenderer
    private float frameSizeX, frameSizeY, framePosX, framePosY; // 枠の大きさ・位置

    private int moveObjType=4,moveObjNum=9; // 動かすオブジェクトの種類(点・辺・面)
    private GameObject moveObj, diagonalObj; // 動かすオブジェクトと対角のオブジェクト
    private bool moveFlg;
    private Vector3 mousePos, beforeMousePos, inputMovement, moveAxis, movement; // マウス位置・前フレームのマウス位置・マウスの移動・移動軸

    [Header("枠のリスト(左上から時計回り)")]
    [SerializeField] private List<GameObject> colObjList;
    private List<BoxCollider2D> colList = new List<BoxCollider2D> { };

    [Header("中心(描画部分)")]
    [SerializeField] private GameObject centerColObj;

    [SerializeField] private float moveSpeed = 5f; // 角・辺・面の移動速度

    private void Start()
    {
        frameSR = frame.GetComponent<SpriteRenderer>();

        for(int i=0;i<colObjList.Count;++i)
        {
            colList.Add(colObjList[i].GetComponent<BoxCollider2D>());
        }

        framePosX = frameSR.transform.position.x;
        framePosY = frameSR.transform.position.y;
        frameSizeX = frameSR.size.x;
        frameSizeY = frameSR.size.y;

        ColSet();
    }

    private void FixedUpdate()
    {
        if (!moveFlg || moveObj == null) return;

        // 所持しているオブジェクトの移動
        Move();

        // 枠の移動
        FrameFollow();

        // 枠に付随するコライダーの移動
        ColSet();

    }

    private void Move()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        inputMovement = mousePos - beforeMousePos;
        inputMovement = new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"), 0);
        movement = Vector3.Scale(inputMovement, moveAxis).normalized * moveSpeed * Time.deltaTime;
        moveObj.transform.position += movement;
        beforeMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FrameFollow()
    {
        if (moveObjType == (int)ObjType.CORNER) // 角を動かしているとき
        {
            frameSizeX = Mathf.Abs(moveObj.transform.position.x - diagonalObj.transform.position.x)-1;
            frameSizeY = Mathf.Abs(moveObj.transform.position.y - diagonalObj.transform.position.y)-1;

            framePosX = (moveObj.transform.position.x + diagonalObj.transform.position.x) / 2;
            framePosY = (moveObj.transform.position.y + diagonalObj.transform.position.y) / 2;

            frameSR.size = new Vector2(frameSizeX + 1, frameSizeY + 1);
        }
        else if (moveObjType == (int)ObjType.EDGE) // 辺を動かしているとき
        {
            frameSizeX = Mathf.Abs(colObjList[3].transform.position.x - colObjList[7].transform.position.x)-1;
            frameSizeY = Mathf.Abs(colObjList[1].transform.position.y - colObjList[5].transform.position.y)-1;

            framePosX = (colObjList[3].transform.position.x + colObjList[7].transform.position.x) / 2;
            framePosY = (colObjList[1].transform.position.y + colObjList[5].transform.position.y) / 2;
        }
        else if (moveObjType == (int)ObjType.WINDOW) // ウィンドウの位置を移動させているとき
        {
            frameSizeX = frameSR.size.x;
            frameSizeY = frameSR.size.y;

            framePosX = centerColObj.transform.position.x;
            framePosY = centerColObj.transform.position.y;
        }

        frameSR.size = new Vector2(frameSizeX, frameSizeY);
        frame.transform.position = new Vector3(framePosX, framePosY, 0);
    }

    void ColSet()
    {
        float colPosX=0, colPosY=0, edgeColSize=0;
        float helfSizeX= frameSizeX / 2+0.5f;
        float helfSizeY= frameSizeY / 2+0.5f;

        for (int i = 0; i < colObjList.Count; ++i)
        {
            switch (i)
            {
                case (int)PositionList.LEFT_UP:
                    colPosX = framePosX - helfSizeX;
                    colPosY = framePosY + helfSizeY;
                    break;

                case (int)PositionList.UP:
                    colPosX = framePosX;
                    colPosY = framePosY + helfSizeY;
                    edgeColSize = frameSR.size.x;
                    break;

                case (int)PositionList.RIGHT_UP:
                    colPosX = framePosX + helfSizeX;
                    colPosY = framePosY + helfSizeY;
                    break;

                case (int)PositionList.RIGHT:
                    colPosX = framePosX + helfSizeX;
                    colPosY = framePosY;
                    edgeColSize = frameSR.size.y;
                    break;

                case (int)PositionList.RIGHT_DOWN:
                    colPosX = framePosX + helfSizeX;
                    colPosY = framePosY - helfSizeY;
                    break;

                case (int)PositionList.DOWN:
                    colPosX = framePosX;
                    colPosY = framePosY - helfSizeY;
                    edgeColSize = frameSR.size.x;
                    break;

                case (int)PositionList.LEFT_DOWN:
                    colPosX = framePosX - helfSizeX;
                    colPosY = framePosY - helfSizeY;
                    break;

                case (int)PositionList.LEFT:
                    colPosX = framePosX - helfSizeX;
                    colPosY = framePosY;
                    edgeColSize = frameSR.size.y;
                    break;
                default:
                    break;
            }

            colObjList[i].transform.position = new Vector3(colPosX, colPosY, 0);
            if(i%2!=0)
            {
                colList[i].size = new Vector2(edgeColSize, colList[i].size.y);
            }
        }

        centerColObj.transform.position = new Vector2(framePosX,framePosY);
        centerColObj.GetComponent<BoxCollider2D>().size = new Vector2(frameSizeX , frameSizeY);

    }

    public void SetMoveFlg(GameObject holdingObj, bool flg)
    {
        moveObj = holdingObj;
        if (holdingObj == centerColObj)
        {
            moveObj = centerColObj;
            moveObjType = (int)ObjType.WINDOW;
            moveAxis = moveAxis = new Vector3(1, 1, 0);
            Debug.Log("win");

            moveObjNum = (int)PositionList.CENTER;

        }
        else
        {
            for (int i = 0; i < colObjList.Count; ++i)
            {
                if (holdingObj == colObjList[i])
                {
                    moveObj = colObjList[i];
                    diagonalObj = colObjList[(i + 4) % (int)PositionList.COUNT];

                    switch (i)
                    {
                        case (int)PositionList.UP:
                        case (int)PositionList.DOWN:
                            moveAxis = new Vector3(0, 1, 0);
                            moveObjType = (int)ObjType.EDGE;
                            break;

                        case (int)PositionList.LEFT:
                        case (int)PositionList.RIGHT:
                            moveAxis = new Vector3(1, 0, 0);
                            moveObjType = (int)ObjType.EDGE;
                            break;

                        default:
                            moveAxis = new Vector3(1, 1, 0);
                            moveObjType = (int)ObjType.CORNER;
                            break;
                    }
                    moveObjNum = i;
                    break;
                }
            }
        }
        moveFlg = flg;
        beforeMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (flg == false)
        {
            ColSet();
        }
    }

    public Vector3 GetMovement()
    {
        if (moveFlg)
        {
            return movement;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public int GetObjNum()
    {
        return moveObjNum;
    }

}
