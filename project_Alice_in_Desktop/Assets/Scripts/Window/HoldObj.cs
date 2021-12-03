using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObj : MonoBehaviour
{
    private GameObject catchObj;
    private WindowManager windowManager;
    [SerializeField] private LayerMask mouseTouchableLayer;

    private void Start()
    {
        windowManager = GetComponent<WindowManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // クリックしたとき
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, mouseTouchableLayer);

            if (hit2d)
            {
                catchObj = hit2d.collider.gameObject;
                windowManager.SetMoveFlg(catchObj, true);


            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            // クリック離した時
            windowManager.SetMoveFlg(catchObj, false);
            catchObj = null;
        }

        
    }
    public GameObject GetCatchObj()
    {
        return catchObj;
    }
}