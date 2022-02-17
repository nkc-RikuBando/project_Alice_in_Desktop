using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace Window
{
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
            FRAME_COUNT,
            HANDLE_BAR,
            CENTER,

        }

        enum ObjType
        {
            CORNER,
            EDGE,
            WINDOW,
            COUNT
        }

        private GameObject frame; // 枠
        private SpriteRenderer frameSR; // 枠のSpriteRenderer
        private float frameSizeX, frameSizeY, framePosX, framePosY; // 枠の大きさ・位置

        private int moveObjType = (int)ObjType.COUNT, moveObjNum = (int)PositionList.FRAME_COUNT; // 動かすオブジェクトの種類(点・辺・面)
        private GameObject moveObj, diagonalObj; // 動かすオブジェクトと対角のオブジェクト
        private bool moveFlg;
        private Vector3 mousePos, beforeMousePos, inputMovement, moveAxis, movement; // マウス位置・前フレームのマウス位置・マウスの移動・移動軸

        private GameObject windowColObj;
        private List<GameObject> colObjList = new List<GameObject>();
        private List<BoxCollider2D> colList = new List<BoxCollider2D> { };

        private GameObject centerColObj;
        private GameObject handleBarColObj;

        [SerializeField] private float moveSpeed = 5f; // 角・辺・面の移動速度

        // 停止するオブジェクトのリスト
        [Header("ウィンドウを触ったときに停止するやつら")]
        [SerializeField] private List<GameObject> stoppableObj;

        [Header("ウィンドウの最小サイズ")]
        [SerializeField, Range(1, 20)] private float limitSizeX = 14;
        [SerializeField, Range(1, 20)] private float limitSizeY = 7;

        private AudioSource audioSource;

        private void Start()
        {
            windowColObj = GetGameObject.WindowColObject;

            frame = GetGameObject.FrameObject;
            frameSR = frame.GetComponent<SpriteRenderer>();

            audioSource = GetComponent<AudioSource>();

            for (int i = 0; i < (int)PositionList.FRAME_COUNT; ++i)
            {
                colObjList.Add(windowColObj.transform.GetChild(i).gameObject);
                colList.Add(colObjList[i].GetComponent<BoxCollider2D>());
            }

            centerColObj = windowColObj.transform.GetChild(8).gameObject;
            handleBarColObj = windowColObj.transform.GetChild(9).gameObject;

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
            // inputMovement = new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"), 0);
            // movement = Vector3.Scale(inputMovement, moveAxis).normalized * moveSpeed * Time.deltaTime;

            if (WindowMovableJudge() == true)
            {
                movement = Vector3.Scale(inputMovement, moveAxis);
                moveObj.transform.position += movement;

                beforeMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                movement = Vector3.zero;
            }

        }

        private bool WindowMovableJudge()
        {
            if (Mathf.Abs(colObjList[(int)PositionList.LEFT].transform.position.x - colObjList[(int)PositionList.RIGHT].transform.position.x) < limitSizeX)
            {
                // 横幅が限界で
                if (moveObjNum == (int)PositionList.LEFT || moveObjNum == (int)PositionList.LEFT_UP || moveObjNum == (int)PositionList.LEFT_DOWN)
                {
                    // 左側の辺を動かしていて
                    if (inputMovement.x > 0)
                    {
                        // 右に移動させていたら
                        return false;
                    }
                }
                else if (moveObjNum == (int)PositionList.RIGHT || moveObjNum == (int)PositionList.RIGHT_UP || moveObjNum == (int)PositionList.RIGHT_DOWN)
                {
                    // 右側の辺を動かしていて
                    if (inputMovement.x < 0)
                    {
                        // 左に移動させていたら
                        return false;
                    }
                }
            }

            if (Mathf.Abs(colObjList[(int)PositionList.UP].transform.position.y - colObjList[(int)PositionList.DOWN].transform.position.y) < limitSizeY)
            {
                // 縦幅が限界で
                if (moveObjNum == (int)PositionList.UP || moveObjNum == (int)PositionList.LEFT_UP || moveObjNum == (int)PositionList.RIGHT_UP)
                {
                    // 上側の辺を動かしていて
                    if (inputMovement.y < 0)
                    {
                        // 下に移動させていたら
                        return false;
                    }
                }
                else if (moveObjNum == (int)PositionList.DOWN || moveObjNum == (int)PositionList.LEFT_DOWN || moveObjNum == (int)PositionList.RIGHT_DOWN)
                {
                    // 下側の辺を動かしていて
                    if (inputMovement.y > 0)
                    {
                        // 上に移動させていたら
                        return false;
                    }
                }
            }

            return true;
        }

        private void FrameFollow()
        {
            if (moveObjType == (int)ObjType.CORNER) // 角を動かしているとき
            {
                frameSizeX = Mathf.Abs(moveObj.transform.position.x - diagonalObj.transform.position.x) - 1;
                frameSizeY = Mathf.Abs(moveObj.transform.position.y - diagonalObj.transform.position.y) - 1;

                framePosX = (moveObj.transform.position.x + diagonalObj.transform.position.x) / 2;
                framePosY = (moveObj.transform.position.y + diagonalObj.transform.position.y) / 2;

                frameSR.size = new Vector2(frameSizeX + 1, frameSizeY + 1);
            }
            else if (moveObjType == (int)ObjType.EDGE) // 辺を動かしているとき
            {
                frameSizeX = Mathf.Abs(colObjList[3].transform.position.x - colObjList[7].transform.position.x) - 1;
                frameSizeY = Mathf.Abs(colObjList[1].transform.position.y - colObjList[5].transform.position.y) - 1;

                framePosX = (colObjList[3].transform.position.x + colObjList[7].transform.position.x) / 2;
                framePosY = (colObjList[1].transform.position.y + colObjList[5].transform.position.y) / 2;
            }
            //else if (moveObjType == (int)ObjType.WINDOW) // ウィンドウの位置を移動させているとき
            //{
            //    frameSizeX = frameSR.size.x;
            //    frameSizeY = frameSR.size.y;

            //    framePosX = handleBarColObj.transform.position.x;
            //    framePosY = (handleBarColObj.transform.position.y-0.5f) - (frameSR.size.y/2);
            //}


                frameSR.size = new Vector2(frameSizeX, frameSizeY);
                frame.transform.position = new Vector3(framePosX, framePosY, 0);

        }

        void ColSet()
        {
            // コライダーの位置を移動
            float colPosX = 0, colPosY = 0, edgeColSize = 0;
            float helfSizeX = frameSizeX / 2 + 0.5f;
            float helfSizeY = frameSizeY / 2 + 0.5f;

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
                if (i % 2 != 0)
                {
                    colList[i].size = new Vector2(edgeColSize, colList[i].size.y);
                }
            }

            //handleBarColObj.transform.position = new Vector3(framePosX, framePosY + helfSizeY);
            //handleBarColObj.GetComponent<BoxCollider2D>().size = new Vector2(frameSizeX, 1);

            centerColObj.transform.position = new Vector2(framePosX, framePosY);
            centerColObj.GetComponent<BoxCollider2D>().size = new Vector2(frameSizeX, frameSizeY);

        }

        public void SetMoveFlg(GameObject holdingObj, bool flg)
        {
            // ウィンドウのハンドル部分(上部)を持ったとき移動させる

            if(holdingObj == handleBarColObj)
            {
                //moveObj = handleBarColObj;
                //moveObjType = (int)ObjType.WINDOW;
                //moveAxis = moveAxis = new Vector3(1, 1, 0);

                //moveObjNum = (int)PositionList.HANDLE_BAR;
            }
            else
            {
                for (int i = 0; i < colObjList.Count; ++i)
                {
                    if (holdingObj == colObjList[i])
                    {
                        moveObj = colObjList[i];
                        diagonalObj = colObjList[(i + 4) % (int)PositionList.FRAME_COUNT];

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

                if(moveObjNum==(int)PositionList.FRAME_COUNT)
                {
                    moveAxis = new Vector3(0, 0, 0);
                }
            }

            moveFlg = flg;
            beforeMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (flg == false)
            {
                if (moveObj != null)
                {
                    moveObjNum = (int)PositionList.FRAME_COUNT;
                    moveObj = null;
                    movement = Vector3.zero;
                    ColSet();

                    // インターフェースを呼び出す
                    for (int i = 0; i < stoppableObj.Count; ++i)
                    {
                        stoppableObj[i].GetComponent<IWindowLeave>().WindowLeaveAction();
                    }
                }
            }
            else
            {
                // インターフェースを呼び出す
                for (int i = 0; i < stoppableObj.Count; ++i)
                {
                    stoppableObj[i].GetComponent<IWindowTouch>().WindowTouchAction();
                }

                audioSource.Play();
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
}
